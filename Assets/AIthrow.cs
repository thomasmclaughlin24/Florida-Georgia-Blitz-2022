using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIthrow : MonoBehaviour
{
    public GameObject football;
    public int delay = 5;
    private float pickuptime = 0f;
    private float v = 8f;
    public Transform player;
    private Player PlayerInfo;

    // Start is called before the first frame update
    void Start()
    {
        PlayerInfo = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (PlayerInfo.hasball == true)
        {
            if (pickuptime == 0f)
            {
                pickuptime = Time.time;
            }
            if (Time.time - pickuptime > delay)
            {
                
            }
        }
        */
    }

    public void Throw(Transform target)
    {
        pickuptime = 0f;
        football.GetComponent<Rigidbody>().isKinematic = false;
        football.transform.parent = null;
        /*float dist = Vector3.Distance(player.position, transform.position);
        float angle = Mathf.Atan((Mathf.Pow(v, 2) - Mathf.Sqrt(Mathf.Pow(v, 4) + Physics.gravity.magnitude * (-Physics.gravity.magnitude * Mathf.Pow(player.position.x, 2) + 2 * player.position.y * Mathf.Pow(v, 2))) / -Physics.gravity.magnitude * player.position.x));
        Debug.Log(angle);
        football.transform.LookAt(player);
        football.transform.rotation = Quaternion.EulerAngles(transform.rotation.x, transform.rotation.y, angle);
        football.GetComponent<Rigidbody>().velocity = v * Vector3.forward * 1000;*/
        Debug.Log("Thrower: " + gameObject + ", Receiver: " + target.gameObject + ", Distance: " + Vector3.Distance(target.position, transform.position));
        /*if(Vector3.Distance(target.position, transform.position) > 30)
        {
            v = 25f;
        }
        else if (Vector3.Distance(target.position, transform.position) > 25)
        {
            v = 20f;
        }
        else if (Vector3.Distance(target.position, transform.position) > 20)
        {
            v = 15f;
        }
        else if (Vector3.Distance(target.position, transform.position) > 10)
        {
            v = 13.5f;
        }
        else
        {
            v = 8f;
        }*/
        ThrowBallAtTargetLocation(target.position);
        PlayerInfo.hasball = false;
        PlayerInfo.pm.playerWithBall = null;
        PlayerInfo.pm.TimeSinceThrow = Time.time;
    }

    public void ThrowBallAtTargetLocation(Vector3 targetLocation)
    {
        Vector3 direction = (targetLocation - transform.position).normalized;
        float distance = Vector3.Distance(targetLocation, transform.position);
        Vector3 velocity;
        do
        {
            float firingElevationAngle = FiringElevationAngle(Physics.gravity.magnitude, distance, v);
            Vector3 elevation = Quaternion.AngleAxis(firingElevationAngle, transform.right) * transform.up;
            float directionAngle = AngleBetweenAboutAxis(transform.forward, direction, transform.up);
            velocity = Quaternion.AngleAxis(directionAngle, transform.up) * elevation * v;
            v += 1f;
        }
        while (float.IsNaN(velocity.x + velocity.y + velocity.z));
        // ballGameObject is object to be thrown
        football.GetComponent<Rigidbody>().AddForce(velocity, ForceMode.VelocityChange);
    }

    // Helper method to find angle between two points (v1 & v2) with respect to axis n
    public static float AngleBetweenAboutAxis(Vector3 v1, Vector3 v2, Vector3 n)
    {
        return Mathf.Atan2(
            Vector3.Dot(n, Vector3.Cross(v1, v2)),
            Vector3.Dot(v1, v2)) * Mathf.Rad2Deg;
    }

    // Helper method to find angle of elevation (ballistic trajectory) required to reach distance with initialVelocity
    // Does not take wind resistance into consideration.
    private float FiringElevationAngle(float gravity, float distance, float initialVelocity)
    {
        float angle = 0.5f * Mathf.Asin((gravity * distance) / (initialVelocity * initialVelocity)) * Mathf.Rad2Deg;
        return angle;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Throwing : MonoBehaviour
{
    private Player PlayerInfo;
	public float throwforce;
	public Transform player;
    private bool MouseDown = false;
    private bool increase = true;
    public GameObject football;
    // Start is called before the first frame update
    void Start()
    {
        PlayerInfo = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
	    if(Mouse.current.leftButton.isPressed && PlayerInfo.hasball == true){
            MouseDown = true;
            if (increase)
            {
                if (throwforce < 800)
                {
                    throwforce += 1;
                }
                if(throwforce == 800)
                {
                    increase = false;
                }
            }
            else
            {
                if (throwforce > 0)
                {
                    throwforce--;
                }
                if(throwforce == 0)
                {
                    increase = true;
                }
            }
	    }
        else if(PlayerInfo.hasball == true && MouseDown)
        {
            Debug.Log("Throwing");
            football.GetComponent<Rigidbody>().isKinematic = false;
            football.transform.parent = null;
            Vector3 force = player.forward.normalized * throwforce + Vector3.up * throwforce;
            var target = predictLandingPosition(force, football.GetComponent<Rigidbody>());
            football.GetComponent<Football>().setTarget(target.position);
            football.GetComponent<Rigidbody>().AddForce(force);
            PlayerInfo.hasball = false;
            PlayerInfo.pm.playerWithBall = null;
            throwforce = 0;
            MouseDown = false;
        }
    }

    Pose predictLandingPosition(Vector3 force, Rigidbody fbrb)
    {
        Pose target = new Pose(Vector3.zero, Quaternion.identity);
        Physics.autoSimulation = false;
        fbrb.AddForce(force);
        Vector3 startpos = fbrb.position;
        Quaternion startrot = fbrb.rotation;
        float timeoutTime = 15f;
        while (timeoutTime >= Time.fixedDeltaTime)
        {
            timeoutTime -= Time.fixedDeltaTime;
            Physics.Simulate(Time.fixedDeltaTime);
            target.position = fbrb.position;
            target.rotation = fbrb.rotation;
            if (target.position.y < 1f || Mathf.Approximately(target.position.y, 1f))
            {
                Physics.autoSimulation = true;
                fbrb.velocity = Vector3.zero;
                fbrb.transform.position = startpos;
                fbrb.transform.rotation = startrot;
                return target;
            }
        }
        Physics.autoSimulation = true;
        fbrb.velocity = Vector3.zero;
        fbrb.transform.position = startpos;
        fbrb.transform.rotation = startrot;
        Debug.Log("Didnt Land");
        return target;
    }
}

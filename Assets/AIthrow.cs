using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIthrow : MonoBehaviour
{
    public GameObject football;
    public int delay = 5;
    private float pickuptime = 0f;
    public float throwforce;
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
        if(PlayerInfo.hasball == true)
        {
            if(pickuptime == 0f)
            {
                pickuptime = Time.time;
            }
            if(Time.time - pickuptime > delay)
            {
                pickuptime = 0f;
                football.GetComponent<Rigidbody>().isKinematic = false;
                football.transform.parent = null;
                transform.LookAt(player);
                football.GetComponent<Rigidbody>().AddForce(transform.forward.normalized * throwforce + Vector3.up * throwforce);
                PlayerInfo.hasball = false;
                PlayerInfo.pm.playerWithBall = null;
            }
        }        
    }
}

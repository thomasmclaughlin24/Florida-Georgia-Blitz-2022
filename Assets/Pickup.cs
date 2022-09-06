using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pickup : MonoBehaviour
{
    public GameObject player;
    public GameObject football;
    public GameObject slot;
    public float range = 0;
    public float cooldown = 2f;
    //internal bool holding = false;
    private Player PlayerInfo;
    // Start is called before the first frame update
    void Start()
    {
        PlayerInfo = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(football.transform.position, player.transform.position) < range && !PlayerInfo.hasball && Time.time - PlayerInfo.pm.TimeSinceThrow > cooldown){
		    football.transform.SetParent(slot.transform);
		    football.transform.position = slot.transform.position;
		    football.GetComponent<Rigidbody>().isKinematic = true;
            football.transform.localEulerAngles = new Vector3(-74, -29, 38);
            PlayerInfo.pm.playerWithBall = PlayerInfo;
            PlayerInfo.hasball = true;
            if(PlayerInfo.inEndzone == true)
            {
                if (Array.IndexOf(PlayerInfo.pm.team1, PlayerInfo) > -1)
                {
                    PlayerInfo.pm.team1score += 7;
                    PlayerInfo.pm.Reset();
                }
                if (Array.IndexOf(PlayerInfo.pm.team2, PlayerInfo) > -1)
                {
                    PlayerInfo.pm.team2score += 7;
                    PlayerInfo.pm.Reset();
                }
            }
	    }
    }
}

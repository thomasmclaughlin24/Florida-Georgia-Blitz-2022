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
    public Player PlayerInfo;
    public PlayerManager pm;
    // Start is called before the first frame update
    void Start()
    {
        PlayerInfo = GetComponent<Player>();
        if(PlayerInfo == null)
        {
            PlayerInfo = GetComponent<AIPlayer>();
        }
        pm = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(football.transform.position, player.transform.position) < range && !PlayerInfo.hasball && Time.time - pm.TimeSinceThrow > cooldown){
            PickupBall();
	    }
    }

    public void PickupBall()
    {
        pm.footballGrounded = false;
        football.transform.SetParent(slot.transform);
        football.transform.position = slot.transform.position;
        football.GetComponent<Rigidbody>().isKinematic = true;
        football.transform.localEulerAngles = new Vector3(-74, -29, 38);
        pm.playerWithBall = PlayerInfo;
        PlayerInfo.hasball = true;
        if (PlayerInfo.inEndzone == true)
        {
            if (Array.IndexOf(pm.team1, PlayerInfo) > -1)
            {
                pm.team1score += 7;
                pm.Reset();
            }
            if (Array.IndexOf(pm.team2, PlayerInfo) > -1)
            {
                pm.team2score += 7;
                pm.Reset();
            }
        }
    }
}

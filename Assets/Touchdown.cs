using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Touchdown : MonoBehaviour
{
    public PlayerManager playerManager;
    public bool isTeam1 = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (playerManager.playerWithBall && playerManager.playerWithBall.gameObject == other.gameObject)
        {
            if (player == null)
            {
                player = other.gameObject.GetComponent<AIPlayer>();
            }
            if (Array.IndexOf(playerManager.team1, player) > -1 && isTeam1 == true)
            {
                playerManager.team1score += 7;
                playerManager.Reset();
            }
            if (Array.IndexOf(playerManager.team2, player) > -1 && isTeam1 == false)
            {
                playerManager.team2score += 7;
                playerManager.Reset();
            }
        }
        else if(other.gameObject.GetComponent<Player>() != null)
        {
            if (Array.IndexOf(playerManager.team1, player) > -1 && isTeam1 == true)
            {
                other.gameObject.GetComponent<Player>().inEndzone = true;
            }
            if (Array.IndexOf(playerManager.team2, player) > -1 && isTeam1 == false)
            {
                other.gameObject.GetComponent<Player>().inEndzone = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
        {
            other.gameObject.GetComponent<Player>().inEndzone = false;
        }
    }
}

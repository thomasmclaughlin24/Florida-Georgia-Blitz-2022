using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPlayer : Player
{
    public Transform football;
    public NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pm.playerWithBall == null)
        {
            agent.destination = football.position;
        }
        else if(pm.playerWithBall.teamname == teamname && pm.playerWithBall != this)
        {
            float myDistance = Vector3.Distance(pm.playerWithBall.gameObject.transform.position, gameObject.transform.position);
            Player[] otherTeam = pm.team1;
            Transform myGoal = pm.team2Goal;
            if(teamname == otherTeam[0].teamname)
            {
                otherTeam = pm.team2;
                myGoal = pm.team1Goal;
            }
            bool runForThrow = false;
            Player closest = null;
            float closestDist = 1000f;
            foreach (Player p in otherTeam)
            {
                float otherDist = Vector3.Distance(pm.playerWithBall.gameObject.transform.position, p.gameObject.transform.position);
                if (myDistance > otherDist)
                {
                    runForThrow = true;
                }
                if(otherDist < closestDist)
                {
                    closest = p;
                    closestDist = otherDist;
                }
            }
            if(runForThrow == true)
            {
                agent.destination = myGoal.position;
            }
            else
            {
                agent.destination = closest.gameObject.transform.position;
            }
        }
    }
}

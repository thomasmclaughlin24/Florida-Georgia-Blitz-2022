using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPlayer : Player
{
    public Transform football;
    public NavMeshAgent agent;
    public float tackledist = 5f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stopped)
        {
            agent.velocity = Vector3.zero;
            agent.destination = transform.position;
        }
        float speed = (new Vector3(agent.velocity.x, 0f, agent.velocity.z)).magnitude;
        animator.SetFloat("Speed", speed);
        animator.SetFloat("MotionSpeed", 1f);
        if(pm.playerWithBall == null)
        {
            agent.destination = football.position;
        }
        else if(pm.playerWithBall.teamname == teamname && pm.playerWithBall != this)
        {
            float myDistance = Vector3.Distance(pm.playerWithBall.gameObject.transform.position, gameObject.transform.position);
            Player[] otherTeam = pm.team1;
            Transform myGoal = pm.Team2Touchdown.transform;
            if(teamname == otherTeam[0].teamname)
            {
                otherTeam = pm.team2;
                myGoal = pm.Team1Touchdown.transform;
            }
            bool runForThrow = true;
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
        else if(pm.playerWithBall == this)
        {
            Player[] otherTeam = pm.team1;
            Transform myGoal = pm.team2Goal;
            Player[] myTeam = pm.team2;
            Player closestfriend = null;
            Player closestfoe = null;
            float closestDist = 1000f;
            float foeClosestDist = 1000f;
            if (teamname == otherTeam[0].teamname)
            {
                otherTeam = pm.team2;
                myGoal = pm.team1Goal;
                myTeam = pm.team1;
            }
            foreach (Player p in otherTeam)
            {
                float otherDist = Vector3.Distance(gameObject.transform.position, p.gameObject.transform.position);
                if (otherDist < foeClosestDist)
                {
                    closestfoe = p;
                    foeClosestDist = otherDist;
                }
            }
            foreach (Player p in myTeam)
            {
                float otherDist = Vector3.Distance(myGoal.position, p.gameObject.transform.position);
                if (otherDist < closestDist && p.gameObject != gameObject && Vector3.Distance(transform.position, p.gameObject.transform.position) > 5 && Vector3.Distance(transform.position, p.gameObject.transform.position) < 22)
                {
                    closestfriend = p;
                    closestDist = otherDist;
                }
            }
            if(closestfriend != null && closestDist < Vector3.Distance(gameObject.transform.position, myGoal.position))
            {
                transform.LookAt(closestfriend.gameObject.transform);
                GetComponent<AIthrow>().Throw(closestfriend.gameObject.transform);
            }
            else
            {
                agent.destination = myGoal.position;
            }
        }
        else if(pm.playerWithBall.teamname != teamname)
        {
            agent.destination = pm.playerWithBall.transform.position;
            float aidistanceaway = Vector3.Distance(gameObject.transform.position, pm.playerWithBall.transform.position);
            if(aidistanceaway < tackledist)
            {
                Tackle();
            }
        }
    }

    private void Tackle()
    {
        tackling = true;
        animator.SetBool("Tackle", true);
        float tacklingend = animator.GetCurrentAnimatorStateInfo(0).length;
        StartCoroutine(StopTackle(tacklingend));
    }

    IEnumerator StopTackle(float delay)
    {
        yield return new WaitForSeconds(delay);
        tackling = false;
        animator.SetBool("Tackle", false);
        hitsomeone = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPlayer : Player
{
    private GameManager gm;
    public Transform football;
    public NavMeshAgent agent;
    public float tackledist = 5f;
    private Player[] otherTeam;
    private Player[] myTeam;
    private Transform myGoal;
    private bool onOffense = false;
    private Vector3 startpos;
    public float catchdist = 10f;

    // Start is called before the first frame update
    void Start()
    {
        startpos = GetComponent<ResetPos>().startpos;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        otherTeam = pm.team1;
        myTeam = pm.team2;
        myGoal = pm.Team2Touchdown.transform;
        if (teamname == otherTeam[0].teamname)
        {
            otherTeam = pm.team2;
            myTeam = pm.team1;
            myGoal = pm.Team1Touchdown.transform;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.paused)
        {
            agent.velocity = Vector3.zero;
            agent.destination = transform.position;
        }
        else
        {
            float speed = (new Vector3(agent.velocity.x, 0f, agent.velocity.z)).magnitude;
            animator.SetFloat("Speed", speed);
            animator.SetFloat("MotionSpeed", 1f);
            if (pm.currentplay == "Run")
            {
                RunPlay();
            }
            else if(pm.currentplay == "Short Pass" || pm.currentplay == "Medium Pass" || pm.currentplay == "Deep Pass")
            {
                PassPlay();
            }
        }
        startpos = new Vector3(startpos.x, startpos.y, transform.position.z);
        if (stopped)
        {
            agent.velocity = Vector3.zero;
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

    IEnumerator StopCatch(float delay)
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Catch", false);
        Debug.Log("Catch Stopping");
    }

    public override void StartPlay()
    {
        if (pm.playerWithBall && pm.playerWithBall.teamname == teamname)
        {
            onOffense = true;
        }
        else
        {
            onOffense = false;
        }
        if (pm.currentplay == "Run")
        {
            if(onOffense == true)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, startPlayZ);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, startPlayZ - 5);
            }
        }
        if (pm.currentplay == "Short Pass")
        {
            if(onOffense == true)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, startPlayZ);
                if (id == 1)
                {
                    agent.destination = new Vector3(transform.position.x + 5f, transform.position.y, transform.position.z + 10f);
                }
                if (id == 2)
                {
                    agent.destination = new Vector3(transform.position.x - 10f, transform.position.y, transform.position.z + 15f);
                }
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, startPlayZ + 12.5f);
            }
            
        }
        if(pm.currentplay == "Medium Pass")
        {
            if (onOffense == true)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, startPlayZ);
                if (id == 1)
                {
                    agent.destination = new Vector3(transform.position.x + 5f, transform.position.y, transform.position.z + 20f);
                }
                if (id == 2)
                {
                    agent.destination = new Vector3(transform.position.x - 10f, transform.position.y, transform.position.z + 20f);
                }
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, startPlayZ + 15f);
            }
        }
        if (pm.currentplay == "Deep Pass")
        {
            if (onOffense == true)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, startPlayZ);
                if (id == 1)
                {
                    agent.destination = new Vector3(transform.position.x + 5f, transform.position.y, transform.position.z + 30f);
                }
                if (id == 2)
                {
                    agent.destination = new Vector3(transform.position.x - 10f, transform.position.y, transform.position.z + 30f);
                }
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, startPlayZ + 20f);
            }
        }
    }

    private void RunPlay()
    {
        if(pm.playerWithBall == null)
        {
            RunForBall();
        }
        else if(onOffense == true)
        {
            if (pm.playerWithBall.teamname == teamname && pm.playerWithBall != this)
            {
                float myDistance = Vector3.Distance(pm.playerWithBall.gameObject.transform.position, gameObject.transform.position);
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
                    if (otherDist < closestDist)
                    {
                        closest = p;
                        closestDist = otherDist;
                    }
                }
                if (runForThrow == true)
                {
                    agent.destination = myGoal.position;
                }
                else
                {
                    agent.destination = closest.gameObject.transform.position;
                }
            }
            
        }
        else
        {
            if (pm.playerWithBall == this)
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
                if (closestfriend != null && closestDist < Vector3.Distance(gameObject.transform.position, myGoal.position))
                {
                    transform.LookAt(closestfriend.gameObject.transform);
                    GetComponent<AIthrow>().Throw(closestfriend.gameObject.transform);
                }
                else
                {
                    agent.destination = myGoal.position;
                }
            }
            else if (pm.playerWithBall.teamname != teamname)
            {
                agent.destination = pm.playerWithBall.transform.position;
                float aidistanceaway = Vector3.Distance(gameObject.transform.position, pm.playerWithBall.transform.position);
                if (aidistanceaway < tackledist)
                {
                    Tackle();
                }
            }
        }
    }

    private void PassPlay()
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
        if (onOffense == true)
        {
            if (pm.playerWithBall == null && Vector3.Distance(transform.position, football.transform.position) <= catchdist)
            {
                Debug.Log("Catching Activated");
                animator.SetBool("Catch", true);
                float catchingend = animator.GetCurrentAnimatorStateInfo(0).length;
                StartCoroutine(StopCatch(catchingend));
                GetComponent<Pickup>().PickupBall();
            }
            if (id == 0)
            {

            }
            if(id == 1)
            {
                if(pm.playerWithBall == this)
                {
                    agent.destination = myGoal.position;
                }
            }
            if(id == 2)
            {
                if (pm.playerWithBall == this)
                {
                    agent.destination = myGoal.position;
                }
            }
        }
        else
        {
            agent.destination = otherTeam[id].transform.position;
            if(pm.playerWithBall == otherTeam[id])
            {
                float aidistanceaway = Vector3.Distance(gameObject.transform.position, pm.playerWithBall.transform.position);
                if (aidistanceaway < tackledist)
                {
                    Tackle();
                }
            }
        }
    }

    private void RunForBall()
    {
        agent.destination = football.position;
    }
}

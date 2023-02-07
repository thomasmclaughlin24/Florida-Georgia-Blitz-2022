using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPlayer : Player
{
    public Transform football;
    public NavMeshAgent agent;
    public float tackledist = 5f;
    private Player[] otherTeam;
    private Player[] myTeam;
    private Transform myGoal;
    public float catchdist = 10f;
    private float timeOfLastReset = 0f;
    public float throwDist = 10f;
    private float timeOfLastThrow = 0f;
    public Vector3 staticdest;
    private bool started = false;
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position;
        startrot = transform.rotation;
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
        else if(started == true)
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
            if(onOffense && pm.playerWithBall && pm.playerWithBall.teamname != teamname)
            {
                pm.Reset();
            }
        }
        //startpos = new Vector3(startpos.x, startpos.y, transform.position.z);
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

        if (pm.currentplay == "Run")
        {
            if (teamname == "Blue")
            {
                if (onOffense == true)
                {
                    startPlayZ = -20;
                }
                else
                {
                    startPlayZ = 15;
                }
            }
            if(teamname == "Red")
            {
                if (onOffense == true)
                {
                    startPlayZ = 20;
                }
                else
                {
                    startPlayZ = -15;
                }
            }
            Reset();
        }
        if (pm.currentplay == "Short Pass")
        {
            if(onOffense == true)
            {
                if(teamname == "Blue")
                {
                    startPlayZ = -20;
                    Reset();
                    if (id == 1)
                    {
                        staticdest = new Vector3(transform.position.x + 5f, transform.position.y, startPlayZ + 10f);
                        Debug.Log("ID 1: " + staticdest);
                    }
                    if (id == 2)
                    {
                        staticdest = new Vector3(transform.position.x - 10f, transform.position.y, startPlayZ + 15f);
                        Debug.Log("ID 2: " + staticdest);
                    }
                }
                else if(teamname == "Red")
                {
                    startPlayZ = 20;
                    Reset();
                    if (id == 1)
                    {
                        staticdest = new Vector3(transform.position.x + 5f, transform.position.y, startPlayZ - 10f);
                    }
                    if (id == 2)
                    {
                        staticdest = new Vector3(transform.position.x - 10f, transform.position.y, startPlayZ - 15f);
                    }
                }
            }
            else
            {
                if(teamname == "Blue")
                {
                    startPlayZ = 15;
                }
                else if(teamname == "Red")
                {
                    startPlayZ = -15;
                }
                Reset();
            }
        }
        if(pm.currentplay == "Medium Pass")
        {
            if (onOffense == true)
            {
                if (teamname == "Blue")
                {
                    startPlayZ = -20;
                    Reset();
                    if (id == 1)
                    {
                        staticdest = new Vector3(transform.position.x + 5f, transform.position.y, startPlayZ + 20f);
                    }
                    if (id == 2)
                    {
                        staticdest = new Vector3(transform.position.x - 10f, transform.position.y, startPlayZ + 25f);
                    }
                }
                else if (teamname == "Red")
                {
                    startPlayZ = 20;
                    Reset();
                    if (id == 1)
                    {
                        staticdest = new Vector3(transform.position.x + 5f, transform.position.y, startPlayZ - 20f);
                    }
                    if (id == 2)
                    {
                        staticdest = new Vector3(transform.position.x - 10f, transform.position.y, startPlayZ - 25f);
                    }
                }
            }
            else
            {
                if (teamname == "Blue")
                {
                    startPlayZ = 15;
                }
                else if (teamname == "Red")
                {
                    startPlayZ = -15;
                }
                Reset();
            }
        }
        if (pm.currentplay == "Deep Pass")
        {
            if (onOffense == true)
            {
                if (teamname == "Blue")
                {
                    startPlayZ = -20;
                    Reset();
                    if (id == 1)
                    {
                        staticdest = new Vector3(transform.position.x + 5f, transform.position.y, startPlayZ + 30f);
                    }
                    if (id == 2)
                    {
                        staticdest = new Vector3(transform.position.x - 10f, transform.position.y, startPlayZ + 35f);
                    }
                }
                else if (teamname == "Red")
                {
                    startPlayZ = 20;
                    Reset();
                    if (id == 1)
                    {
                        staticdest = new Vector3(transform.position.x + 5f, transform.position.y, startPlayZ - 30f);
                    }
                    if (id == 2)
                    {
                        staticdest = new Vector3(transform.position.x - 10f, transform.position.y, startPlayZ - 35f);
                    }
                }
            }
            else
            {
                if (teamname == "Blue")
                {
                    startPlayZ = 15;
                }
                else if (teamname == "Red")
                {
                    startPlayZ = -15;
                }
                Reset();
            }
        }
        timeOfLastReset = Time.time;
        started = true;
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
                    timeOfLastThrow = Time.time;
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
        float closestDist = 1000f;
        if (teamname == otherTeam[0].teamname)
        {
            otherTeam = pm.team2;
            myGoal = pm.team1Goal;
            myTeam = pm.team1;
        }
        if (onOffense == true)
        {
            if (pm.playerWithBall == null && Vector3.Distance(transform.position, football.transform.position) <= catchdist && Time.time - timeOfLastThrow > 1f)
            {
                Debug.Log("Catching Activated");
                animator.SetBool("Catch", true);
                float catchingend = animator.GetCurrentAnimatorStateInfo(0).length;
                StartCoroutine(StopCatch(catchingend));
                GetComponent<Pickup>().PickupBall();
            }
            if (pm.playerWithBall == this)
            {
                if(Time.time - timeOfLastReset >= 1)
                {
                    Player ldf = null;
                    float ldfDist = -1000f;
                    Player myClosestFoe = null;
                    foreach (Player friend in myTeam)
                    {
                        Player closestfoe = null;
                        float foeClosestDist = 1000f;
                        foreach (Player foe in otherTeam)
                        {
                            float dist = Vector3.Distance(friend.gameObject.transform.position, foe.gameObject.transform.position);
                            if (dist < foeClosestDist)
                            {
                                closestfoe = foe;
                                foeClosestDist = dist;
                            }
                        }
                        if (foeClosestDist > ldfDist && Mathf.Abs(foeClosestDist - ldfDist) >= 1f && Vector3.Distance(transform.position, myGoal.position) >= Vector3.Distance(friend.transform.position, myGoal.position))
                        {
                            ldf = friend;
                            ldfDist = foeClosestDist;
                        }
                        if(friend == this)
                        {
                            myClosestFoe = closestfoe;
                        }
                    }
                    if(ldf == this)
                    {
                        var zmodifier = 1;
                        if(transform.position.z < myClosestFoe.transform.position.z)
                        {
                            zmodifier = -1;
                        }
                        agent.destination = new Vector3(myGoal.position.x, myGoal.position.y, myGoal.position.z + 10f*zmodifier);
                    }
                    else
                    {
                        agent.destination = ldf.transform.position;
                        if (Vector3.Distance(this.transform.position, ldf.transform.position) <= throwDist)
                        {
                            transform.LookAt(ldf.gameObject.transform);
                            GetComponent<AIthrow>().Throw(ldf.gameObject.transform);
                            timeOfLastThrow = Time.time;
                        }
                    }
                }
            }
            else
            {
                agent.destination = staticdest;
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
        if (football.gameObject.GetComponent<Rigidbody>().velocity.y <= 0)
        {
            agent.destination = football.gameObject.GetComponent<Football>().getTarget();
        }
    }
}

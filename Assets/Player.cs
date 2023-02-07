using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class Player : MonoBehaviour
{
    public string teamname = "Blue";
    internal bool hasball = false;
    public PlayerManager pm;
    internal bool inEndzone = false;
    public bool tackling = false;
    internal Animator animator;
    internal bool stopped = false;
    internal bool hitsomeone = false;
    public int startPlayZ = 20;
    private ThirdPersonController tpc;
    internal int id = 0;
    internal bool onOffense;
    internal Vector3 startpos;
    internal Quaternion startrot;
    internal GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        tpc = GetComponent<ThirdPersonController>();
        startpos = transform.position;
        startrot = transform.rotation;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tpc)
        {
            tpc.stopmove = stopped;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Player otherPlayer = other.GetComponent<Player>();
        if (otherPlayer)
        {
            if(otherPlayer.tackling == true && otherPlayer.hitsomeone == false)
            {
                otherPlayer.hitsomeone = true;
                if(pm.playerWithBall == this)
                {
                    hasball = false;
                    pm.playerWithBall = null;
                    pm.football.transform.parent = null;
                    pm.football.GetComponent<Rigidbody>().isKinematic = false;
                    gm.paused = true;
                }
                animator.SetBool("Fall", true);
                stopped = true;
                StartCoroutine(StopFall());
            }
        }
    }

    IEnumerator StopFall()
    {
        float delay = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(delay);
        animator.SetBool("Fall", false);
        if(gm.paused == true)
        {
            pm.Reset();
        }
    }

    void GetUp()
    {
        stopped = false;
    }

    public virtual void StartPlay()
    {
        if (onOffense == true)
        {
            startPlayZ = -20;
        }
        else
        {
            startPlayZ = 15;
        }
        StartCoroutine(ForceMove(new Vector3(transform.position.x, transform.position.y, startPlayZ)));
    }

    IEnumerator ForceMove(Vector3 position)
    {
        var tpc = GetComponent<ThirdPersonController>();
        var cc = GetComponent<CharacterController>();
        if (tpc)
        {
            tpc.enabled = false;
            cc.enabled = false;
        }
        yield return null;
        transform.position = position;
        yield return null;
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        if (tpc)
        {
            tpc.enabled = true;
            cc.enabled = true;
        }
    }

    public void Reset()
    {
        StartCoroutine("resetCoRoutine");
    }

    IEnumerator resetCoRoutine()
    {
        var tpc = GetComponent<ThirdPersonController>();
        var cc = GetComponent<CharacterController>();
        var ai = GetComponent<AIPlayer>();
        if (tpc)
        {
            tpc.enabled = false;
            cc.enabled = false;
        }
        yield return null;
        transform.position = new Vector3(startpos.x, startpos.y, startPlayZ);
        transform.rotation = startrot;
        yield return null;
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        if (tpc)
        {
            tpc.enabled = true;
            cc.enabled = true;
        }
        if (ai)
        {
            ai.agent.destination = startpos;
        }
    }
}

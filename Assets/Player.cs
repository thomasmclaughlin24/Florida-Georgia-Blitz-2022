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
    public int startPlayZ = 22;
    private ThirdPersonController tpc;
    internal int id = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        tpc = GetComponent<ThirdPersonController>();
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
    }

    void GetUp()
    {
        stopped = false;
    }

    public virtual void StartPlay()
    {

    }
}

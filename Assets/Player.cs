using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Player otherPlayer = other.GetComponent<Player>();
        if (otherPlayer)
        {
            Debug.Log("Collided");
            if(otherPlayer.tackling == true && otherPlayer.hitsomeone == false)
            {
                otherPlayer.hitsomeone = true;
                Debug.Log("I'm hit!");
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
}

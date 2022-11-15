using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class ResetPos : MonoBehaviour
{
    internal Vector3 startpos;
    internal Quaternion startrot;
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position;
        startrot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -1)
        {
            Reset();
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
        transform.position = startpos;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class ResetPos : MonoBehaviour
{
    private Vector3 startpos;
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position;
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
        if (tpc)
        {
            tpc.enabled = false;
            cc.enabled = false;
        }
        yield return null;
        transform.position = startpos;
        yield return null;
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            Debug.Log("Something");
        }
        if (tpc)
        {
            tpc.enabled = true;
            cc.enabled = true;
        }
    }
}

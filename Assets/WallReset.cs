using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallReset : MonoBehaviour
{
    public GameObject football;
    public PlayerManager pm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == football)
        {
            football.GetComponent<ResetPos>().Reset();
            football.GetComponent<Rigidbody>().isKinematic = false;
            football.transform.parent = null;
            pm.playerWithBall.hasball = false;
            pm.playerWithBall = null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBall : MonoBehaviour
{
    public PlayerManager pm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision other)
    {
        if(other.gameObject == pm.football)
        {
            pm.footballGrounded = true;
        }
    }
}

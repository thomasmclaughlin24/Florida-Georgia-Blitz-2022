using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowMeter : MonoBehaviour
{
    public Throwing throwing;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(1, throwing.throwforce / 800, 1);
        //transform.position = new Vector3(transform.position.x, transform.parent.position.y - throwing.throwforce / 1600, transform.position.z);
    }
}

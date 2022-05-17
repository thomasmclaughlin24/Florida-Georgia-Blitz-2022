using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public GameObject player;
    public GameObject football;
    public GameObject slot;
    public float range = 0;
    //internal bool holding = false;
    private Player PlayerInfo;
    // Start is called before the first frame update
    void Start()
    {
        PlayerInfo = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(football.transform.position, player.transform.position) < range && !PlayerInfo.hasball){
		    football.transform.SetParent(slot.transform);
		    football.transform.position = slot.transform.position;
		    football.GetComponent<Rigidbody>().isKinematic = true;
            football.transform.localEulerAngles = new Vector3(-74, -29, 38);
            PlayerInfo.pm.playerWithBall = PlayerInfo;
            PlayerInfo.hasball = true;
	    }
    }
}

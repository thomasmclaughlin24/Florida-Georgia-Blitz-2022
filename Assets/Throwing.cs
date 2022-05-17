using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Throwing : MonoBehaviour
{
    private Player PlayerInfo;
	public float throwforce;
	public Transform player;
    private bool MouseDown = false;
    private bool increase = true;
    public GameObject football;
    // Start is called before the first frame update
    void Start()
    {
        PlayerInfo = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
	    if(Mouse.current.leftButton.isPressed && PlayerInfo.hasball == true){
            MouseDown = true;
            if (increase)
            {
                if (throwforce < 800)
                {
                    throwforce += 1;
                }
                if(throwforce == 800)
                {
                    increase = false;
                }
            }
            else
            {
                if (throwforce > 0)
                {
                    throwforce--;
                }
                if(throwforce == 0)
                {
                    increase = true;
                }
            }
	    }
        else if(PlayerInfo.hasball == true && MouseDown)
        {
            football.GetComponent<Rigidbody>().isKinematic = false;
            football.transform.parent = null;
            football.GetComponent<Rigidbody>().AddForce(player.forward.normalized * throwforce + Vector3.up * throwforce);
            PlayerInfo.hasball = false;
            PlayerInfo.pm.playerWithBall = null;
            throwforce = 0;
            MouseDown = false;
        }
    }
}

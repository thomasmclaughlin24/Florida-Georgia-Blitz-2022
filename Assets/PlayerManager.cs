using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class PlayerManager : MonoBehaviour
{
    public Player[] team1;
    public Player[] team2;
    public Player playerWithBall = null;
    public Transform team1Goal;
    public Transform team2Goal;
    public GameObject Team1Touchdown;
    public GameObject Team2Touchdown;
    public int team1score = 0;
    public int team2score = 0;
    private Vector3[] team1startpos;
    private Vector3[] team2startpos;
    public GameObject football;
    private Vector3 footballstartpos;
    internal float TimeSinceThrow = 0f;
    internal string currentplay = "Run";
    public bool footballGrounded = false;
    // Start is called before the first frame update
    void Start()
    {
        team1startpos = new Vector3[team1.Length];
        team2startpos = new Vector3[team2.Length];
        for(int i = 0; i<team1.Length; i++)
        {
            team1startpos[i] = team1[i].gameObject.transform.position;
            team2startpos[i] = team2[i].gameObject.transform.position;
            footballstartpos = football.transform.position;
            team1[i].id = i;
            team2[i].id = i;
        }
        
    }

    // Update is called once per frame
    void Awake()
    {
        
    }

    public void Reset()
    {
        football.transform.position = footballstartpos;
        football.GetComponent<Rigidbody>().isKinematic = false;
        football.transform.parent = null;
        playerWithBall.hasball = false;
        playerWithBall = null;
        for (int i = 0; i < team1.Length; i++)
        {
            ResetPlayer(team1, team1startpos, i);
            ResetPlayer(team2, team2startpos, i);
        }
        //throwforce = 0;
        //MouseDown = false;
    }

    private void ResetPlayer(Player[] team, Vector3[] teampos, int i)
    {
        var reset = team[i].gameObject.GetComponent<ResetPos>();
        if (reset)
        {
            reset.Reset();
        }
        else
        {
            team[i].gameObject.transform.position = teampos[i];
        }
    }

    public void SetPlay(string play)
    {
        currentplay = play;
        Debug.Log(play);
    }

    public void StartPlay()
    {
        team1[0].GetComponent<Pickup>().PickupBall();
        playerWithBall = team1[0];
        for (int i = 0; i < team1.Length; i++)
        {
            team1[i].StartPlay();
            team2[i].StartPlay();
        }
    }
}

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
    private bool Team1Offense = true;
    public GameObject playMenu;
    internal GameManager gm;
    public AudioSource floridaSource;
    public AudioSource georgiaSource;
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
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
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
        if (playerWithBall)
        {
            playerWithBall.hasball = false;
            playerWithBall = null;
        }
        Team1Offense = !Team1Offense;
        for (int i = 0; i < team1.Length; i++)
        {
            ResetPlayer(team1, team1startpos, i, Team1Offense);
            ResetPlayer(team2, team2startpos, i, !Team1Offense);
        }
        gm.paused = true;
        playMenu.SetActive(true);
        //throwforce = 0;
        //MouseDown = false;
    }

    private void ResetPlayer(Player[] team, Vector3[] teampos, int i, bool onOffense)
    {
        /*var reset = team[i].gameObject.GetComponent<ResetPos>();
        if (reset)
        {
            reset.Reset();
        }
        else
        {
            team[i].gameObject.transform.position = teampos[i];
        }*/
        team[i].Reset();
        team[i].onOffense = onOffense;
    }

    public void SetPlay(string play)
    {
        currentplay = play;
        Debug.Log(play);
    }

    public void StartPlay()
    {
        floridaSource.Stop();
        georgiaSource.Stop();
        if (playerWithBall)
        {
            playerWithBall.hasball = false;
            playerWithBall = null;
        }
        if (Team1Offense == true)
        {
            team1[0].GetComponent<Pickup>().PickupBall();
            playerWithBall = team1[0];
        }
        else
        {
            team2[0].GetComponent<Pickup>().PickupBall();
            playerWithBall = team2[0];
        }
        for (int i = 0; i < team1.Length; i++)
        {
            team1[i].onOffense = Team1Offense;
            team2[i].onOffense = !Team1Offense;
            team1[i].StartPlay();
            team2[i].StartPlay();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimeControl : MonoBehaviour
{
    TextMeshProUGUI timetext;
    float starttime;
    public float totaltime = 300;
    private float timeleft;
    private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        timetext = GetComponent<TextMeshProUGUI>();
        starttime = Time.time;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        timeleft = totaltime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.paused)
        {
            timeleft -= Time.deltaTime;
            timetext.text = (int)(timeleft / 60) + ":" + ((int)(timeleft % 60)).ToString("00");
            if (timeleft <= 0)
            {
                SceneManager.LoadScene("Game Over");
            }
        }
    }
}

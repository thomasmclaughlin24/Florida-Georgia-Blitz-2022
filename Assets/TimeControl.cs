using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeControl : MonoBehaviour
{
    TextMeshProUGUI timetext;
    float starttime;
    public float totaltime = 300;
    // Start is called before the first frame update
    void Start()
    {
        timetext = GetComponent<TextMeshProUGUI>();
        starttime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        var timeleft = totaltime - (Time.time - starttime);
        timetext.text = (int)(timeleft / 60) + ":" + (int)(timeleft % 60);
    }
}

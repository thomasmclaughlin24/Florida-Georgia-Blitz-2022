using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreTextUpdate : MonoBehaviour
{
    public PlayerManager pm;
    public bool isTeam1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var text = GetComponent<TextMeshProUGUI>();
        if(isTeam1 == true)
        {
            text.text = pm.team1score.ToString();
        }
        else
        {
            text.text = pm.team2score.ToString();
        }
    }
}

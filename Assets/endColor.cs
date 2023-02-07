using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class endColor : MonoBehaviour
{
    private int redScore;
    private int blueScore;
    public Color blueWinColor;
    public Color redWinColor;
    public Color tieColor;
    public AudioSource FloridaSource;
    public AudioSource GeorgiaSource;
    // Start is called before the first frame update
    void Start()
    {
        redScore = PlayerPrefs.GetInt("redscore");
        blueScore = PlayerPrefs.GetInt("bluescore");
        if(blueScore > redScore)
        {
            GetComponent<Image>().color = blueWinColor;
            FloridaSource.Play();
        }
        if (redScore > blueScore)
        {
            GetComponent<Image>().color = redWinColor;
            GeorgiaSource.Play();
        }
        if (blueScore == redScore)
        {
            GetComponent<Image>().color = tieColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

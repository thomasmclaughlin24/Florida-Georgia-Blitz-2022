using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScore : MonoBehaviour
{
    public bool Team1 = false;
    // Start is called before the first frame update
    void Start()
    {
        var text = GetComponent<TextMeshProUGUI>();
        if(Team1 == true)
        {
            text.text = PlayerPrefs.GetInt("bluescore").ToString();
        }
        else
        {
            text.text = PlayerPrefs.GetInt("redscore").ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

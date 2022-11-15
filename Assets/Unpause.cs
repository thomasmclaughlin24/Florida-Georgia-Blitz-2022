using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unpause : MonoBehaviour
{
    private GameManager gm;
    public GameObject PlayMenu;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UnPause()
    {
        if (gm.paused)
        {
            gm.paused = false;
            PlayMenu.SetActive(false);
        }
    }
}

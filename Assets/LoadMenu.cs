using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.InputSystem;

public class LoadMenu : MonoBehaviour
{
    public GameObject screen;
    private VideoPlayer vp;
    // Start is called before the first frame update
    void Start()
    {
        vp = GetComponent<VideoPlayer>();
        vp.loopPointReached += LoadScene;
    }

    void Update()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            LoadScene(vp);
            vp.Stop();
        }
    }

    // Update is called once per frame
    void LoadScene(VideoPlayer vidplay)
    {
        screen.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropdownTool : MonoBehaviour
{
    public PlayerManager pm;
    private TMP_Dropdown dropdown;
    // Start is called before the first frame update
    void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlay()
    {
        pm.SetPlay(dropdown.options[dropdown.value].text);
        Debug.Log(dropdown.options[dropdown.value].text);
    }
}

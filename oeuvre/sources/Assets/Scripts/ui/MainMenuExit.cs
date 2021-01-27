using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuExit : MonoBehaviour
{
    private ShareMyScore _shareMyScore;

    private void Start()
    {
        _shareMyScore = FindObjectOfType<ShareMyScore>();
    }

    void Update()
    { 
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_shareMyScore.IsPanelActive) { _shareMyScore.HidePanel(); } else { Application.Quit(); }
            }
        }
    }
}

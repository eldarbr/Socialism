using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;

    private bool _isPause;

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_isPause) { Unpause(); } else { Pause(); }

            }
        }
    }

    public void Pause()
    {
        _isPause = true;
        FindObjectOfType<AudioManager>().Play("Click");
        _pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Unpause()
    {
        _isPause = false;
        FindObjectOfType<AudioManager>().Play("Click");
        _pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }


    public void LeaveGame()
    {
        string gameMode = FindObjectOfType<WinOrLossConditionsSystem>()._gameType;
        int record = FindObjectOfType<WinOrLossConditionsSystem>().ParseRecord();
        if (record > PlayerPrefs.GetInt(gameMode, 0))
        {
            PlayerPrefs.SetInt(gameMode, record);
        }
        FindObjectOfType<ChangeScene>().LoadScene(0);
    }
}

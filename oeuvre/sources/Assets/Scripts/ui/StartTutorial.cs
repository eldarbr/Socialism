using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTutorial : MonoBehaviour
{
    [SerializeField] private GameObject _tutorialPanel;
    [SerializeField] private GameObject _pausePanel;

    void Start()
    {
        if (PlayerPrefs.GetInt("TutorialShown", 0) < 2)
        {
            ForceTutorial();
            PlayerPrefs.SetInt("TutorialShown", PlayerPrefs.GetInt("TutorialShown", 0) + 1);
        }   
    }

    public void TutorialFromPause()
    {
        _pausePanel.SetActive(false);
        ForceTutorial();
    }

    public void PauseTime()
    {
        Time.timeScale = 0f;
    }

    public void ForceTutorial()
    {
        _tutorialPanel.SetActive(true);
        PauseTime();
    }
}

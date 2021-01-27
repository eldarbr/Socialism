using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutrorialScript : MonoBehaviour
{
    [SerializeField] private Image _hand;
    [SerializeField] private Text _tutorialText;
    [SerializeField] private GameObject _tutorialPanel;

    private System.Action _onNextSlide;

    public void Next()
    {
        _onNextSlide?.Invoke();
    }

    void Start()
    {
        ZeroSlide();
    }

    private void ThirdSlide()
    {
        _tutorialPanel.SetActive(false);
        UnPauseTime();
        ResetTutorial();
    }

    private void SecondSlide()
    {
        _hand.rectTransform.localPosition = new Vector3(56f, 351.92f, 0f);

        _hand.GetComponent<LowFrequencySineOscillation>().MinValue = -_hand.GetComponent<LowFrequencySineOscillation>().MinValue;
        _tutorialText.text = "This is your score, how many days you rule the country.";

        _onNextSlide = () => ThirdSlide();
    }

    private void FirstSlide()
    {
        _hand.rectTransform.localPosition = new Vector3(10.9f, -347.4f, 0f);

        _hand.GetComponent<LowFrequencySineOscillation>().MinValue = -_hand.GetComponent<LowFrequencySineOscillation>().MinValue;
        _tutorialText.text = "These are actions that you can do as a ruler to improve country state. You can press on them to make a decision.";

        _onNextSlide = () => SecondSlide();
    }

    private void ZeroSlide()
    {
        _hand.rectTransform.localPosition = new Vector3(-217.1f, 351.92f, 0f);
        _tutorialText.text = "This is your attention points. You need them to choose actions. That amount will decrease with every decision you make and increase every game day.";
        _onNextSlide = () => FirstSlide();
    }

    public void UnPauseTime()
    {
        Time.timeScale = 1f;
    }

    private void ResetTutorial()
    {
        ZeroSlide();
        _onNextSlide = () => FirstSlide();
    }
}

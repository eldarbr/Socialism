using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertsSystem : MonoBehaviour
{
    public readonly Color POSITIVE_ALERT_COLOR = new Color(0.562115f, 0.8f, 0.3960784f);
    public readonly Color NEUTRAL_ALERT_COLOR = new Color(0.8f, 0.7898195f, 0.3960784f);
    public readonly Color NEGATIVE_ALERT_COLOR = new Color(0.8f, 0.4084508f, 0.3960784f);

    [SerializeField] private Image _alertsPanel;
    [SerializeField] private Text _alertText;

    [SerializeField] private AnimationCurve _alertShowCurveX;
    [SerializeField] private AnimationCurve _alertShowCurveY;
    [SerializeField] private float _alertShowSpeed;

    [SerializeField] private AnimationCurve _alertHideCurveX;
    [SerializeField] private AnimationCurve _alertHideCurveY;
    [SerializeField] private float _alertHideSpeed;

    public struct Alert
    {
        public string text;
        public Color bgColor;
        public float seconds;

        public Alert(string text, Color bgColor, float seconds)
        {
            this.text = text;
            this.bgColor = bgColor;
            this.seconds = seconds;
        }
    }

    private Queue<Alert> _alertsQueue;
    private bool _isBusy;

    private void Start()
    {
        _alertsQueue = new Queue<Alert>();
        
    }

    public void PushAlert(Alert alert)
    {
        if (_alertsQueue.Contains(alert)) return;

        if (!_isBusy)
            ShowAlert(alert);
        else
            _alertsQueue.Enqueue(alert);
    }

    private void ShowAlert(Alert alert)
    {
        FindObjectOfType<AudioManager>().Play("Accept");
        _isBusy = true;

        _alertsPanel.color = alert.bgColor;
        _alertText.text = alert.text;

        IEnumerator holdAlert()
        {
            yield return new WaitForSeconds(alert.seconds);
            StartCoroutine(ProceduralPopAnimations.LocalScaleTransform(_alertsPanel.transform, _alertHideCurveX,
                _alertHideCurveY, _alertHideSpeed, () => 
                {
                    if (_alertsQueue.Count > 0)
                        ShowAlert(_alertsQueue.Dequeue());
                    else
                        _isBusy = false;
                }));
        }

        StartCoroutine(ProceduralPopAnimations.LocalScaleTransform(_alertsPanel.transform, _alertShowCurveX, 
            _alertShowCurveY, _alertShowSpeed, () =>
            {
                StartCoroutine(holdAlert());
            }));
    }
}

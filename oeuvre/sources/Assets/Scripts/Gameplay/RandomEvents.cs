using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomEvents : MonoBehaviour
{
    [SerializeField] private Image _buffImage;
    [SerializeField] private AnimationCurve _buffFadeInCurve;
    [SerializeField] private AnimationCurve _buffFadeOutCurve;
    [SerializeField] private float _buffAnimationsSpeed;

    [SerializeField] private AlertsSystem _alertsSystem;
    [SerializeField] private AttributesDecreaseSystem _attributesDecreaseSystem;

    [SerializeField] private float _averageTimeDelta;
    [SerializeField] private float _averageTimeChange;

    [Header("Sprites")]
    [SerializeField] private Sprite _generalEconomy;
    [SerializeField] private Sprite _generalPeople;
    [SerializeField] private Sprite _generalDemocraticLoyalty;
    

    private System.Action[] _eventsList;

    void Start()
    {
        _eventsList = new System.Action[] 
        {
            () => OneAttributeEventTrendChange(new AlertsSystem.Alert("Economic crisis has began",
            _alertsSystem.NEGATIVE_ALERT_COLOR, 5.5f), new AlertsSystem.Alert("Economic crisis has ended",
                _alertsSystem.POSITIVE_ALERT_COLOR, 4f), _generalEconomy, 0, 0.00016f, 24f),
            () => OneAttributeEventTrendChange(new AlertsSystem.Alert("Economic growth has began",
            _alertsSystem.POSITIVE_ALERT_COLOR, 5.5f), new AlertsSystem.Alert("Economic growth has ended",
                _alertsSystem.NEUTRAL_ALERT_COLOR, 4f), _generalEconomy, 0, -0.00016f, 24f),
            () => OneAttributeEventTrendChange(new AlertsSystem.Alert("Epidemic has began",
            _alertsSystem.NEGATIVE_ALERT_COLOR, 5.5f), new AlertsSystem.Alert("Epidemic has ended",
                _alertsSystem.POSITIVE_ALERT_COLOR, 4f), _generalPeople, 1, 0.00016f, 24f),
            () => OneAttributeEventTrendChange(new AlertsSystem.Alert("Living standarts are improving",
            _alertsSystem.POSITIVE_ALERT_COLOR, 5.5f), new AlertsSystem.Alert("Living standarts have stopped improving",
                _alertsSystem.NEUTRAL_ALERT_COLOR, 4f), _generalPeople, 1, -0.00016f, 24f),
            () => OneAttributeEventTrendChange(new AlertsSystem.Alert("Citizens go on strikes",
            _alertsSystem.NEGATIVE_ALERT_COLOR, 5.5f), new AlertsSystem.Alert("Situation has stabilized",
                _alertsSystem.POSITIVE_ALERT_COLOR, 4f), _generalDemocraticLoyalty, 2, 0.00016f, 24f),
            () => OneAttributeEventTrendChange(new AlertsSystem.Alert("Your rating is rising",
            _alertsSystem.POSITIVE_ALERT_COLOR, 5.5f), new AlertsSystem.Alert("Your rating has stopped rising",
                _alertsSystem.NEUTRAL_ALERT_COLOR, 4f), _generalDemocraticLoyalty, 2, -0.00016f, 24f),
        };




        StartCoroutine(WaitThenDo(Random.Range(_averageTimeDelta - _averageTimeChange,
            _averageTimeDelta + _averageTimeChange), () => _eventsList[Random.Range(0, _eventsList.Length)].Invoke()));
    }

    private void OneAttributeEventTrendChange(AlertsSystem.Alert startAlert, AlertsSystem.Alert endAlert,
        Sprite buffIcon, int changeFactorId, float changeFactorValue, float time)
    {
        _alertsSystem.PushAlert(startAlert);

        _buffImage.overrideSprite = buffIcon;
        _buffImage.color = startAlert.bgColor;

        StartCoroutine(ProceduralPopAnimations.ImageFade(_buffImage, _buffFadeInCurve, _buffAnimationsSpeed));

        _attributesDecreaseSystem.AddtitionalChangeFactors[changeFactorId] = changeFactorValue;
        StartCoroutine(WaitThenDo(time, () =>
        {
            _attributesDecreaseSystem.AddtitionalChangeFactors[changeFactorId] = 0f;
            StartCoroutine(ProceduralPopAnimations.ImageFade(_buffImage, _buffFadeOutCurve, _buffAnimationsSpeed));
            _alertsSystem.PushAlert(endAlert);
            StartCoroutine(WaitThenDo(Random.Range(_averageTimeDelta - _averageTimeChange,
                _averageTimeDelta + _averageTimeChange), ()=> _eventsList[Random.Range(0, _eventsList.Length)].Invoke()));
        }));
    }


    private IEnumerator WaitThenDo(float seconds, System.Action action)
    {
        yield return new WaitForSeconds(seconds);
        action();
    }
}

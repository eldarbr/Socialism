using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class LevelLocker : MonoBehaviour
{
    [SerializeField] private GameObject _lockIcon;

    [SerializeField] private GameObject _lockPopup;
    [SerializeField] private AnimationCurve _popupShowX;
    [SerializeField] private AnimationCurve _popupShowY;
    [SerializeField] private float _showSpeed;
    [SerializeField] private AnimationCurve _popupHideX;
    [SerializeField] private AnimationCurve _popupHideY;
    [SerializeField] private float _HideSpeed;


    [SerializeField] private GameObject _popupTextContainer;
    [SerializeField] private string _popupTextPattern;


    [SerializeField] private StaticColorSettings _staticColorSettings;
    [System.Serializable]
    struct StaticColorSettings
    {
        [SerializeField, Range(0f, 1f)] public float saturation;
        [SerializeField, Range(0f, 1f)] public float value;
    }


    [SerializeField] private StaticShakeSettings _staticShakeSettings;

    [System.Serializable]
    struct StaticShakeSettings
    {
        [SerializeField, Range(0f, 10f)] public float movementsSpeed;
        [SerializeField, Range(0f, 100f)] public float movementsAmplitude;
        [SerializeField] public int numberOfMovements;
    }

    public void LockLevel(Color lockIconColor, uint scoreToUnlock)
    {
        _popupTextContainer.GetComponent<Text>().text = string.Format(_popupTextPattern, scoreToUnlock);
        _lockIcon.GetComponent<Image>().color = lockIconColor;
        _lockIcon.SetActive(true);
    }

    public void UnlockLevel()
    {
        _lockIcon.SetActive(false);
        HideLockPopup();
    }

    public void ShowLockPopup()
    {
        if (!_lockPopup.activeSelf)
        {
            _lockPopup.GetComponent<Image>().color = RandColor();
            _lockPopup.SetActive(true);
            StartCoroutine(ProceduralPopAnimations.LocalScaleTransform(_lockPopup.GetComponent<Image>().transform, _popupShowX, _popupShowY, _showSpeed));
        }
        else
        {
            AnimationCurve shakeCurveX = new AnimationCurve(ShakeCurve());
            AnimationCurve shakeCurveY = new AnimationCurve(new Keyframe(0f, _lockPopup.transform.localPosition.y), new Keyframe(1f, _lockPopup.transform.localPosition.y));
            Coroutine animatinng = StartCoroutine(ProceduralPopAnimations.LocalPositionTransform(_lockPopup.transform, shakeCurveX, shakeCurveY, 
                _staticShakeSettings.movementsSpeed));
        }
    }

    private Keyframe[] ShakeCurve()
    {
        Keyframe[] keyframes = new Keyframe[_staticShakeSettings.numberOfMovements + 2];
        float startPosition = _lockPopup.transform.localPosition.x;

        keyframes[0] = new Keyframe(0f, startPosition);

        float timeDelta = 1f / (_staticShakeSettings.numberOfMovements + 1);

        for (int i = 0; i < _staticShakeSettings.numberOfMovements; i++)
        {
            if (i % 2 == 0)
            {
                keyframes[i + 1] = new Keyframe(i * timeDelta, startPosition + _staticShakeSettings.movementsAmplitude);
            }
            else
            {
                keyframes[i + 1] = new Keyframe(i * timeDelta, startPosition - _staticShakeSettings.movementsAmplitude);
            }
        }

        keyframes[_staticShakeSettings.numberOfMovements + 1] = new Keyframe(1f, startPosition);

        return keyframes;
    }


    public void HideLockPopup()
    {
        if (_lockPopup.activeSelf)
        {
            StartCoroutine(ProceduralPopAnimations.LocalScaleTransform(_lockPopup.GetComponent<Image>().transform, _popupHideX, _popupHideY, _HideSpeed,
            () => _lockPopup.SetActive(false)));
        }
    }

    private Color RandColor() => Color.HSVToRGB(Random.Range(0f, 1f), _staticColorSettings.saturation, _staticColorSettings.value);
}

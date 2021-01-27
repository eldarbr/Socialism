using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowFrequencySineOscillation : MonoBehaviour
{
    public float MinValue
    {
        get { return _minValue; }
        set { _minValue = value; }
    }

    [SerializeField] private float _amplitude;
    [SerializeField] private float _speed;
    [SerializeField] private float _minValue;
    [SerializeField] private float _phase;

    [SerializeField] private RectTransform _rectTransform;

    private float _t;
    private Vector3 _startVector;

    void Start()
    {
        _startVector = _rectTransform.localScale;
    }

    
    void Update()
    {
        _rectTransform.localScale = _startVector * (_minValue + Mathf.Sin(_t + _phase) * _amplitude);
        _t += Time.unscaledDeltaTime * _speed;
    }
}

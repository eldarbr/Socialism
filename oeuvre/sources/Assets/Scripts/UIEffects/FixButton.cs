using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixButton : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    private void OnDisable()
    {
        _rectTransform.localScale = Vector3.one;
    }
}

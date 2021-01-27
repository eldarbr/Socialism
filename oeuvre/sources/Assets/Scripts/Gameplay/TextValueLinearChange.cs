using UnityEngine.UI;
using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Text))]
public class TextValueLinearChange : MonoBehaviour
{
    private WinOrLossConditionsSystem _WinOrLoss;

    public int Value
    {
        get
        {
            return int.Parse(_targetText.text);
        }

        set
        {
            _targetText.text = value.ToString();
        }
    }

    [SerializeField] private Text _targetText;
    [SerializeField] private int _delta;
    [SerializeField] private int _waiter;

    void Start()
    {
        _WinOrLoss = FindObjectOfType<WinOrLossConditionsSystem>();
        StartCoroutine(TicCoroutine());
    }

    private IEnumerator TicCoroutine()
    {
        while (!_WinOrLoss.isLost)
        {
            yield return new WaitForSeconds(_waiter);
            Value += _delta;
        }
    }


}

using UnityEngine;
using UnityEngine.UI;
using Unity.Mathematics;


public class AttributesDecreaseSystem : MonoBehaviour
{
    public float[] AddtitionalChangeFactors { get; set; }

    [HideInInspector] public bool gameStopped;

    [SerializeField] private Image[] _fillAttributes;
    [SerializeField] [Range(0.001f, 5f)] private float _moderationFactor;
    [SerializeField] private float _speedOfTrendChange;
    [SerializeField] [Range(0f, 1f)] private float _increaseTendentionChance;

    private float2[] _t;


    private WinOrLossConditionsSystem WinOrLoss;


    void Start()
    {
        WinOrLoss = FindObjectOfType<WinOrLossConditionsSystem>();

        AddtitionalChangeFactors = new float[_fillAttributes.Length];
        _t = new float2[_fillAttributes.Length];

        for (int i = 0; i < _fillAttributes.Length; i++)
        {
            _t[i] = new float2(UnityEngine.Random.Range(100f, 10000f), 0f);
        }

    }


    void FixedUpdate()
    {
        if (!WinOrLoss.isLost)
        {
            for (int i = 0; i < _fillAttributes.Length; i++)
            {
                _fillAttributes[i].fillAmount -= (noise.cnoise(_t[i]) + 1f - _increaseTendentionChance) * (_moderationFactor / 10000 + AddtitionalChangeFactors[i]);
                _t[i].x += Time.fixedDeltaTime * _speedOfTrendChange;
            }
        }
    }
}

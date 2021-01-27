using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinOrLossConditionsSystem : MonoBehaviour
{
    [SerializeField] private Image[] _fillAttributes;

    [SerializeField] public string _gameType;

    [SerializeField] private GameObject _losePanel;
    [SerializeField] private Text _days;

    [SerializeField] private Text _loseReason;
    [SerializeField] private string[] _idsToLoseReasons;

    private static int _gameplayIterations;

    [HideInInspector] public bool isLost;

    //private void Start()
    //{
    //    Lost(0);
    //}

    void Update()
    {
        if (isLost) { return; }
        for (int i = 0; i < _fillAttributes.Length; i++)
        {
            if (Mathf.Approximately(_fillAttributes[i].fillAmount, 0f))
            {
                Lost(i);
                break;
            }

        }
    }

    private void Lost(int attrId)
    {
        isLost = true;
        _gameplayIterations += 1;
        
        int daysScore = int.Parse(_days.text);

        if (daysScore > PlayerPrefs.GetInt(_gameType, 0))
        {
            RecordHandler(daysScore, _gameType);
            PlayerPrefs.SetInt(_gameType, daysScore);
        }
        
        _loseReason.text = _idsToLoseReasons[attrId];
        _losePanel.SetActive(true);

    }

    public int ParseRecord() => int.Parse(_days.text);
    

    private void RecordHandler(int score, string gameMode)
    {
        FindObjectOfType<ShareMyScore>().SetRecordParameters(score, gameMode, true);
        StartCoroutine(FindObjectOfType<ShareMyScore>().DelayedPanel(0.8f));
    }
}

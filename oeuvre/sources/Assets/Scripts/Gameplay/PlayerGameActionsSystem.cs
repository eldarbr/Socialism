using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerGameActionsSystem : MonoBehaviour
{
    [Header("Technical stuff")]
    [SerializeField] private TextValueLinearChange _attentionAmount;
    [SerializeField] private Image[] _fillImages;
    [SerializeField] private Text _nameText;
    [SerializeField] private Text _descriptionText;
    [SerializeField] private RectTransform _actionPanel;
    [SerializeField] private Image _rejectImage;
    [SerializeField] private Image _acceptImage;


    [System.Serializable]
    struct AnimationSettings
    {
        public AnimationCurve curve;
        public float speed;
    }

    [Header("Animations")]
    [SerializeField] private AnimationSettings _deflation;
    [SerializeField] private AnimationSettings _inflation;
    [SerializeField] private AnimationSettings _fadeIn;
    [SerializeField] private AnimationSettings _fadeOut;


    [Header("Gameplay")]
    [SerializeField] private byte _recentLenght;
    [SerializeField, Range(0.01f, 10f)] private float _alternativeRandomDumping;
    [SerializeField, Range(0f, 1f)] private float _randomizationDelta;
    [SerializeField] private int _actionAttentionPrice;
    [SerializeField] private GameAction[] _gameActions;
    [SerializeField] private int[] _firstAttrActions;
    [SerializeField] private int[] _secondAttrActions;
    [SerializeField] private int[] _thirdAttrActions;

    [System.Serializable]
    struct GameAction
    {
        public string name;
        public string description;
        [Range(-1f, 1f)] public float[] onAcceptChanges;
        [Range(-1f, 1f)] public float[] onRejectChanges; 
    }

    private GameAction _currentGameAction;
    private bool _isPanelEmpty;

    private int[] _recentAccions;


    public void CallAction(int actionTagId) // standart: 0-money 1-people 2-popularity
    {
        FindObjectOfType<AudioManager>().Play("Click");
        if (_attentionAmount.Value < _actionAttentionPrice)
            return;

        _attentionAmount.Value -= _actionAttentionPrice;

        switch (actionTagId)
        {
            case 0: SetActionActiveFrom(_firstAttrActions); break;
            case 1: SetActionActiveFrom(_secondAttrActions); break;
            case 2: SetActionActiveFrom(_thirdAttrActions); break;
            default:
                break;
        }

    }

    private void Start()
    {
        ActionsMemoryInit();
        _firstAttrActions = ShuffleActions(_firstAttrActions);
        _secondAttrActions = ShuffleActions(_secondAttrActions);
        _thirdAttrActions = ShuffleActions(_thirdAttrActions);
    }

    private void ActionsMemoryInit()
    {
        _recentAccions = new int[_recentLenght];
        for (byte i = 0; i < _recentLenght; i++)
        {
            _recentAccions[i] = -1;
        }
    }

    public void RecieveActionChoice(bool accept)
    {
        _isPanelEmpty = true;

        _rejectImage.GetComponent<Button>().interactable = false;
        _acceptImage.GetComponent<Button>().interactable = false;
        StartCoroutine(ProceduralPopAnimations.Pop(_actionPanel, 365f, 287f, _deflation.curve, _deflation.speed));
        StartCoroutine(ProceduralPopAnimations.ImageFade(_rejectImage, _fadeOut.curve, _fadeOut.speed));
        StartCoroutine(ProceduralPopAnimations.ImageFade(_acceptImage, _fadeOut.curve, _fadeOut.speed));

        if (accept)
        {
            FindObjectOfType<AudioManager>().Play("Accept");
            for (int i = 0; i < _fillImages.Length; i++)
            {
                float change = _currentGameAction.onAcceptChanges[i];
                if (change != 0f)
                    _fillImages[i].fillAmount += Random.Range(change - _randomizationDelta, change + _randomizationDelta);
            }
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("Reject");
            for (int i = 0; i < _fillImages.Length; i++)
            {
                float change = _currentGameAction.onRejectChanges[i];
                if (change != 0f)
                    _fillImages[i].fillAmount += Random.Range(change - _randomizationDelta, change + _randomizationDelta);
            }
        }

    }

    private void SetActionActiveFrom(int[] actionsIds)
    {
        if (!_isPanelEmpty)
            StartCoroutine(ProceduralPopAnimations.Pop(_actionPanel, 365f, 287f, _deflation.curve, _deflation.speed, () =>
            {
                ActivateNewPanel(actionsIds);
            }));
        else
            ActivateNewPanel(actionsIds);
    }

    private void ActivateNewPanel(int[] actionsIds)
    {
        _isPanelEmpty = false;

        _rejectImage.gameObject.SetActive(true);
        _acceptImage.gameObject.SetActive(true);
        _rejectImage.GetComponent<Button>().interactable = true;
        _acceptImage.GetComponent<Button>().interactable = true;


        _currentGameAction = _gameActions[RandomWMemory(actionsIds)];

        _nameText.text = _currentGameAction.name;
        _descriptionText.text = _currentGameAction.description;

        StartCoroutine(ProceduralPopAnimations.Pop(_actionPanel, 365f, 287f, _inflation.curve, _inflation.speed));
        StartCoroutine(ProceduralPopAnimations.ImageFade(_rejectImage, _fadeIn.curve, _fadeIn.speed));
        StartCoroutine(ProceduralPopAnimations.ImageFade(_acceptImage, _fadeIn.curve, _fadeIn.speed));
    }

    private int RandomWMemory(int[] actionsIds)
    {

        int currentId = AlternateRandom(actionsIds);
        while (IsActionInMemory(currentId))
        {
            currentId = AlternateRandom(actionsIds);
        }
        return currentId;
    }

    private bool IsActionInMemory(int actionId)
    {
        if (_recentAccions.Contains(actionId))
        {
            return true;
        }
        else
        {
            ActionToMemory(actionId);
            return false;
        }
    }

    private void ActionToMemory(int actionId)
    {
        for (byte i = (byte)(_recentLenght - 1); i > 0; i--)
        {
            _recentAccions[i] = _recentAccions[i - 1];
        }
        _recentAccions[0] = actionId;
    }

    private int AlternateRandom(int[] actionsIds)
    {
        float len = actionsIds.Length;
        int gen = (int)Mathf.Round(Mathf.Lerp(0f, len - 1, Mathf.Clamp01((float)(Math.Sqrt(_alternativeRandomDumping * len * Random.Range(0f, len / _alternativeRandomDumping) / len)))));
        int toReturn = actionsIds[gen];
        MoveRandomElement(actionsIds, gen);
        return toReturn;
    }

    private void MoveRandomElement(int[] actionsIds, int element)
    {
        int tmp = actionsIds[element];
        for (int i = element; i > 0; i--)
        {
            actionsIds[i] = actionsIds[i - 1];
        }
        actionsIds[0] = tmp;
    }

    private int[] ShuffleActions(int[] actions)
    {
        int ln = actions.Length;
        int[] shuffled = new int[ln];
        while (ln > 0)
        {
            int cursor = Random.Range(0, ln - 1);
            shuffled[actions.Length - ln] = actions[cursor];
            actions[cursor] = actions[ln - 1];
            ln--;
        }
        return shuffled;
    }
}

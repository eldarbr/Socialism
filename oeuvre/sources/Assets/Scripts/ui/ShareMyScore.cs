using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UI;

public class ShareMyScore : MonoBehaviour
{
    [SerializeField] private GameObject _share;
    [SerializeField] private GameObject _escape;
    [SerializeField, Range(0f, 1f)] private float _maxTransparency;
    [SerializeField] private AnimationCurve _open;
    [SerializeField] private AnimationCurve _close;
    [SerializeField] private float _speed;
    [SerializeField] private Text _shareTextContainer;
    [SerializeField] private string _sharePanelText;
    [SerializeField] private Text _shareButtonText;
    private string _gameMode;
    private int _record;
    private bool _inGame;
    [SerializeField] StaticShareColorSettings _staticShareColorSettings;
    

    [System.Serializable]
    struct StaticShareColorSettings
    {
        [SerializeField, Range(0f, 1f)] public float saturation;
        [SerializeField, Range(0f, 1f)] public float value;
    }

    [SerializeField] private string _shareSubject;
    [SerializeField] private string _shareText;
    [SerializeField] private Texture2D _shareImage;

    [HideInInspector] public bool IsPanelActive { get => _share.activeSelf; set {
            _escape.SetActive(value);
            _share.SetActive(value);
        } }


    public void ShowPanel()
    {
        IsPanelActive = true;

        Image image = _share.GetComponent<Image>();
        Color newColor = RandColor();
        image.color = newColor;
        _shareButtonText.color = newColor;

        
        if (!_inGame)
        {
            FindObjectOfType<AudioManager>().Play("Accept");
            StartCoroutine(ProceduralPopAnimations.ImageFade(_escape.GetComponent<Image>(), _open, _speed, null, _maxTransparency));
        }           
        StartCoroutine(ProceduralPopAnimations.LocalScaleTransform(_share.transform, _open, _open, _speed, () => Time.timeScale = 0f));

    }

    public void SetRecordParameters(int record, string gameMode, bool inGame = false)
    {
        _shareTextContainer.text = string.Format(_sharePanelText, record.ToString());
        _record = record;
        _gameMode = gameMode;
        _inGame = inGame;
        if (inGame) _escape = new GameObject();
    }


    private Color RandColor() => Color.HSVToRGB(Random.Range(0f, 1f), _staticShareColorSettings.saturation, _staticShareColorSettings.value);


    public void HidePanel()
    {
        FindObjectOfType<AudioManager>().Play("Reject");
        StartCoroutine(ProceduralPopAnimations.LocalScaleTransform(_share.transform, _close, _close, _speed, () => IsPanelActive = false));
        if (!_inGame) StartCoroutine(ProceduralPopAnimations.ImageFade(_escape.GetComponent<Image>(), _close, _speed, null, _maxTransparency));
    }

    public void SuperShare()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        SendShare();
    }


    public IEnumerator DelayedPanel(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ShowPanel();
    }


    public void SendShare(string subject = null, string text = null)
    {
        if (subject is null)
        {
            subject = this._shareSubject;
            text = this._shareText;
        }

        string filePath = Path.Combine(Application.temporaryCachePath, "shared_img.jpg");
        if (!System.IO.File.Exists(filePath)) { File.WriteAllBytes(filePath, this._shareImage.EncodeToJPG()); }

        new NativeShare().AddFile(filePath).SetSubject(subject).SetText(string.Format(text, _gameMode, _record))
        .Share();
    }
}

using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeReference] private Image _SFXControlImage;

    [SerializeReference] private Sprite _SFXOn;
    [SerializeReference] private Sprite _SFXOff;


    void Start()
    {
        InitSFX();        
    }

    private void InitSFX()
    {
        if (PlayerPrefs.GetInt("SFX_ON", 1) == 1)
        {
            AudioManager.instance.isSFXMuted = false;
            _SFXControlImage.overrideSprite = _SFXOn;
        }
        else
        {
            AudioManager.instance.isSFXMuted = true;
            _SFXControlImage.overrideSprite = _SFXOff;
        }
    }

    public void ChangeSFX()
    {
        if (AudioManager.instance.isSFXMuted)
        {
            AudioManager.instance.isSFXMuted = false;
            _SFXControlImage.overrideSprite = _SFXOn;
            AudioManager.instance.Play("Accept");
            PlayerPrefs.SetInt("SFX_ON", 1);
        }
        else
        {
            _SFXControlImage.overrideSprite = _SFXOff;
            AudioManager.instance.Play("Reject");
            AudioManager.instance.isSFXMuted = true;
            PlayerPrefs.SetInt("SFX_ON", 0);
        }
    }
}

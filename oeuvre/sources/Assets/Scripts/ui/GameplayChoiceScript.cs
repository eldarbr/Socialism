using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayChoiceScript : MonoBehaviour
{
    [SerializeField] private FractionsScenes[] _fractionsScenes;
    [SerializeField] private Text _fractionText;
    [SerializeField] private Text _record;

    private int _currentIndex;

    [System.Serializable]
    struct FractionsScenes
    {
        public string name;
        public Color color;
        public int sceneIndex;
        public bool isLocked;
        public uint scoreToUnlock;
        public int[] modeToUnlock;
    }

    void Start()
    {
        FillRecord();
        UnlockByRecords();
    }

    public void ChangeFraction(int change)
    {
        FindObjectOfType<AudioManager>().Play("Click");
        _currentIndex += change;

        if (_currentIndex < 0)
            _currentIndex = _fractionsScenes.Length - 1;
        if (_currentIndex == _fractionsScenes.Length)
            _currentIndex = 0;

        _fractionText.text = _fractionsScenes[_currentIndex].name;
        _fractionText.color = _fractionsScenes[_currentIndex].color;

        FillRecord();
        LockedOrNot();
    }

    public void LoadFraction()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        Time.timeScale = 1f;
        if (_fractionsScenes[_currentIndex].isLocked)
        {
            FindObjectOfType<LevelLocker>().ShowLockPopup();
        }
        else
        {
            SceneManager.LoadScene(_fractionsScenes[_currentIndex].sceneIndex);
        }
        
    }

    private void FillRecord()
    {
        string record = WhatIsMyRecord().ToString();
        _record.text = "Record: " + record;
        _record.color = _fractionsScenes[_currentIndex].color;

        FindObjectOfType<ShareMyScore>().SetRecordParameters(WhatIsMyRecord(), _fractionsScenes[_currentIndex].name);
    }

    private void LockedOrNot()
    {
        if (_fractionsScenes[_currentIndex].isLocked)
        {
            FindObjectOfType<LevelLocker>().LockLevel(_fractionsScenes[_currentIndex].color, _fractionsScenes[_currentIndex].scoreToUnlock);
        }
        else
        {
            FindObjectOfType<LevelLocker>().UnlockLevel();
        }
    }


    public int WhatIsMyRecord(int index = -1)
    {
        if (index == -1) index = _currentIndex;
        return PlayerPrefs.GetInt(_fractionsScenes[index].name, 0);
    }

    public string WhatIsMyGameMode()
    {
        return _fractionsScenes[_currentIndex].name;
    }


    private void UnlockByRecords()
    {
        for(int i = 0; i<_fractionsScenes.Length; i++)
        {            
            if (_fractionsScenes[i].isLocked)
            {
                foreach(int index in _fractionsScenes[i].modeToUnlock)
                {
                    if (WhatIsMyRecord(index) >= _fractionsScenes[i].scoreToUnlock)
                    {
                        _fractionsScenes[i].isLocked = false;
                        break;
                    }
                }
            }
        }
    }
}


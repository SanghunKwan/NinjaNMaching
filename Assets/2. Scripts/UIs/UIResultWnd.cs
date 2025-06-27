using DefineStructure;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIResultWnd : MonoBehaviour
{
    [Header("결과창 자원")]
    [SerializeField] Sprite[] _resultIcons;
    [Header("결과창 설정")]
    [SerializeField] GameObject _resultIconObj;
    [SerializeField] Image _resultIcon;

    [SerializeField] Text _textTitle;

    [SerializeField] Text _textMatchCount;
    [SerializeField] Text _textMissMatchCount;

    [SerializeField] Text _textMinute;
    [SerializeField] Text _textSeconds;
    [SerializeField] Text _textMilSeconds;
    [SerializeField] Text _textAcquisitionXP;

    [SerializeField] ScrollRect _killCountViewer;
    [SerializeField] GameObject _nextButton;

    GameObject _prefabBar;
    RectTransform _content;

    public void CloseWnd()
    {
        gameObject.SetActive(false);
    }
    public void OpenWnd(bool isSuccess, int rank, int mCount, int mmCount, float gameTime, int xp, IReadOnlyDictionary<int, DefeatMonsterInfo> killList)
    {
        gameObject.SetActive(true);

        if (isSuccess)
        {
            _resultIconObj.SetActive(true);
            _nextButton.SetActive(true);
            _resultIcon.sprite = _resultIcons[rank - 1];
        }
        else
        {
            _resultIconObj.SetActive(false);
            _nextButton.SetActive(false);
        }

        _textMatchCount.text = mCount.ToString();
        _textMissMatchCount.text = mmCount.ToString();
        _textAcquisitionXP.text = xp.ToString();

        int integerTime = Mathf.FloorToInt(gameTime);

        _textMinute.text = (integerTime / 60).ToString();
        _textSeconds.text = (integerTime % 60).ToString();
        _textMinute.text = Mathf.FloorToInt((gameTime - integerTime) * 100).ToString();

        _prefabBar = Resources.Load<GameObject>("Prefabs/UIs/KillCountBar");
        _content = _killCountViewer.content;
        foreach (var item in killList.Values)
        {

            GameObject go = Instantiate(_prefabBar, _content);
            UIKillCountBar bar = go.GetComponent<UIKillCountBar>();
            bar.OpenBar(item._rank, item._icon, item._name, item._count);
        }
    }


    public void ClickRegameButton()
    {

    }
    public void ClickGoHomeButton()
    {

    }
    public void ClickNextStageButton()
    {

    }
}

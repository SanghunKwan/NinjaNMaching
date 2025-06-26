using System;
using System.Collections;
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

    [SerializeField] GameObject _nextButton;


    public void CloseWnd()
    {
        gameObject.SetActive(false);
    }
    public void OpenWnd(bool isSuccess, int rank, int mCount, int mmCount, float gameTime, int xp)
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

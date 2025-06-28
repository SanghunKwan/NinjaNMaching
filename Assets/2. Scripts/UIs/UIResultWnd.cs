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

        //_textMatchCount.text = mCount.ToString();
        //_textMissMatchCount.text = mmCount.ToString();
        //_textAcquisitionXP.text = xp.ToString();

        //int integerTime = Mathf.FloorToInt(gameTime);

        //_textMinute.text = (integerTime / 60).ToString();
        //_textSeconds.text = (integerTime % 60).ToString();
        //_textMilSeconds.text = Mathf.FloorToInt((gameTime - integerTime) * 100).ToString();

        _prefabBar = Resources.Load<GameObject>("Prefabs/UIs/KillCountBar");
        _content = _killCountViewer.content;
        foreach (var item in killList.Values)
        {

            GameObject go = Instantiate(_prefabBar, _content);
            UIKillCountBar bar = go.GetComponent<UIKillCountBar>();
            bar.OpenBar(item._rank, item._icon, item._name, item._count);
        }

        StartCoroutine(SetText(mCount, mmCount, gameTime, xp));
    }
    IEnumerator SetText(int mCount, int mmCount, float gameTime, int xp)
    {
        //수정할 모든 text 
        //특정 초가 되기 전까지 text 규칙에 따라 계속 바꿈.
        float accumTime = 0f;

        yield return RepeatUntil(() => accumTime < 2,
           () =>
           {
               ChangeTextRandom(_textMatchCount, 5);
               ChangeTextRandom(_textMissMatchCount, 5);
               ChangeTextRandom(_textAcquisitionXP, 5);
               ChangeTextRandom(_textMinute, 2);
               ChangeTextRandom(_textSeconds, 2);
               ChangeTextRandom(_textMilSeconds, 2);
               accumTime += Time.deltaTime;
           },
           () =>
           {
               _textMatchCount.text = mCount.ToString();
               _textMissMatchCount.text = mmCount.ToString();
               accumTime = 0f;
           });

        yield return RepeatUntil(() => accumTime < 2,
            () =>
            {
                ChangeTextRandom(_textAcquisitionXP, 2);
                ChangeTextRandom(_textMinute, 2);
                ChangeTextRandom(_textSeconds, 2);
                ChangeTextRandom(_textMilSeconds, 2);
                accumTime += Time.deltaTime;
            },
            () =>
            {
                int integerTime = Mathf.FloorToInt(gameTime);

                _textMinute.text = (integerTime / 60).ToString();
                _textSeconds.text = (integerTime % 60).ToString();
                _textMilSeconds.text = Mathf.FloorToInt((gameTime - integerTime) * 100).ToString();
                accumTime = 0f;
            });

        yield return RepeatUntil(() => accumTime < 2,
            () =>
            {
                ChangeTextRandom(_textAcquisitionXP, 2);
                accumTime += Time.deltaTime;
            },
            () =>
            {
                _textAcquisitionXP.text = xp.ToString();
                accumTime = 0f;
            });
        //특정 초 후에 이벤트 작동.
        //이벤트가 작동한 후에 관련 text는 규칙에 따라 바뀌지 않음.
    }
    IEnumerator RepeatUntil(Func<bool> condition, Action repeatAction, Action endAction)
    {
        while (condition())
        {
            repeatAction?.Invoke();
            yield return null; // 다음 프레임까지 대기
        }
        endAction?.Invoke();
    }
    void ChangeTextRandom(Text text, uint digits)
    {
        string str = string.Format("{0:D" + digits + "}", UnityEngine.Random.Range(0, ((int)digits) * 10));
        text.text = str;
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

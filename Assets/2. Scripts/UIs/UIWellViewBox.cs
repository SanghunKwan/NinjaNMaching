using UnityEngine;
using UnityEngine.UI;
using DefineEnum;

public class UIWellViewBox : MonoBehaviour
{
    [Header("리소스 자원")]
    [SerializeField] Sprite[] _btnImg;

    [Header("UI 설정")]
    [SerializeField] RectTransform _epBG;
    [SerializeField] Text _titleName;
    [SerializeField] Image _dragUnDBtn;
    [SerializeField] Button _prevButton;
    [SerializeField] Button _nextButton;

    bool _isUp;

    int _epCount;
    int _nowChapter;


    //임시

    private void Start()
    {
        InitBox(1, 2);
    }
    //==

    void SetTitleBox(int epNum)
    {
        _nowChapter = epNum;
        _titleName.text = GameTableManager._instance.Get(InfoTableName.ChapterInfoList).ToStr(epNum, "TitleName");
        if (_nowChapter < 2)
        {
            _prevButton.interactable = false;
            _nextButton.interactable = true;
        }
        else if (_nowChapter >= _epCount)
        {
            _prevButton.interactable = true;
            _nextButton.interactable = false;
        }
        else
        {
            _prevButton.interactable = true;
            _nextButton.interactable = true;
        }
    }


    public void InitBox(int epNum, int maxEp)
    {
        _isUp = true;
        _epCount = maxEp;

        SetTitleBox(epNum);
    }

    public void ClickUpNDownButton()
    {
        if (_isUp = !_isUp)
        {
            //_epBG를 밑으로 내린다.
            _epBG.anchoredPosition += Vector2.up * 90;
            _dragUnDBtn.sprite = _btnImg[0];
        }
        else
        {
            //_epBG를 위로 올린다.
            _epBG.anchoredPosition += Vector2.down * 90;
            _dragUnDBtn.sprite = _btnImg[1];
        }
    }
    public void ClickPrevButton()
    {
        SetTitleBox(_nowChapter - 1);
        //맵과 배치 변화.
    }
    public void ClickNextButton()
    {
        SetTitleBox(_nowChapter + 1);
        //맵과 배치 변화
    }
}

using UnityEngine;
using UnityEngine.UI;
using DefineEnum;
using System.Collections.Generic;

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
    int _openChapter = 1;
    int _clearStage = 2;
    //==


    Vector2 _epBGAnchorPosition;

    float _epBGSpeed;

    //prefab
    GameObject _prefabStageBtn;
    GameObject _prefabStagePoint;
    GameObject _prefabSelectWnd;

    Transform _pointRoot;

    //참조
    Image _imageMap;
    Dictionary<int, Dictionary<int, UIStageInfoBtn>> _stageAllList;
    List<GameObject> _rootList;
    Dictionary<int, UIStageInfoBtn> _refStageList;


    //UI
    UISelectStageWnd _uiStageInfoWnd;

    void SetChapterInfo(int epNum, TableBase table)
    {
        _nowChapter = epNum;

        string name = table.ToStr(epNum, ChapterInfoList.Index.MapName.ToString());
        Sprite map = Resources.Load<Sprite>("Images/Maps/" + name);
        _imageMap.sprite = map;

        if (_stageAllList.ContainsKey(epNum))
        {
            for (int i = 0; i < _rootList.Count; i++)
            {
                if (i + 1 != epNum)
                    _rootList[i].SetActive(false);
                else
                {
                    _rootList[i].SetActive(true);
                }
            }
            _refStageList = _stageAllList[epNum];
        }
        else
        {
            Dictionary<int, UIStageInfoBtn> chapterList = new Dictionary<int, UIStageInfoBtn>();



            name = table.ToStr(epNum, ChapterInfoList.Index.RootName.ToString());
            _prefabStagePoint = Resources.Load<GameObject>("Prefabs/Maps/" + name);

            GameObject root = new GameObject("root" + epNum);
            Transform rootTransform = root.transform;
            rootTransform.SetParent(_pointRoot, false);


            GameObject go = Instantiate(_prefabStagePoint, _pointRoot);
            Transform point = go.transform;

            GameObject tempItem;
            UIStageInfoBtn tempStageBtn;
            for (int i = 0; i < point.childCount; i++)
            {
                tempItem = Instantiate(_prefabStageBtn, point.GetChild(i).position, Quaternion.identity, rootTransform);

                tempStageBtn = tempItem.GetComponent<UIStageInfoBtn>();
                //수정 사항
                int star = 0;
                int clear = _clearStage;
                if (epNum > _openChapter)
                {
                    clear = -1;
                }

                if (i < clear) // i + 1 < _clearStage + 1
                {
                    star = Random.Range(1, 4); //1 ~ 3
                }
                tempStageBtn.InitBtn(i + 1, clear, star, this);
                //==

                chapterList.Add(i + 1, tempStageBtn);
            }
            _stageAllList.Add(epNum, chapterList);
            _rootList.Add(root);

            for (int i = 0; i < _rootList.Count; i++)
            {
                if (i + 1 != epNum)
                    _rootList[i].SetActive(false);
            }
            _refStageList = _stageAllList[epNum];
        }

        _titleName.text = table.ToStr(epNum, "TitleName");

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


    public void InitBox(int epNum)
    {
        _isUp = true;
        _epBGAnchorPosition = _epBG.anchoredPosition;

        _prefabStageBtn = Resources.Load<GameObject>("Prefabs/UIs/StageInfoMiniButton");
        _prefabSelectWnd = Resources.Load<GameObject>("Prefabs/UIs/SelectStageWindow");

        _stageAllList = new();
        _rootList = new();

        ScrollRect sRect = transform.GetChild(0).GetComponent<ScrollRect>();
        _imageMap = sRect.content.GetChild(0).GetComponent<Image>();

        TableBase table = GameTableManager._instance.Get(InfoTableName.ChapterInfoList);
        _epCount = table._recordCount;
        _pointRoot = _imageMap.transform.GetChild(0);

        SetChapterInfo(epNum, table);
    }

    private void Update()
    {
        if (_epBG.anchoredPosition == _epBGAnchorPosition) return;

        _epBG.anchoredPosition = Vector2.MoveTowards(_epBG.anchoredPosition, _epBGAnchorPosition, _epBGSpeed);
        _epBGSpeed += 0.05f;

    }

    public void AllCancel()
    {
        foreach (var item in _refStageList.Values)
        {
            item.SetBtnToNormal();
        }
    }
    public void OpenSelectStageWnd(int selectStage)
    {
        if (_uiStageInfoWnd == null)
        {
            GameObject go = Instantiate(_prefabSelectWnd);
            _uiStageInfoWnd = go.GetComponent<UISelectStageWnd>();
        }
        _uiStageInfoWnd.OpenWnd(_nowChapter, selectStage);
    }


    public void ClickUpNDownButton()
    {
        _epBGSpeed = -0.7f;

        if (_isUp = !_isUp)
        {
            //_epBG를 밑으로 내린다.
            _epBGAnchorPosition += Vector2.up * 90;
            _dragUnDBtn.sprite = _btnImg[0];
        }
        else
        {
            //_epBG를 위로 올린다.
            _epBGAnchorPosition += Vector2.down * 90;
            _dragUnDBtn.sprite = _btnImg[1];
        }
    }
    public void ClickPrevButton()
    {
        SetChapterInfo(_nowChapter - 1, GameTableManager._instance.Get(InfoTableName.ChapterInfoList));
    }
    public void ClickNextButton()
    {
        SetChapterInfo(_nowChapter + 1, GameTableManager._instance.Get(InfoTableName.ChapterInfoList));
    }
}

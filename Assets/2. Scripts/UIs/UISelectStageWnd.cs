using System.Collections.Generic;
using DefineEnum;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISelectStageWnd : MonoBehaviour
{
    [SerializeField] Text _textStageName;

    [SerializeField] Text _textReward;
    [SerializeField] Text _textCardCount;
    [SerializeField] Text _textHitTimeLimit;

    [SerializeField] Text[] _starConditions;
    const int ConditionCount = 3;

    int _thisChapter;
    int _thisStage;


    [SerializeField] Transform _monBarParent;

    [SerializeField] Sprite[] _monsterGradeIcons;

    [SerializeField] EventTrigger nextButton;

    GameObject _prefabBar;
    List<GameObject> _monsterBarList;



    public Sprite GetIconFromMonsterGrade(MonsterGrade mg) => _monsterGradeIcons[(int)mg];
    public Sprite GetIconFromMonsterHead(in string iconName) => ResourcePoolManager._instance.Get<Sprite>(PoolDataType.MONSTERICON, iconName);



    // closeWnd
    // openWnd

    //startBtn

    public void InitWnd()
    {
        _monsterBarList = new List<GameObject>();

        _prefabBar = Resources.Load<GameObject>("Prefabs/UIs/MonsterInfoBar");
    }


    public void CloseWnd()
    {
        gameObject.SetActive(false);

        WellOfGodManager._instance.SetButtonCancel(_thisStage);

        int length = _monsterBarList.Count;
        for (int i = 0; i < length; i++)
        {
            _monsterBarList[i].SetActive(false);
        }
    }
    public void OpenWnd(int chapter, int stageIndex)
    {
        gameObject.SetActive(true);

        _thisChapter = chapter;
        _thisStage = stageIndex;


        string tempString;
        //스테이지 이름
        TableBase tb = GameTableManager._instance.Get(InfoTableName.StageInfoList);
        tempString = tb.ToStr(stageIndex, StageInfoList.Index.StageName.ToString());
        _textStageName.text = tempString;

        //몬스터 추가타 대기 시간
        tempString = tb.ToStr(stageIndex, StageInfoList.Index.LimitTime.ToString());
        _textHitTimeLimit.text = tempString;
        //카드 패 수
        tempString = tb.ToStr(stageIndex, StageInfoList.Index.CardCount.ToString());
        _textCardCount.text = tempString;
        //리워드
        tempString = tb.ToStr(stageIndex, StageInfoList.Index.AccquisitionXP.ToString());
        _textReward.text = tempString;


        tempString = tb.ToStr(stageIndex, StageInfoList.Index.Condition1.ToString());
        //별 1 클리어 조건(대부분 생존 시 별 1)
        _starConditions[0].text = "생명력 " + tempString + "% 이하";
        //별 2 클리어 조건
        _starConditions[1].text = LifeCondition(tempString);
        //별 3 클리어 조건
        tempString = tb.ToStr(stageIndex, StageInfoList.Index.Condition2.ToString());
        _starConditions[2].text = LifeCondition(tempString);

        //몬스터 정보
        int spawnIndex = tb.ToInt(stageIndex, StageInfoList.Index.SpawnIndex.ToString());

        tb = GameTableManager._instance.Get(InfoTableName.MonsterSpawnList);
        int monsterCount = tb.ToInt(spawnIndex, MonsterSpawnList.Index.MonsterCount.ToString());
        int[] monIndexArray = new int[monsterCount];
        for (int i = 0; i < monsterCount; i++)
        {
            monIndexArray[i] = tb.ToInt(spawnIndex, ((MonsterSpawnList.Index)(i + 2)).ToString());
        }

        tb = GameTableManager._instance.Get(InfoTableName.MonsterInfoList);

        Sprite tempIcon;
        MonsterGrade tempGrade;
        for (int i = 0; i < monsterCount; i++)
        {
            tempString = tb.ToStr(monIndexArray[i], MonsterInfoList.Index.IconName.ToString());
            tempGrade = (MonsterGrade)tb.ToInt(monIndexArray[i], MonsterInfoList.Index.Grade.ToString());
            tempIcon = GetIconFromMonsterHead(tempString);

            tempString = tb.ToStr(monIndexArray[i], MonsterInfoList.Index.MonsterName.ToString());

            if (_monsterBarList.Count > i)
            {
                _monsterBarList[i].SetActive(true);
            }
            else
            {
                GameObject barObject = Instantiate(_prefabBar, _monBarParent);
                _monsterBarList.Add(barObject);
                barObject.GetComponent<UIMonsterInfoBar>().InitBar(GetIconFromMonsterGrade(tempGrade), tempIcon, tempString);
            }
        }

        //nextButton.triggers = null;
        //EventTrigger.Entry entry = new EventTrigger.Entry();
        //entry.eventID = EventTriggerType.PointerClick;
        //entry.callback.AddListener(CallNextScene);
        //nextButton.triggers.Add(entry);
    }
    string LifeCondition(in string life) => "생명력 " + life + "% 이상";


    public void OnClickExitBtn()
    {
        CloseWnd();
    }
    public void OnClickStartButton()
    {
        UserInfoManager._instance._nowChapter = _thisChapter;
        UserInfoManager._instance._selectStage = _thisStage;

        SceneControlManager._instance.StartGameStage();
    }
}

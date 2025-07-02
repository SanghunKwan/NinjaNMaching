using System.Collections.Generic;
using DefineEnum;
using UnityEngine;
using UnityEngine.UI;

public class UISelectStageWnd : MonoBehaviour
{
    [SerializeField] Text _textStageName;

    [SerializeField] Text _textReward;
    [SerializeField] Text _textCardCount;
    [SerializeField] Text _textHitTimeLimit;

    [SerializeField] Text[] _starConditions;
    const int ConditionCount = 3;

    [SerializeField] Transform _monBarParent;

    [SerializeField] Sprite[] _monsterGradeIcons;

    GameObject _prefabBar;
    List<GameObject> _monsterBarList;

    public Sprite GetIconFromMonsterGrade(MonsterGrade mg) => _monsterGradeIcons[(int)mg];
    public Sprite GetIconFromMonsterHead(in string iconName) => ResourcePoolManager._instance.Get<Sprite>(PoolDataType.MONSTERICON, iconName);



    // closeWnd
    // openWnd

    //startBtn
    public void CloseWnd()
    {
        gameObject.SetActive(false);
    }

    private void Start()
    {
        OpenWnd(1, 1);
    }
    public void OpenWnd(int chapter, int stageIndex)
    {
        gameObject.SetActive(true);

        string tempString;
        //�������� �̸�
        TableBase tb = GameTableManager._instance.Get(InfoTableName.StageInfoList);
        tempString = tb.ToStr(stageIndex, StageInfoList.Index.StageName.ToString());
        _textStageName.text = tempString;

        //���� �߰�Ÿ ��� �ð�
        tempString = tb.ToStr(stageIndex, StageInfoList.Index.LimitTime.ToString());
        _textHitTimeLimit.text = tempString;
        //ī�� �� ��
        tempString = tb.ToStr(stageIndex, StageInfoList.Index.CardCount.ToString());
        _textCardCount.text = tempString;
        //������
        tempString = tb.ToStr(stageIndex, StageInfoList.Index.AccquisitionXP.ToString());
        _textReward.text = tempString;


        //�� 1 Ŭ���� ����(��κ� ���� �� �� 1)
        _starConditions[0].text = "����";
        //�� 2 Ŭ���� ����
        tempString = tb.ToStr(stageIndex, StageInfoList.Index.Condition1.ToString());
        _starConditions[1].text = LifeCondition(tempString);
        //�� 3 Ŭ���� ����
        tempString = tb.ToStr(stageIndex, StageInfoList.Index.Condition2.ToString());
        _starConditions[2].text = LifeCondition(tempString);

        //���� ����
        int spawnIndex = tb.ToInt(stageIndex, StageInfoList.Index.SpawnIndex.ToString());
        Debug.Log(spawnIndex);

        tb = GameTableManager._instance.Get(InfoTableName.MonsterSpawnList);
        int monsterCount = tb.ToInt(spawnIndex, MonsterSpawnList.Index.MonsterCount.ToString());
        int[] monIndexArray = new int[monsterCount];
        for (int i = 0; i < monsterCount; i++)
        {
            monIndexArray[i] = tb.ToInt(spawnIndex, ((MonsterSpawnList.Index)(i + 2)).ToString());
        }

        tb = GameTableManager._instance.Get(InfoTableName.MonsterInfoList);
        UIMonsterInfoBar bar = Resources.Load<UIMonsterInfoBar>("Prefabs/UIs/MonsterInfoBar");

        Sprite tempIcon;
        MonsterGrade tempGrade;
        for (int i = 0; i < monsterCount; i++)
        {
            tempString = tb.ToStr(monIndexArray[i], MonsterInfoList.Index.IconName.ToString());
            tempGrade = (MonsterGrade)tb.ToInt(monIndexArray[i], MonsterInfoList.Index.Grade.ToString());
            tempIcon = GetIconFromMonsterHead(tempString);

            tempString = tb.ToStr(monIndexArray[i], MonsterInfoList.Index.MonsterName.ToString());
            Instantiate(bar, _monBarParent).InitBar(GetIconFromMonsterGrade(tempGrade), tempIcon, tempString);
        }

    }
    string LifeCondition(in string life) => "����� " + life + "% �̻�";


    public void OnClickExitBtn()
    {
        CloseWnd();
    }
    public void OnClickStartButton()
    {

    }
}

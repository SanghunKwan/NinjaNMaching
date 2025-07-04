using System.Collections.Generic;
using UnityEngine;
using DefineEnum;
using System.Collections;
using DefineStructure;

public class IngameManager : MonoBehaviour
{
    static IngameManager _uniqueInstance;

    const int _limitWidth = 6;
    const float _cardGenDelayTime = 0.1f;
    Vector2 _offset = new Vector2(1.6f, -2.1f);

    [Header("리소스 관리")]

    [SerializeField] Sprite[] _monsterGradeIcons;


    //참조 변수
    GameObject _prefabPlayer;
    GameObject _prefabCard;
    GameObject _prefabBigMsgBox;
    GameObject _prefabMiniMonBox;
    GameObject _prefabMiniInfoBox;
    GameObject _prefabTimerBox;
    GameObject _prefabMatchBox;
    GameObject _prefabResultWnd;

    Transform _cardRoot;
    Transform _mapPosition;
    Transform _playerStartPos;
    Transform _rootAction;

    HeroObj _player;
    MonsterObj _other;

    //정보 변수
    int _firstIndex = -1;
    int _secondIndex = -1;
    int _choiceCount;

    int _maxMonsterCount;
    int _nowMonsterNumber;

    int _totalMissmatchCount;
    int _totalMatchCount;

    GameState _currentState;
    StageInfo _stageInfo;

    float _checkTime;
    float _missMatchCount;

    float _playTime;
    float _extraStrokeTime;

    bool _isDestoryCard;
    bool _gameSuccess;

    //Datas
    List<CardObj> _genCardList;
    Dictionary<int, DefeatMonsterInfo> _killMonsterList;

    //UIs
    Transform _mainFrame;
    UIBigMessageBox _uiBMBox;
    UIMiniInfoMonsterBox _uiMiniInfoMonBox;
    UIMiniStatInfoBox _uiMiniInfoBox;
    UITimerBox _uiTimerBox;
    UIMatchInfoBox _uiMatchBox;


    public GameState _nowState => _currentState;
    public bool _isChoiceEnd => _choiceCount == 2;

    public static IngameManager _instance => _uniqueInstance;

    private void Awake()
    {
        _uniqueInstance = this;
    }

    private void Update()
    {
        switch (_currentState)
        {
            case GameState.START:
                _checkTime += Time.deltaTime;
                if (_checkTime > 1)
                    PlayGame();
                break;
            case GameState.PLAYGAME:
                _extraStrokeTime -= Time.deltaTime;
                if (_extraStrokeTime <= 0)
                {
                    //한 대 맞아라~
                    _extraStrokeTime += _stageInfo._penaltyTime;
                }
                _uiTimerBox.SetNowTime(_extraStrokeTime);
                if (_isDestoryCard)
                {
                    bool isAll = true;
                    foreach (var item in _genCardList)
                    {
                        if (item != null)
                        {
                            isAll = false;
                            break;
                        }
                    }

                    if (isAll)
                    {
                        _genCardList.Clear();
                        CardDeploy();
                    }
                    _isDestoryCard = false;
                }
                if (_choiceCount == 2)
                {
                    if (_genCardList[_firstIndex]._isOpened && _genCardList[_secondIndex]._isOpened)
                    {
                        if (MatchedCard(_firstIndex, _secondIndex))
                        {
                            //플레이어에게 몬스터를 공격하도록 지시.
                            _player.OrderOfAttack();

                            //삭제
                            //Debug.LogFormat("{0}번 카드와 {1}번 카드가 삭제되었습니다.", _firstIndex, _secondIndex);
                            Destroy(_genCardList[_firstIndex].gameObject);
                            Destroy(_genCardList[_secondIndex].gameObject);

                            _totalMatchCount++;
                            _choiceCount = 0;
                            _firstIndex = _secondIndex = -1;
                            _isDestoryCard = true;
                            _uiMatchBox.SetMatchCount(_totalMatchCount);
                        }
                        else
                        {
                            _totalMissmatchCount++;
                            _missMatchCount += 1;
                            //몬스터에게 실패 횟수를 알려줌.
                            _missMatchCount = _other.CalcAttackStartRate(_missMatchCount);

                            _genCardList[_firstIndex].CloseCard();
                            _genCardList[_secondIndex].CloseCard();
                            _uiMatchBox.SetMissMatchCount(_totalMissmatchCount);
                        }
                    }
                    else if (_genCardList[_firstIndex]._isClosed && _genCardList[_secondIndex]._isClosed)
                    {
                        _choiceCount = 0;
                        _firstIndex = _secondIndex = -1;
                    }
                }
                break;
            case GameState.DEADDELAY:
                _checkTime += Time.deltaTime;
                if (_checkTime >= 3)
                {
                    Intermission();
                }
                break;

            case GameState.ENDGAME:
                _checkTime += Time.deltaTime;
                if (_checkTime >= 1.5f)
                {
                    ResultGame();
                }
                break;
        }
    }

    //카드 관련 함수 모음
    IEnumerator GenerateCard(int count, float delay)
    {
        _genCardList = new List<CardObj>();
        int[] iconNumbers = GetShuffleCard(count / 2);

        int idxX;
        int idxY;
        Vector2 pos;
        GameObject go;
        CardObj obj;

        for (int i = 0; i < count; i++)
        {
            idxX = i % _limitWidth;
            idxY = i / _limitWidth;
            pos = new Vector2(idxX * _offset.x, idxY * _offset.y);

            go = Instantiate(_prefabCard, pos, Quaternion.identity, _cardRoot);
            go.transform.localPosition = pos;
            obj = go.GetComponent<CardObj>();
            obj.InitSetData(i, CardBGKind.CARD_BG_SUN, (CardIconType)iconNumbers[i]);
            _genCardList.Add(obj);

            yield return new WaitForSeconds(delay);
        }

        StartGame();
    }
    int[] GetShuffleCard(int typeCount)
    {
        int[] returnCards = new int[typeCount * 2];
        int[] typeIndexList = new int[typeCount];

        int cnt = (int)CardIconType.Max;
        // typeCount만큼의 종류를 임의로 뽑는다.
        // 1.
        //for (int i = 0; i < typeIndexList.Length; i++)
        //{
        //    int number = Random.Range(0, cnt);
        //    int index = i;
        //    while (--index >= 0)
        //    {
        //        if (typeIndexList[index] == number)
        //        {
        //            number = Random.Range(0, cnt);
        //            index = i;
        //        }
        //    }
        //    typeIndexList[i] = number;
        //}


        //2.
        int[] typeList = new int[cnt];
        for (int i = 0; i < cnt; i++)
            typeList[i] = i;

        int mix = 3;
        for (int i = 0; i < mix; i++)
        {
            for (int j = 0; j < cnt; j++)
            {
                int index = Random.Range(0, cnt);
                int number = typeList[i];
                typeList[i] = typeList[index];
                typeList[index] = number;
            }
        }

        for (int i = 0; i < typeIndexList.Length; i++)
        {
            int index = i * 2;
            typeIndexList[i] = typeList[i];
            returnCards[index] = typeIndexList[i];
            returnCards[index + 1] = typeIndexList[i];
        }
        mix = 5;
        for (int i = 0; i < mix; i++)
        {
            for (int j = 0; j < returnCards.Length; j++)
            {
                int index = Random.Range(0, returnCards.Length);
                int number = returnCards[i];
                returnCards[i] = returnCards[index];
                returnCards[index] = number;
            }
        }

        return returnCards;
    }
    bool MatchedCard(int firstIndex, int secondIndex)
    {
        if (_genCardList[firstIndex]._cardType != _genCardList[secondIndex]._cardType) return false;

        return true;
    }

    public void SelectCard(int NO)
    {
        if (_firstIndex == -1)
        {
            _firstIndex = NO;
            _choiceCount = 1;
        }
        else if (_secondIndex == -1)
        {
            if (_firstIndex != _secondIndex)
            {
                _secondIndex = NO;
                _choiceCount = 2;
            }
        }
    }
    //카드 관련 모음(end)
    public void KillCounting(int monID)
    {
        if (!_killMonsterList.ContainsKey(monID))
        {
            Debug.Log("있을 수 없는 일입니다!!!!");
            return;
        }
        _killMonsterList[monID]._count += 1;
    }

    void SettingStageInfoValues(int stage)
    {
        TableBase stageTable = GameTableManager._instance.Get(InfoTableName.StageInfoList);
        TableBase spawnTable = GameTableManager._instance.Get(InfoTableName.MonsterSpawnList);

        int spawnIndex = stageTable.ToInt(stage, "SpawnIndex");
        int[] indexList = new int[spawnTable.ToInt(spawnIndex, "MonsterCount")];
        for (int i = 0; i < indexList.Length; i++)
        {
            string column = "MonsterIndex" + (i + 1);
            indexList[i] = spawnTable.ToInt(spawnIndex, column);
        }
        string name = stageTable.ToStr(stage, "StageName");
        string map = stageTable.ToStr(stage, "MapName");
        float t = stageTable.ToFloat(stage, "LimitTime");
        int xp = stageTable.ToInt(stage, "AccquisitionXP");
        int con1 = stageTable.ToInt(stage, "Condition1");
        int con2 = stageTable.ToInt(stage, "Condition2");
        int cnt = stageTable.ToInt(stage, "CardCount");

        _stageInfo = new StageInfo(name, map, t, cnt, xp, con1, con2, indexList);
        _maxMonsterCount = _stageInfo._monIndexList.Count;
        _nowMonsterNumber = 0;
    }
    public void InitLoadGame(int stage)
    {
        _currentState = GameState.INITLOAD;

        _killMonsterList = new Dictionary<int, DefeatMonsterInfo>();
        _prefabPlayer = Resources.Load<GameObject>("Prefabs/Objects/HeroObject");
        _prefabCard = Resources.Load<GameObject>("Prefabs/Objects/CardObject");
        _prefabBigMsgBox = Resources.Load<GameObject>("Prefabs/UIs/BigMessageBox");
        _prefabMiniMonBox = Resources.Load<GameObject>("Prefabs/UIs/MiniInfoMonsterBox");
        _prefabMiniInfoBox = Resources.Load<GameObject>("Prefabs/UIs/MiniStatInfoBox");
        _prefabTimerBox = Resources.Load<GameObject>("Prefabs/UIs/TimerBox");
        _prefabMatchBox = Resources.Load<GameObject>("Prefabs/UIs/MatchInfoBox");
        _prefabResultWnd = Resources.Load<GameObject>("Prefabs/UIs/ResultWindow");

        GameObject go = GameObject.FindGameObjectWithTag("PosRoot");
        _cardRoot = go.transform;
        go = GameObject.FindGameObjectWithTag("MapPos");
        _mapPosition = go.transform;
        go = GameObject.FindGameObjectWithTag("PlayerStart");
        _playerStartPos = go.transform;
        go = GameObject.FindGameObjectWithTag("ActionRoot");
        _rootAction = go.transform;

        go = GameObject.FindGameObjectWithTag("UIMainFrame");
        _mainFrame = go.transform;
        go = Instantiate(_prefabBigMsgBox, _mainFrame);
        _uiBMBox = go.GetComponent<UIBigMessageBox>();
        go = Instantiate(_prefabMiniMonBox, _mainFrame);
        _uiMiniInfoMonBox = go.GetComponent<UIMiniInfoMonsterBox>();
        go = Instantiate(_prefabMiniInfoBox, _mainFrame);
        _uiMiniInfoBox = go.GetComponent<UIMiniStatInfoBox>();
        go = Instantiate(_prefabTimerBox, _mainFrame);
        _uiTimerBox = go.GetComponent<UITimerBox>();
        go = Instantiate(_prefabMatchBox, _mainFrame);
        _uiMatchBox = go.GetComponent<UIMatchInfoBox>();

        SettingStageInfoValues(stage);

        _uiBMBox.CloseBox();
        _uiMiniInfoMonBox.CloseBox();
        _uiMiniInfoBox.CloseBox();
        _uiTimerBox.CloseBox();
        _uiMatchBox.CloseBox();

        GameObject map = Resources.Load("Prefabs/Maps/" + _stageInfo._mapName) as GameObject;
        Instantiate(map, _mapPosition);
    }
    public void ReadyGame()
    {
        _currentState = GameState.READY;

        _uiBMBox.OpenBox("READY!!");

        GameObject go = Instantiate(_prefabPlayer);
        _player = go.GetComponent<HeroObj>();
        _missMatchCount = _totalMissmatchCount = 0;

        //임시
        _player.InitSet(_rootAction.GetChild(0), "홍길동", 1, 0, _uiMiniInfoBox);
        //==

        _uiTimerBox.OpenBox();
        _uiMatchBox.OpenBox();
        _playTime = Time.time;
    }
    public void CardDeploy()
    {
        _currentState = GameState.CARDDEPLOY;

        bool beNot = true;

        if (_genCardList != null)
        {
            for (int i = 0; i < _genCardList.Count; i++)
            {
                if (_genCardList[i] != null)
                {
                    beNot = false;
                    break;
                }
            }
        }
        if (beNot)
        {
            _uiBMBox.OpenBox("CardSet...");
            StartCoroutine(GenerateCard(_stageInfo._cardCount, _cardGenDelayTime));
        }
        else
            StartGame();
    }
    public void StartGame()
    {
        _currentState = GameState.START;

        _uiBMBox.OpenBox("Game Start!!!!!", MessageType.Movement, 0.3f);
        _checkTime = 0;
    }
    public void PlayGame()
    {
        _currentState = GameState.PLAYGAME;

        _uiBMBox.CloseBox();
        _checkTime = 0;

    }
    public void Intermission()
    {
        _currentState = GameState.INTERMISSION;

        _uiBMBox.OpenBox("몬스터 등장!?!");
        _nowMonsterNumber++;
        int monIndex = _stageInfo._monIndexList.Dequeue();
        TableBase tb = GameTableManager._instance.Get(InfoTableName.MonsterInfoList);
        string prefabName = tb.ToStr(monIndex, "PrefabName");
        string iconName = tb.ToStr(monIndex, MonsterInfoList.Index.IconName.ToString());
        GameObject prefabMon = Resources.Load<GameObject>("Prefabs/Objects/" + prefabName);
        GameObject go = Instantiate(prefabMon);
        _other = go.GetComponent<MonsterObj>();
        _other.InitSet(_rootAction.GetChild(1), monIndex, _uiMiniInfoMonBox, _player);
        _player.SetTargetMonster(_other);

        if (!_killMonsterList.ContainsKey(monIndex))
        {
            Sprite rank = GetIconFromMonsterGrade((MonsterGrade)tb.ToInt(monIndex, MonsterInfoList.Index.Grade.ToString()));
            Sprite icon = ResourcePoolManager._instance.Get<Sprite>(PoolDataType.MONSTERICON, iconName);
            DefeatMonsterInfo info = new DefeatMonsterInfo(monIndex, _other._myName, rank, icon);
            _killMonsterList.Add(monIndex, info);
        }

        _extraStrokeTime = _stageInfo._penaltyTime;
        _uiTimerBox.SetNowTime(_extraStrokeTime);
    }
    public void DeadDelayTime()
    {
        _currentState = GameState.DEADDELAY;

        _checkTime = 0;
        if (_stageInfo._monIndexList.Count > 0)
        {
            _uiBMBox.OpenBox("Gooooooood~!!", MessageType.Timer, 2);
        }
        else
        {
            EndGame(true);
        }
    }
    public void EndGame(bool isSuccess)
    {
        _currentState = GameState.ENDGAME;

        if (_gameSuccess = isSuccess)
            _uiBMBox.OpenBox("축하합니다~!!");
        else
            _uiBMBox.OpenBox("게임 오버...");

        _checkTime = 0;
        _playTime = Time.time - _playTime;
    }
    public void ResultGame()
    {
        _currentState = GameState.RESULTGAME;

        _uiBMBox.CloseBox();
        GameObject go = Instantiate(_prefabResultWnd);
        UIResultWnd wnd = go.GetComponent<UIResultWnd>();

        int rank = 0;
        if (_gameSuccess)
        {
            //Rank 체크
            float std = _player._hpRate * 100;
            if (std >= _stageInfo._clearCon._condition2)
                rank = 3;
            else if (std >= _stageInfo._clearCon._condition1)
                rank = 2;
            else
                rank = 1;
        }

        wnd.OpenWnd(_gameSuccess, rank, _totalMatchCount, _totalMissmatchCount, _playTime, _stageInfo._rewardXP, _killMonsterList);
    }

    public Sprite GetIconFromMonsterGrade(MonsterGrade mg)
    {
        int id = (int)mg;
        return _monsterGradeIcons[id];
    }
}

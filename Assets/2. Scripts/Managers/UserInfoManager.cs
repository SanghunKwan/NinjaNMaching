using DefineEnum;
using DefineStructure;
using System.Collections.Generic;
using UnityEngine;

public class UserInfoManager : TSingleton<UserInfoManager>
{
    //임시
    int[] stageCounts = { 4, 4 };
    //===
    bool _isSave;


    Dictionary<int, Dictionary<int, int>> _acquisitionRewardList;

    GameInfoDESC _gameInfo;

    public bool _isAudioSaved { get; set; }

    public int _nowChapter
    {
        get => _gameInfo._selectedChapter;
        set
        {
            _gameInfo._selectedChapter = value;
            PlayerPrefs.SetInt("PlayerChapter", _gameInfo._selectedChapter);
            if (_isSave)
                PlayerPrefs.Save();
        }

    }
    public int _selectStage
    {
        get => _gameInfo._selectedStage;
        set
        {
            _gameInfo._selectedStage = value;
            if (_isSave)
                PlayerPrefs.Save();
        }
    }
    public int _openedChapter
    {
        get => _gameInfo._openChapter;
        set
        {
            _gameInfo._openChapter = value;
            PlayerPrefs.SetInt("OpenedChapter", value);
            if (_isSave)
                PlayerPrefs.Save();
        }
    }
    public int _clearedStage
    {
        get => _gameInfo._clearedStage;
        set
        {
            _gameInfo._clearedStage = value;
            PlayerPrefs.SetInt("CompleteStage", value);
            if (_isSave)
                PlayerPrefs.Save();
        }
    }
    public string _characterName
    {
        get => _gameInfo._name;
        set
        {
            _gameInfo._name = value;
            if (_isSave)
                PlayerPrefs.Save();
        }
    }
    public int _currentCharacterLevel
    {
        get => _gameInfo._level;
        set
        {
            _gameInfo._level = value;
            if (_isSave)
                PlayerPrefs.Save();
        }
    }
    public int _currentCharacterXP
    {
        get => _gameInfo._xp;
        set
        {
            _gameInfo._xp = value;
            LevelUPCheck();
            if (_isSave)
                PlayerPrefs.Save();
        }
    }
    public float _bgmVol { get; set; }
    public bool _bgmMute { get; set; }
    public bool _bgmLoop { get; set; }
    public float _sfxVol { get; set; }
    public bool _sfxMute { get; set; }
    public bool _sfxLoop { get; set; }


    public void InitInfoData()
    {
        _acquisitionRewardList = new Dictionary<int, Dictionary<int, int>>();


        if (PlayerPrefs.HasKey("PlayChapter"))
            _nowChapter = PlayerPrefs.GetInt("PlayChapter");
        else
        {
            _nowChapter = 1;
            PlayerPrefs.SetInt("PlayChapter", _nowChapter);
        }

        if (PlayerPrefs.HasKey("PlayStage"))
            _selectStage = PlayerPrefs.GetInt("PlayStage");
        else
        {
            _selectStage = 1;
            PlayerPrefs.SetInt("PlayStage", _selectStage);
        }

        if (PlayerPrefs.HasKey("OpenedChapter"))
            _openedChapter = PlayerPrefs.GetInt("OpenedChapter");
        else
        {
            _openedChapter = 1;
            PlayerPrefs.SetInt("OpenedChapter", _openedChapter);
        }

        if (PlayerPrefs.HasKey("CompleteStage"))
            _clearedStage = PlayerPrefs.GetInt("CompleteStage");
        else
        {
            _clearedStage = 0;
            PlayerPrefs.SetInt("CompleteStage", _clearedStage);
        }

        if (PlayerPrefs.HasKey("CharacterName"))
            _characterName = PlayerPrefs.GetString("CharacterName");
        else
        {
            _characterName = "비밀";
            PlayerPrefs.SetString("CharacterName", _characterName);
        }

        if (PlayerPrefs.HasKey("CharacterLevel"))
            _currentCharacterLevel = PlayerPrefs.GetInt("CharacterLevel");
        else
        {
            _currentCharacterLevel = 1;
            PlayerPrefs.SetInt("CharacterLevel", _currentCharacterLevel);
        }

        if (PlayerPrefs.HasKey("CharacterEXP"))
            _currentCharacterXP = PlayerPrefs.GetInt("CharacterEXP");
        else
        {
            _currentCharacterXP = 0;
            PlayerPrefs.SetInt("CharacterEXP", _currentCharacterXP);
        }

        for (int i = 1; i <= _openedChapter; i++)
        {
            Dictionary<int, int> stageRewardList = new Dictionary<int, int>();
            int count = (_openedChapter > i) ? stageCounts[i] : _clearedStage;
            for (int j = 1; j <= count; j++)
            {
                string index = RewardStarKey(i, j);
                int reward = PlayerPrefs.GetInt(index);

                stageRewardList.Add(j, reward);
            }

            if (_clearedStage > 0)
                _acquisitionRewardList.Add(i, stageRewardList);
        }

        //volume
        if (PlayerPrefs.HasKey("BGMVolum"))
            _bgmVol = PlayerPrefs.GetFloat("BGMVolum");
        else
        {
            _bgmVol = 1;
            PlayerPrefs.SetFloat("BGMVolum", _bgmVol);
        }

        if (PlayerPrefs.HasKey("SFXVolum"))
            _sfxVol = PlayerPrefs.GetFloat("SFXVolum");
        else
        {
            _sfxVol = 1;
            PlayerPrefs.SetFloat("SFXVolum", _sfxVol);
        }

        if (PlayerPrefs.HasKey("BGMMute"))
            _bgmMute = PlayerPrefs.GetInt("BGMMute") == 1 ? true : false;
        else
        {
            _bgmMute = false;
            PlayerPrefs.SetInt("BGMMute", _bgmMute ? 1 : 0);
        }

        if (PlayerPrefs.HasKey("SFXMute"))
            _sfxMute = PlayerPrefs.GetInt("SFXMute") == 1 ? true : false;
        else
        {
            _sfxMute = false;
            PlayerPrefs.SetInt("SFXMute", _sfxMute ? 1 : 0);
        }

        if (PlayerPrefs.HasKey("BGMLoop"))
            _bgmLoop = PlayerPrefs.GetInt("BGMLoop") == 1 ? true : false;
        else
        {
            _bgmLoop = false;
            PlayerPrefs.SetInt("BGMLoop", _bgmLoop ? 1 : 0);
        }

        if (PlayerPrefs.HasKey("SFXLoop"))
            _sfxLoop = PlayerPrefs.GetInt("SFXLoop") == 1 ? true : false;
        else
        {
            _sfxLoop = false;
            PlayerPrefs.SetInt("SFXLoop", _sfxLoop ? 1 : 0);
        }


        PlayerPrefs.Save();
        _isSave = true;
    }
    string RewardStarKey(int chapter, int stage) => string.Format("{0}Chapter{1}Stage", chapter, stage);


    public void SaveOption()
    {
        PlayerPrefs.SetFloat("BGMVolum", _bgmVol);
        PlayerPrefs.SetInt("BGMMute", _bgmMute ? 1 : 0);
        PlayerPrefs.SetInt("BGMLoop", _bgmLoop ? 1 : 0);
        PlayerPrefs.SetFloat("SFXVolum", _sfxVol);
        PlayerPrefs.SetInt("SFXMute", _sfxMute ? 1 : 0);
        PlayerPrefs.SetInt("SFXLoop", _sfxLoop ? 1 : 0);


        PlayerPrefs.Save();
    }

    public void StageClear(int clearRewardXp, int rank)
    {
        _currentCharacterXP += clearRewardXp;
        PlayerPrefs.SetInt("CharacterLevel", _currentCharacterLevel);
        PlayerPrefs.SetInt("CharacterEXP", _currentCharacterXP);

        string index = RewardStarKey(_nowChapter, _selectStage);
        if (_acquisitionRewardList.ContainsKey(_nowChapter))
        {
            if (_acquisitionRewardList[_nowChapter].ContainsKey(_selectStage))
            {
                if (_acquisitionRewardList[_nowChapter][_selectStage] < rank)
                {
                    PlayerPrefs.SetInt(index, rank);
                    _acquisitionRewardList[_nowChapter][_selectStage] = rank;
                }
            }
            else
            {
                PlayerPrefs.SetInt(index, rank);
                _acquisitionRewardList[_nowChapter].Add(_selectStage, rank);
            }
        }
        else
        {
            PlayerPrefs.SetInt(index, rank);
            Dictionary<int, int> tempDic = new Dictionary<int, int>();
            tempDic.Add(_selectStage, rank);
            _acquisitionRewardList.Add(_nowChapter, tempDic);
        }


        if (_nowChapter == _openedChapter &&
            _selectStage == _clearedStage + 1)
        {
            if (stageCounts[_nowChapter] == _selectStage)
            {
                if (_openedChapter == stageCounts.Length)
                {
                    Debug.Log("마지막 스테이지 클리어");
                    return;
                }


                //다음 챕터 오픈
                _openedChapter++;
                _clearedStage = 0;


            }
            else
            {
                //다음 스테이지 오픈
                _clearedStage++;
            }
        }
    }
    public void LevelUPCheck()
    {
        TableBase table = GameTableManager._instance.Get(InfoTableName.LevelInfoList);
        Debug.LogFormat("현재 레벨 : {0}\n현재 xp : {1}", _currentCharacterLevel, _currentCharacterXP);

        //레벨업 체크
        while (table.ToInt(_currentCharacterLevel, LevelInfoList.Index.XP.ToString()) <= _gameInfo._xp)
        {
            _currentCharacterLevel++;
            Debug.Log("현재 레벨 : " + _currentCharacterLevel);
        }

    }
    public IReadOnlyDictionary<int, int> GetStarDictionary(int chapter)
    {
        if (_acquisitionRewardList.ContainsKey(chapter))
            return _acquisitionRewardList[chapter];
        else
            return null;
    }
}

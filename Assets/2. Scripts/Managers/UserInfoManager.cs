using System.Collections.Generic;
using UnityEngine;

public class UserInfoManager : TSingleton<UserInfoManager>
{
    //임시
    int[] stageCounts = { 4, 4 };
    //===


    Dictionary<int, Dictionary<int, int>> _acquisitionRewardList;

    public int _nowChapter { get; set; }
    public int _selectStage { get; set; }
    public int _openedChapter { get; set; }
    public int _clearedStage { get; set; }
    public string _characterName { get; set; }
    public int _currentCharacterLevel { get; set; }
    public int _currentCharacterXP { get; set; }


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
            _selectStage = 0;
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
                string index = string.Format("{0}Chapter{1}Stage", i, j);
                int reward = PlayerPrefs.GetInt(index);

                stageRewardList.Add(j, reward);
            }

            if (_clearedStage > 0)
                _acquisitionRewardList.Add(i, stageRewardList);
        }

    }
}

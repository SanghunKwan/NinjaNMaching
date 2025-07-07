using System.Collections.Generic;
using UnityEngine;

namespace DefineStructure
{
    #region [public Utill Class]
    public class DefeatMonsterInfo
    {
        public int _id { get; set; }
        public string _name { get; set; }
        public Sprite _rank { get; set; }
        public Sprite _icon { get; set; }
        public int _count { get; set; }

        public DefeatMonsterInfo(int idx, string n, Sprite r, Sprite i)
        {
            _id = idx;
            _name = n;
            _rank = r;
            _icon = i;
            _count = 0;
        }
    }
    #endregion [public Utill Class]

    #region [public Utill Struct]
    public struct AudioPlayerDESC
    {
        public AudioSource _player;


        public float _vol
        {
            get => _player.volume;
            set
            {
                if (value < 0)
                {
                    _player.volume = 0;
                    _player.mute = true;
                }
                else if (value > 1)
                {
                    _player.volume = 1;
                    _player.mute = false;
                }
                else
                {
                    _player.volume = value;
                    _player.mute = false;
                }
            }
        }
        public bool _mute
        {
            get => _player.mute;
            set => _player.mute = value;
        }
        public bool _loop
        {
            get => _player.loop;
            set => _player.loop = value;
        }


        public AudioPlayerDESC(AudioSource audios, float vol, bool mute, bool loop = true)
        {
            _player = audios;
            _player.playOnAwake = false;

            _player.volume = vol;
            _player.mute = mute;
            _player.loop = loop;
        }
    }
    public struct StageClearInfo
    {
        public float _condition1;
        public float _condition2;


        public StageClearInfo(float con1, float con2)
        {
            _condition1 = con1;
            _condition2 = con2;
        }
    }

    public struct StageInfo
    {
        public string _stageName;
        public string _mapName;
        public float _penaltyTime;
        public int _cardCount;
        public Queue<int> _monIndexList;
        public int _rewardXP;
        public StageClearInfo _clearCon;


        public StageInfo(string name, string map, float time, int cnt, int xp, int con1, int con2, params int[] idx)
        {
            _stageName = name;
            _mapName = map;
            _penaltyTime = time;
            _cardCount = cnt;
            _rewardXP = xp;
            _clearCon = new StageClearInfo(con1, con2);
            _monIndexList = new Queue<int>(idx.Length);
            for (int i = 0; i < idx.Length; i++)
                _monIndexList.Enqueue(idx[i]);
        }
    }
    #endregion [public Utill Struct]
}

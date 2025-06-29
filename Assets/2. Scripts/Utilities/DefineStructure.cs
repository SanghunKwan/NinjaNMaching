using System.Collections.Generic;
using UnityEngine;

namespace DefineStructure
{
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
}

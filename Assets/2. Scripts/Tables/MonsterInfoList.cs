using UnityEngine;


public class MonsterInfoList : TableBase
{
    public enum Index
    {
        Index,
        MonsterName,
        HP,
        Att,
        Def,
        Grade,
        PerCount,
        PrefabName,

        Max
    }

    public override void Load(string txtData)
    {
        string[] record = txtData.Split("\r\n");

        for (int i = 0; i < record.Length; i++)
        {
            string[] values = record[i].Split("|");
            if (values.Length != (int)Index.Max)
                Debug.LogErrorFormat("MonsterInfoList의 컬럼 수가 맞지 않습니다");

            for (int j = 0; j < values.Length; j++)
            {
                Add(values[0], ((Index)j).ToString(), values[j]);
            }
        }
    }
}

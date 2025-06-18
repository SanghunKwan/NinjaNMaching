using UnityEngine;

public class MonsterSpawnList : TableBase
{
    public enum Index
    {
        Index,
        MonsterCount,
        MonsterIndex1,
        MonsterIndex2,
        MonsterIndex3,
        MonsterIndex4,
        MonsterIndex5,
        MonsterIndex6,
        MonsterIndex7,
        MonsterIndex8,
        MonsterIndex9,
        MonsterIndex10,

        Max
    }

    public override void Load(string txtData)
    {
        string[] record = txtData.Split("\r\n");

        for (int i = 0; i < record.Length; i++)
        {
            string[] values = record[i].Split("|");
            if (values.Length != (int)Index.Max)
                Debug.LogErrorFormat("MonsterSpawnList의 컬럼 수가 맞지 않습니다");

            for (int j = 0; j < values.Length; j++)
            {
                Add(values[0], ((Index)j).ToString(), values[j]);
            }
        }
    }
}

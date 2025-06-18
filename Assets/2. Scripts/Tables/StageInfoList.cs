using UnityEngine;

public class StageInfoList : TableBase
{

    public enum Index
    {
        Index,
        StageName,
        CardCount,
        LimitTime,
        MapName,
        SpawnIndex,
        Condition1,
        Condition2,
        AccquisitionXP,

        Max
    }

    public override void Load(string txtData)
    {
        string[] record = txtData.Split("\r\n");

        for (int i = 0; i < record.Length; i++)
        {
            string[] values = record[i].Split("|");
            if (values.Length != (int)Index.Max)
                Debug.LogErrorFormat("StageInfoList의 컬럼 수가 맞지 않습니다 {0}", values.Length);

            for (int j = 0; j < values.Length; j++)
            {
                Add(values[0], ((Index)j).ToString(), values[j]);
            }
        }
    }
}

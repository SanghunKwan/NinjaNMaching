using UnityEngine;

public class LevelInfoList : TableBase
{
    public enum Index
    {
        Index,
        HP,
        Attack,
        Defence,
        XP,

        Max
    }

    public override void Load(string txtData)
    {
        string[] record = txtData.Split("\r\n");

        for (int i = 0; i < record.Length; i++)
        {
            string[] values = record[i].Split("|");
            if (values.Length != (int)Index.Max)
                Debug.LogErrorFormat("LevelInfoList �÷� ���� ���� �ʽ��ϴ� {0}", values.Length);

            for (int j = 0; j < values.Length; j++)
            {
                Add(values[0], ((Index)j).ToString(), values[j]);
            }
        }
    }
}

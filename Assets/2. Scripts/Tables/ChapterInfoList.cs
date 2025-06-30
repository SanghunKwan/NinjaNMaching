using UnityEngine;

public class ChapterInfoList : TableBase
{
    public enum Index
    {
        Index,
        TitleName,
        MapName,
        RootName,

        Max
    }


    public override void Load(string txtData)
    {
        string[] record = txtData.Split("\r\n");

        for (int i = 0; i < record.Length; i++)
        {
            string[] values = record[i].Split("|");
            if (values.Length != (int)Index.Max)
                Debug.LogErrorFormat("ChapterInfoList �÷� ���� ���� �ʽ��ϴ� {0}", values.Length);

            for (int j = 0; j < values.Length; j++)
            {
                Add(values[0], ((Index)j).ToString(), values[j]);
            }
        }
    }

}

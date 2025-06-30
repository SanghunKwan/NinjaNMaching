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
        string[] records = txtData.Split("\r\n");
    }

}

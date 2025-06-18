using UnityEngine;

public class Test : MonoBehaviour
{
    private void Awake()
    {
        GameTableManager._instance.AllLoadTable();
        ResourcePoolManager._instance.AllLoad();

        TableBase tb = GameTableManager._instance.Get(DefineEnum.InfoTableName.MonsterSpawnList);
        string txt = tb.ToStr(1, "MonsterIndex1");
        Debug.Log(txt);
    }
}

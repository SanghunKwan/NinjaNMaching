using UnityEngine;
using DefineEnum;
using System.Collections.Generic;

public class GameTableManager : TSingleton<GameTableManager>
{
    Dictionary<InfoTableName, TableBase> _tableDoc;

    protected override void Init()
    {
        base.Init();
        _tableDoc = new Dictionary<InfoTableName, TableBase>();
    }

    TableBase Load<T>(InfoTableName name) where T : TableBase, new()
    {
        if (_tableDoc.ContainsKey(name))
        {
            TableBase tBase = _tableDoc[name];
            return tBase;
        }

        TextAsset tAsset = Resources.Load("Tables/" + name.ToString()) as TextAsset;
        if (tAsset != null)
        {
            T t = new T();
            t.Load(tAsset.text);
            _tableDoc.Add(name, t);
        }
        else
        {
            Debug.LogFormat("{0}이 Resources/Tables 에 없습니다.", name);
            return null;
        }

        return _tableDoc[name];
    }

    public void AllLoadTable()
    {
        Load<MonsterInfoList>(InfoTableName.MonsterInfoList);
        Load<MonsterSpawnList>(InfoTableName.MonsterSpawnList);
        Load<StageInfoList>(InfoTableName.StageInfoList);
        Load<LevelInfoList>(InfoTableName.LevelInfoList);
    }
    public TableBase Get(InfoTableName name)
    {
        if (_tableDoc.ContainsKey(name))
            return _tableDoc[name];

        return null;
    }
}

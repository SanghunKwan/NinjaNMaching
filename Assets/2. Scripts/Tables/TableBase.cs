using System.Collections.Generic;
using UnityEngine;

public abstract class TableBase
{
    Dictionary<string, Dictionary<string, string>> _tableDatas;

    public int _recordCount => _tableDatas.Count;

    protected TableBase()
    {
        _tableDatas = new Dictionary<string, Dictionary<string, string>>();
    }

    protected void Add(string index, string column, string val)
    {
        if (!_tableDatas.ContainsKey(index))
            _tableDatas.Add(index, new Dictionary<string, string>());

        if (!_tableDatas[index].ContainsKey(column))
            _tableDatas[index].Add(column, val);
        else
            Debug.LogErrorFormat("Index[{0}], Column[{1}](이/가) 같은 자료가 있습니다.", index, column);
    }

    public string ToStr(string index, string column)
    {
        string findValue = string.Empty;
        if (_tableDatas.ContainsKey(index))
            _tableDatas[index].TryGetValue(column, out findValue);

        return findValue;
    }
    public string ToStr(int index, string column) => ToStr(index.ToString(), column);
    public int ToInt(string index, string column)
    {
        string findValue = ToStr(index, column);
        int val = 0;

        if (int.TryParse(findValue, out val))
            return val;
        else
            return int.MinValue;
    }
    public int ToInt(int index, string column) => ToInt(index.ToString(), column);
    public float ToFloat(string index, string column)
    {
        string findValue = ToStr(index, column);
        float val = 0;

        if (float.TryParse(findValue, out val))
            return val;
        else
            return float.MinValue;
    }
    public float ToFloat(int index, string column) => ToFloat(index.ToString(), column);
    public abstract void Load(string txtData);

}

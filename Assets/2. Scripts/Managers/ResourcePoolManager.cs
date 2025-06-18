using System.Collections.Generic;
using UnityEngine;
using DefineEnum;

public class ResourcePoolManager : TSingleton<ResourcePoolManager>
{
    Dictionary<PoolDataType, Dictionary<string, object>> _allPooldatas;

    public void AllLoad()
    {
        _allPooldatas = new Dictionary<PoolDataType, Dictionary<string, object>>();

        LoadCardBG();
        LoadCardIcon();
    }

    void LoadCardBG()
    {
        //TextAsset tAsset = Resources.Load("Tables/CardBGImageList") as TextAsset;

        //string[] bgNames = tAsset.text.Split(",");

        Dictionary<string, object> cardBGs = new Dictionary<string, object>();
        Sprite[] img = Resources.LoadAll<Sprite>("Images/CardImages/BackImages");
        int length = img.Length;
        for (int i = 0; i < length; i++)
        {
            cardBGs.Add(i.ToString(), img[i]);
        }
        _allPooldatas.Add(PoolDataType.CARDIMAGEBG, cardBGs);
    }
    void LoadCardIcon()
    {
        TextAsset tAsset = Resources.Load("Tables/CardIconImageList") as TextAsset;
        string[] iconNames = tAsset.text.Split(",");

        Dictionary<string, object> iconImages = new Dictionary<string, object>();
        int length = iconNames.Length;
        for (int i = 0; i < length; i++)
        {
            Sprite iconImage = Resources.Load<Sprite>($"Images/FantasyIcon/{iconNames[i]}");
            iconImages.Add(i.ToString(), iconImage);
        }
        _allPooldatas.Add(PoolDataType.CARDIMAGEICON, iconImages);
    }

    public T Get<T>(PoolDataType type, string index)
    {
        if (!_allPooldatas.ContainsKey(type)) return default(T);
        if (!_allPooldatas[type].ContainsKey(index)) return default(T);

        return (T)_allPooldatas[type][index];
    }

    public T Get<T>(PoolDataType type, int index)
    {
        return Get<T>(type, index.ToString());
    }

}

using System.Collections.Generic;
using UnityEngine;
using DefineEnum;
using System.Collections;

public class IngameManager : MonoBehaviour
{
    static IngameManager _uniqueInstance;

    const int _limitWidth = 6;
    Vector2 _offset = new Vector2(1.6f, -2.1f);


    //���� ����
    GameObject _prefabCard;
    Transform _cardRoot;

    //���� ����
    int _firstIndex = -1;
    int _secondIndex = -1;
    int _choiceCount;

    //Datas
    List<CardObj> _genCardList;
    public bool _isChoiceEnd => _choiceCount == 2;

    public static IngameManager _instance => _uniqueInstance;

    private void Awake()
    {
        _uniqueInstance = this;
    }
    private void Start()
    {
        _prefabCard = Resources.Load<GameObject>("Prefabs/Objects/CardObject");
        GameObject go = GameObject.FindGameObjectWithTag("PosRoot");
        _cardRoot = go.transform;
        StartCoroutine(GenerateCard(30, 0.10f));
    }
    private void Update()
    {
        if (_choiceCount == 2)
        {
            if (_genCardList[_firstIndex]._isOpened && _genCardList[_secondIndex]._isOpened)
            {
                if (MatchedCard(_firstIndex, _secondIndex))
                {
                    //����
                    Debug.LogFormat("{0}�� ī��� {1}�� ī�尡 �����Ǿ����ϴ�.", _firstIndex, _secondIndex);

                    Destroy(_genCardList[_firstIndex].gameObject);
                    Destroy(_genCardList[_secondIndex].gameObject);

                    _choiceCount = 0;
                    _firstIndex = _secondIndex = -1;
                }
                else
                {
                    _genCardList[_firstIndex].CloseCard();
                    _genCardList[_secondIndex].CloseCard();
                }
            }
            else if (_genCardList[_firstIndex]._isClosed && _genCardList[_secondIndex]._isClosed)
            {
                _choiceCount = 0;
                _firstIndex = _secondIndex = -1;
            }
        }
    }

    //ī�� ���� �Լ� ����
    IEnumerator GenerateCard(int count, float delay)
    {
        _genCardList = new List<CardObj>();
        int[] iconNumbers = GetShuffleCard(count / 2);

        int idxX;
        int idxY;
        Vector2 pos;
        GameObject go;
        CardObj obj;

        for (int i = 0; i < count; i++)
        {
            idxX = i % _limitWidth;
            idxY = i / _limitWidth;
            pos = new Vector2(idxX * _offset.x, idxY * _offset.y);

            go = Instantiate(_prefabCard, pos, Quaternion.identity, _cardRoot);
            go.transform.localPosition = pos;
            obj = go.GetComponent<CardObj>();
            obj.InitSetData(i, CardBGKind.CARD_BG_SUN, (CardIconType)iconNumbers[i]);
            _genCardList.Add(obj);

            yield return new WaitForSeconds(delay);
        }
    }
    int[] GetShuffleCard(int typeCount)
    {
        int[] returnCards = new int[typeCount * 2];
        int[] typeIndexList = new int[typeCount];

        int cnt = (int)CardIconType.Max;
        // typeCount��ŭ�� ������ ���Ƿ� �̴´�.
        // 1.
        //for (int i = 0; i < typeIndexList.Length; i++)
        //{
        //    int number = Random.Range(0, cnt);
        //    int index = i;
        //    while (--index >= 0)
        //    {
        //        if (typeIndexList[index] == number)
        //        {
        //            number = Random.Range(0, cnt);
        //            index = i;
        //        }
        //    }
        //    typeIndexList[i] = number;
        //}


        //2.
        int[] typeList = new int[cnt];
        for (int i = 0; i < cnt; i++)
            typeList[i] = i;

        int mix = 3;
        for (int i = 0; i < mix; i++)
        {
            for (int j = 0; j < cnt; j++)
            {
                int index = Random.Range(0, cnt);
                int number = typeList[i];
                typeList[i] = typeList[index];
                typeList[index] = number;
            }
        }

        for (int i = 0; i < typeIndexList.Length; i++)
        {
            int index = i * 2;
            typeIndexList[i] = typeList[i];
            returnCards[index] = typeIndexList[i];
            returnCards[index + 1] = typeIndexList[i];
        }
        mix = 5;
        for (int i = 0; i < mix; i++)
        {
            for (int j = 0; j < returnCards.Length; j++)
            {
                int index = Random.Range(0, returnCards.Length);
                int number = returnCards[i];
                returnCards[i] = returnCards[index];
                returnCards[index] = number;
            }
        }

        return returnCards;
    }
    bool MatchedCard(int firstIndex, int secondIndex)
    {
        if (_genCardList[firstIndex]._cardType != _genCardList[secondIndex]._cardType) return false;

        return true;
    }

    public void SelectCard(int NO)
    {
        if (_firstIndex == -1)
        {
            _firstIndex = NO;
            _choiceCount = 1;
        }
        else if (_secondIndex == -1)
        {
            if (_firstIndex != _secondIndex)
            {
                _secondIndex = NO;
                _choiceCount = 2;
            }
        }
    }
}

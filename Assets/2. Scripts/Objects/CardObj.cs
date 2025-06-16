using UnityEngine;
using DefineEnum;

public class CardObj : MonoBehaviour
{
    [Header("ī�� ���� Param")]
    [SerializeField] GameObject _frontSide;
    [SerializeField] SpriteRenderer _cardBG;
    [SerializeField] SpriteRenderer _icon;

    int _myNO;
    CardIconType _myType;


    //�ӽ�
    private void Start()
    {
        InitSetData(1, CardBGKind.CARD_BG_VISION, CardIconType.Icon11);
    }
    public void InitSetData(int no, CardBGKind bg, CardIconType type)
    {
        //Transform tf = transform;
        //_frontSide = tf.GetChild(1).gameObject;
        //_icon = _frontSide.transform.GetChild(0).GetComponent<SpriteRenderer>();
        _myNO = no;
        _cardBG.sprite = ResourcePoolManager._instance.Get<Sprite>(PoolDataType.CARDIMAGEBG, (int)bg);

    }
}

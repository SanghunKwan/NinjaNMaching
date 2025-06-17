using UnityEngine;
using DefineEnum;
using System;

public class CardObj : MonoBehaviour
{
    [Header("카드 설정 Param")]
    [SerializeField] GameObject _frontSide;
    [SerializeField] SpriteRenderer _cardBG;
    [SerializeField] SpriteRenderer _icon;

    int _myNO;
    CardIconType _myType;
    public CardIconType _cardType => _myType;
    bool _isFront;
    bool _isRolling;
    public bool _isOpened => (!_isRolling) && _isFront;
    public bool _isClosed => (!_isRolling) && (!_isFront);

    Transform tr;
    [SerializeField] float rollingSpeed;


    //임시
    private void Start()
    {
        tr = transform;
    }
    private void Update()
    {
        if (_isRolling)
        {
            float rollingDelta = rollingSpeed * Time.deltaTime;
            float angleY = tr.rotation.eulerAngles.y;
            float pageHalf = Convert.ToInt32(!_isFront) * 180;

            tr.Rotate(Vector3.up, rollingDelta);

            if (angleY > 90 + pageHalf)
                _frontSide.SetActive(_isFront);

            if (_isFront ? (angleY > 180) : (angleY < 180))
            {
                _isRolling = false;
                tr.rotation = Quaternion.Euler(0, pageHalf + 180, 0);
            }

            ////(rollingDelta / 2) 변수로.
            //if (_isFront)
            //{
            //    if (tr.rotation.eulerAngles.y > 90)
            //        _frontSide.SetActive(true);

            //    if (tr.rotation.eulerAngles.y > 180)
            //    {
            //        _isRolling = false;
            //        tr.rotation = Quaternion.Euler(0, 180, 0);
            //    }
            //}
            //else
            //{
            //    if (tr.rotation.eulerAngles.y > 270)
            //        _frontSide.SetActive(false);

            //    if (tr.rotation.eulerAngles.y < 180)
            //    {
            //        _isRolling = false;
            //        tr.rotation = Quaternion.Euler(0, 0, 0);
            //    }
            //}




            //if (angleY > 90 - (rollingDelta / 2) && angleY < 90 + (rollingDelta / 2))
            //{
            //    _isFront = !_isFront;
            //    _frontSide.SetActive(_isFront);
            //}
            //else if (angleY > 180 - (rollingDelta / 2) || angleY < (rollingDelta / 2))
            //{
            //    _isRolling = false;
            //    tr.rotation = Quaternion.Euler(0, Mathf.Ceil(tr.rotation.eulerAngles.y), 0);
            //}


            //if (_isFront == _wasFront)
            //{
            //    //회전
            //    if (tr.rotation.eulerAngles.y < 89)
            //    {
            //        tr.Rotate(Vector3.up, rollingDelta);
            //    }
            //    else
            //    {
            //        _isFront = !_isFront;
            //        _frontSide.SetActive(_isFront);
            //    }
            //}
            //else
            //{
            //    if (tr.rotation.eulerAngles.y > rollingDelta)
            //    {
            //        tr.Rotate(Vector3.up, -rollingDelta);
            //    }
            //    else
            //    {
            //        _isRolling = false;
            //        tr.rotation = Quaternion.Euler(0, 0, 0);
            //    }
            //}
        }
    }

    public void InitSetData(int no, CardBGKind bg, CardIconType type)
    {
        //Transform tf = transform;
        //_frontSide = tf.GetChild(1).gameObject;
        //_icon = _frontSide.transform.GetChild(0).GetComponent<SpriteRenderer>();
        _myNO = no;
        _myType = type;
        _cardBG.sprite = ResourcePoolManager._instance.Get<Sprite>(PoolDataType.CARDIMAGEBG, (int)bg);
        _icon.sprite = ResourcePoolManager._instance.Get<Sprite>(PoolDataType.CARDIMAGEICON, (int)type);
        _frontSide.SetActive(false);
        _isFront = false;
        _isRolling = false;
    }

    public void OpenCard()
    {
        _isRolling = true;
        _isFront = true;
    }
    public void CloseCard()
    {
        _isRolling = true;
        _isFront = false;
    }

    private void OnMouseDown()
    {
        if (_isRolling || IngameManager._instance._isChoiceEnd) return;

        OpenCard();
        IngameManager._instance.SelectCard(_myNO);
    }
}

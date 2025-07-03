using UnityEngine;
using UnityEngine.UI;

public class UIMovingVedeo : MonoBehaviour
{
    [SerializeField] float _startPos = 1620;
    [SerializeField] float _endPos = -540;
    [SerializeField] float _moveSpeed = 5;

    bool _isOn;

    RectTransform _myRt;


    private void Update()
    {
        if (_isOn)
        {
            if (_myRt.anchoredPosition.x <= _endPos)
            {
                float returnDistance = _startPos - _endPos;
                _myRt.anchoredPosition += Vector2.right * returnDistance;
            }
            _myRt.anchoredPosition += Vector2.left * _moveSpeed * Time.deltaTime;
        }
    }

    public void InitMoving(Sprite img)
    {
        Image video = GetComponent<Image>();
        video.sprite = img;

        _isOn = true;
        _myRt = video.rectTransform;
    }
    public void Release()
    {
        _isOn = false;
    }
}

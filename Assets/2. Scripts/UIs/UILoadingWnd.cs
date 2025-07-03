using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UILoadingWnd : MonoBehaviour
{
    const int _videoPerCount = 2;
    const string _defaultLoadingString = "Loading";

    [Header("내부 Component")]
    [SerializeField] UIMovingVedeo[] _moveVedeo;
    [SerializeField] Text _textLoading;
    [SerializeField] Text _textMessage;
    [SerializeField] Slider _sliderLoading;
    [SerializeField] Animator _animator;

    [SerializeField] float _dotChangeTime;
    [SerializeField] float _loadingMaxTime;
    float _accumTime;

    int _currentDot;

    [Header("리소스 자료")]
    [SerializeField] Sprite[] _videoImages;
    [SerializeField] string[] _textTipList;



    public void CloseWnd()
    {
        gameObject.SetActive(false);
        foreach (var item in _moveVedeo)
        {
            item.Release();
        }
    }
    public void OpenWnd()
    {
        gameObject.SetActive(true);
        int tempRandom = Random.Range(0, _videoImages.Length);

        //이미지 설정 및 동작.
        foreach (var item in _moveVedeo)
        {
            item.InitMoving(_videoImages[tempRandom]);
        }

        //tip 랜덤.
        tempRandom = Random.Range(0, _textTipList.Length);
        _textMessage.text = _textTipList[tempRandom];

        //닌자 달리기 애니메이션 넣기.
    }



    void CheckDot(string defaultString)
    {
        _textLoading.text = defaultString;

        for (int i = 0; i < _currentDot; i++)
        {
            _textLoading.text += ".";
        }
        _currentDot++;
        _currentDot %= 6;
    }
    private void Update()
    {
        //0.4초마다 . 추가.
        _accumTime += Time.deltaTime;
        if (_accumTime >= _dotChangeTime)
        {
            _accumTime -= _dotChangeTime;
            CheckDot(_defaultLoadingString);
        }
    }
    public void SetLoadingRate(float num)
    {
        _sliderLoading.value = num;
    }
}

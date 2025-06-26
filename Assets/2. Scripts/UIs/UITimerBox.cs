using UnityEngine;
using UnityEngine.UI;

public class UITimerBox : MonoBehaviour
{
    [SerializeField] Text _textSeconds;
    [SerializeField] Text _textMiliSeconds;

    public void CloseBox()
    {
        gameObject.SetActive(false);
    }
    public void OpenBox()
    {
        gameObject.SetActive(true);
        _textSeconds.text = "0";
        _textMiliSeconds.text = "00";
    }
    public void SetNowTime(float time)
    {
        int sec = (int)time;
        int msec = (int)((time - sec) * 100);

        _textSeconds.text = sec < 10 ? "0" + sec.ToString() : sec.ToString();
        _textMiliSeconds.text = msec < 10 ? "0" + msec.ToString() : msec.ToString();
    }
}

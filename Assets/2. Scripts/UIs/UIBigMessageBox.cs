using DefineEnum;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIBigMessageBox : MonoBehaviour
{
    [SerializeField] Text _txtMessage;

    MessageType _type;

    public void CloseBox()
    {
        gameObject.SetActive(false);
    }

    public void OpenBox(in string msg, MessageType t = MessageType.Normal, float time = 0)
    {
        gameObject.SetActive(true);

        _txtMessage.text = msg;
        _type = t;

        if (_type == MessageType.Timer)
        {
            StartCoroutine(CheckTimer(time, CloseBox));
        }
        if (_type == MessageType.Movement)
        {
            //이동하는 코루틴 생성 및 이동 코루틴 중단 시부터 시간 측정.
            StartCoroutine(FadeAnimation(time));
        }
    }
    IEnumerator CheckTimer(float timer, Action action)
    {
        yield return new WaitForSeconds(timer);

        action();
    }
    IEnumerator FadeAnimation(float timer)
    {
        RectTransform rectTransform = (RectTransform)transform;

        yield return Fade(rectTransform.anchoredPosition + Vector2.right * Screen.width, Vector2.zero, 0.01f, 1.001f, 0.02f);

        yield return new WaitForSeconds(timer);

        yield return Fade(Vector2.zero, Vector2.left * Screen.width, 0.01f, 1.001f, Screen.width / 2);
        CloseBox();
    }

    IEnumerator Fade(Vector2 startPos, Vector2 endPos, float lerpT, float multiple, float sqrMag)
    {
        RectTransform rectTransform = (RectTransform)transform.GetChild(0).GetChild(0);
        rectTransform.anchoredPosition = startPos;
        while (Vector2.SqrMagnitude(rectTransform.anchoredPosition - endPos) > sqrMag)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, endPos, lerpT);
            lerpT = Mathf.Min(1, multiple * lerpT);
            yield return null;
        }
    }
}

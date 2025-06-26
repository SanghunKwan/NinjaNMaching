using UnityEngine;
using UnityEngine.UI;

public class ButtonUtils : MonoBehaviour
{
    [SerializeField] float _offset = 10;

    [SerializeField] Color _downColor;
    Color _originalColor;
    public void ButtonDownAction(RectTransform textRect)
    {
        Text btnText = textRect.GetComponent<Text>();
        _originalColor = btnText.color;
        btnText.color = _downColor;
        textRect.anchoredPosition += Vector2.down * _offset;
    }
    public void ButtonUpAction(RectTransform textRect)
    {
        Text btnText = textRect.GetComponent<Text>();
        btnText.color = _originalColor;
        textRect.anchoredPosition += Vector2.up * _offset;
    }
}

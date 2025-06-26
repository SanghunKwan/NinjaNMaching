using UnityEngine;
using UnityEngine.UI;

public class UIMatchInfoBox : MonoBehaviour
{
    [SerializeField] Text _textMatchCount;
    [SerializeField] Text _textMissMatchCount;


    public void CloseBox()
    {
        gameObject.SetActive(false);
    }
    public void OpenBox()
    {
        gameObject.SetActive(true);
        _textMissMatchCount.text = _textMatchCount.text = "0";
    }
    public void SetMatchCount(int val)
    {
        _textMatchCount.text = val.ToString();
    }
    public void SetMissMatchCount(int val)
    {
        _textMissMatchCount.text = val.ToString();
    }
}

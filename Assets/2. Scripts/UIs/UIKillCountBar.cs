using UnityEngine;
using UnityEngine.UI;

public class UIKillCountBar : MonoBehaviour
{
    [SerializeField] Image _rankIcon;
    [SerializeField] Image _monsterIcon;
    [SerializeField] Text _textMonsterName;
    [SerializeField] Text _textKillCounts;

    public void CloseBar()
    {
        gameObject.SetActive(false);
    }
    public void OpenBar(Sprite rank, Sprite mon, string name, int count)
    {
        gameObject.SetActive(true);
        _rankIcon.sprite = rank;
        _monsterIcon.sprite = mon;
        _textMonsterName.text = name;
        _textKillCounts.text = count.ToString();
    }
}

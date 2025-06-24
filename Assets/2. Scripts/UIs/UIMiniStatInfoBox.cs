using UnityEngine;
using UnityEngine.UI;

public class UIMiniStatInfoBox : MonoBehaviour
{
    [SerializeField] Text _textName;
    [SerializeField] Text _textAttack;
    [SerializeField] Text _textDefence;
    [SerializeField] Slider _barHP;
    [SerializeField] Slider _barXP;

    public void CloseBox()
    {
        gameObject.SetActive(false);
    }
    public void OpenBox(in string name, int att, int def, float xp)
    {
        gameObject.SetActive(true);
        _textName.text = name;
        _textAttack.text = att.ToString();
        _textDefence.text = def.ToString();

        _barHP.value = 1;
        _barXP.value = xp;
    }
    public void SetHPRate(float rate)
    {
        _barHP.value = rate;
    }
    public void SetXPRate(float rate)
    {
        _barXP.value = rate;
    }
}

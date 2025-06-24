using DefineEnum;
using UnityEngine;
using UnityEngine.UI;

public class UIMiniInfoMonsterBox : MonoBehaviour
{
    [SerializeField] Text _txtName;
    [SerializeField] Image _iconGrade;
    [SerializeField] Text _txtAtt;
    [SerializeField] Text _txtDef;
    [SerializeField] Text _txtPerCount;
    [SerializeField] Slider _barHP;

    public void CloseBox()
    {
        gameObject.SetActive(false);
    }
    public void OpenBox(in string name, int att, int def, float per, MonsterGrade grade)
    {
        gameObject.SetActive(true);
        _txtName.text = name;
        _txtAtt.text = att.ToString();
        _txtDef.text = def.ToString();
        _txtPerCount.text = per.ToString();
        _iconGrade.sprite = IngameManager._instance.GetIconFromMonsterGrade(grade);

        _barHP.value = 1;
    }

    public void SetHPRate(float rate)
    {
        _barHP.value = rate;
    }
}

using DefineEnum;
using UnityEngine;
using UnityEngine.UI;

public class UIMonsterInfoBar : MonoBehaviour
{
    [SerializeField] Image _iconMonGrade;
    [SerializeField] Image _iconMonHead;
    [SerializeField] Text _textMonName;

    //initBar
    public void InitBar(Sprite gradeIcon, Sprite headIcon, in string name)
    {
        //���� ��� �̹���
        _iconMonGrade.sprite = gradeIcon;
        //���� ������
        _iconMonHead.sprite = headIcon;
        //���� �̸�
        _textMonName.text = name;
    }
}

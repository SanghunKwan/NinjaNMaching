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
        //몬스터 등급 이미지
        _iconMonGrade.sprite = gradeIcon;
        //몬스터 아이콘
        _iconMonHead.sprite = headIcon;
        //몬스터 이름
        _textMonName.text = name;
    }
}

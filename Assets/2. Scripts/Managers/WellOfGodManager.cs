using UnityEngine;
using DefineEnum;
using UnityEngine.UI;
using Unity.VisualScripting;

public class WellOfGodManager : MonoBehaviour
{
    static WellOfGodManager _uniqueInstance;

    [Header("리소스 자료")]
    [SerializeField] Sprite[] _stageBtnIconList;
    [SerializeField] Sprite[] _stageRewardIconList;



    //UIs
    UIWellViewBox _veiwBox;

    public static WellOfGodManager _instance => _uniqueInstance;
    public Sprite GetStageIcon(BtnState state) => _stageBtnIconList[(int)state];
    public Sprite GetRewardIcon(StageState state) => _stageRewardIconList[(int)state];



    private void Awake()
    {
        _uniqueInstance = this;
    }


    public void InitManager(int chapterNum)
    {
        GameObject go = GameObject.FindGameObjectWithTag("WellView");
        _veiwBox = go.GetComponent<UIWellViewBox>();

        _veiwBox.InitBox(chapterNum);
    }

}

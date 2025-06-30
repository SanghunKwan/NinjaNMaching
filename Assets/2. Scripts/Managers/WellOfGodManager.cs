using UnityEngine;
using DefineEnum;

public class WellOfGodManager : MonoBehaviour
{
    static WellOfGodManager _uniqueInstance;
    [Header("리소스 자료")]
    [SerializeField] Sprite[] _stageBtnIconList;
    [SerializeField] Sprite[] _stageRewardIconList;


    public static WellOfGodManager _instance => _uniqueInstance;
    public Sprite GetStageIcon(BtnState state) => _stageBtnIconList[(int)state];
    public Sprite GetRewardIcon(StageState state) => _stageRewardIconList[(int)state];



    private void Awake()
    {
        _uniqueInstance = this;
    }


}

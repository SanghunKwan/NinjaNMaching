using DefineEnum;
using UnityEngine;
using UnityEngine.UI;

public class UIStageInfoBtn : MonoBehaviour
{
    [SerializeField] Image _btnIcon;
    [SerializeField] Text _textStageNum;
    [SerializeField] Image[] _rewardStars;
    [SerializeField] Image _lockIcon;

    UIWellViewBox _owerBox;

    BtnState _nowState;

    /// <summary>
    /// �������� ��ư�� �ʱ� ����.
    /// </summary>
    /// <param name="number">���������� ��ȣ</param>
    /// <param name="clearStage">�����带 ���� ���������� ���� ��ȣ</param>
    /// <param name="rewardRank">�ش� ���������� ������ ��� 1, 2, 3�̴�.</param>
    public void InitBtn(int number, int clearStage, int rewardRank, UIWellViewBox owner)
    {
        _owerBox = owner;
        _textStageNum.text = number.ToString();
        _nowState = BtnState.Normal;
        _btnIcon.sprite = WellOfGodManager._instance.GetStageIcon(_nowState);

        if (number - 1 > clearStage)
        {
            _lockIcon.enabled = true;

            foreach (Image item in _rewardStars)
                item.sprite = WellOfGodManager._instance.GetRewardIcon(StageState.NotClear);
        }
        else
        {
            _lockIcon.enabled = false;

            for (int i = 0; i < _rewardStars.Length; i++)
            {
                if (i < rewardRank)
                    _rewardStars[i].sprite = WellOfGodManager._instance.GetRewardIcon(StageState.GetReward);
                else
                    _rewardStars[i].sprite = WellOfGodManager._instance.GetRewardIcon(StageState.NoReward);
            }
        }
    }
    public void SetBtnToNormal()
    {
        _btnIcon.sprite = WellOfGodManager._instance.GetStageIcon(BtnState.Normal);
        _nowState = BtnState.Normal;
        _btnIcon.raycastTarget = true;
    }
    public void ClickStageButton()
    {
        if (_nowState == BtnState.Select) return;

        //��ü ��ư�� ���� Normal ȭ�� �ؾ���.
        _owerBox.AllCancel();

        _nowState = BtnState.Select;
        _btnIcon.sprite = WellOfGodManager._instance.GetStageIcon(_nowState);
        _btnIcon.raycastTarget = false;
    }
}

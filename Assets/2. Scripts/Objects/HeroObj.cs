using UnityEngine;
using DefineEnum;

public class HeroObj : MonoBehaviour
{
    Animator _aniController;

    AniActionState _state;

    //юс╫ц
    private void Awake()
    {
        InitSet();
    }

    public void InitSet()
    {
        _aniController = GetComponent<Animator>();
    }

    public void ExchangeAniToAction(AniActionState state)
    {
        switch (state)
        {
            case AniActionState.IDLE:
                _aniController.SetBool("IsEntry1", false);
                _aniController.SetBool("IsEntry2", false);
                break;
            case AniActionState.ENTRY1:
                _aniController.SetBool("IsEntry1", true);
                break;
            case AniActionState.ENTRY2:
                _aniController.SetBool("IsEntry2", true);
                break;

            case AniActionState.ATTACK1:
                _aniController.SetTrigger("Attack1");
                break;
            case AniActionState.ATTACK2:
                _aniController.SetTrigger("Attack2");
                break;
            case AniActionState.SPECIAL1:
                _aniController.SetTrigger("Skill1");
                break;
            case AniActionState.SPECIAL2:
                _aniController.SetTrigger("Skill2");
                break;
            case AniActionState.HIT:
                _aniController.SetTrigger("Hit");
                break;
            case AniActionState.DIE:
                _aniController.SetTrigger("Dead");
                break;
        }

        _state = state;
    }

    //public void OnGUI()
    //{
    //    if (GUI.Button(new Rect(Screen.width - 200, 0, 200, 40), "IDLE"))
    //    {
    //        ExchangeAniToAction(AniActionState.IDLE);
    //    }
    //    if (GUI.Button(new Rect(Screen.width - 200, 50, 200, 40), "Entry1"))
    //    {
    //        ExchangeAniToAction(AniActionState.ENTRY1);
    //    }
    //    if (GUI.Button(new Rect(Screen.width - 200, 100, 200, 40), "Entry2"))
    //    {
    //        ExchangeAniToAction(AniActionState.ENTRY2);
    //    }
    //    if (GUI.Button(new Rect(Screen.width - 200, 150, 200, 40), "attack1"))
    //    {
    //        ExchangeAniToAction(AniActionState.ATTACK1);
    //    }
    //    if (GUI.Button(new Rect(Screen.width - 200, 200, 200, 40), "attack2"))
    //    {
    //        ExchangeAniToAction(AniActionState.ATTACK2);
    //    }
    //    if (GUI.Button(new Rect(Screen.width - 200, 250, 200, 40), "Special1"))
    //    {
    //        ExchangeAniToAction(AniActionState.SPECIAL1);
    //    }
    //    if (GUI.Button(new Rect(Screen.width - 200, 300, 200, 40), "Special2"))
    //    {
    //        ExchangeAniToAction(AniActionState.SPECIAL2);
    //    }
    //    if (GUI.Button(new Rect(Screen.width - 200, 350, 200, 40), "hit"))
    //    {
    //        ExchangeAniToAction(AniActionState.HIT);
    //    }
    //    if (GUI.Button(new Rect(Screen.width - 200, 400, 200, 40), "dead"))
    //    {
    //        ExchangeAniToAction(AniActionState.DIE);
    //    }
    //}
}

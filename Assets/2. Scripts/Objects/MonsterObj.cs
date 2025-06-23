using UnityEngine;
using DefineEnum;
using System.Collections;
using System.Net;

public class MonsterObj : MonoBehaviour
{
    Animator _aniController;

    AniActionState _state;
    float _speed;

    public void InitSet(Transform goalRoot)
    {
        _aniController = GetComponent<Animator>();

        PlayEntry(goalRoot);
    }

    public void PlayEntry(Transform goalRoot)
    {
        Vector3 startPos;
        Vector3 endPos = goalRoot.position;
        int num = Random.Range(0, 2);
        if (num == 0)
        {
            startPos = goalRoot.GetChild(0).position;
        }
        else
        {
            startPos = goalRoot.GetChild(1).position;
        }

        _speed = Vector3.Distance(startPos, endPos) / _aniController.GetCurrentAnimatorStateInfo(0).length;
        transform.position = startPos;

        StartCoroutine(EntryDirecting(num, endPos));
    }
    IEnumerator EntryDirecting(int number, Vector3 ePos)
    {
        Transform myPos = transform;

        if (number == 0)
            ExchangeAniToAction(AniActionState.ENTRY1);
        else
            ExchangeAniToAction(AniActionState.ENTRY2);

        while (myPos.position != ePos)
        {
            myPos.position = Vector3.MoveTowards(myPos.position, ePos, _speed * Time.deltaTime);
            yield return null;
        }

        ExchangeAniToAction(AniActionState.IDLE);

        yield return new WaitForSeconds(0.5f);

        IngameManager._instance.CardDeploy();
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
            case AniActionState.SPECIAL2:
                _aniController.SetTrigger("Skill");
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

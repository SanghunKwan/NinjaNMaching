using UnityEngine;
using DefineEnum;
using System.Collections;

public class MonsterObj : CharBase
{
    //stat 관련 정보
    MonsterGrade _grade;
    float _stdPerCount;

    Animator _aniController;
    UIMiniInfoMonsterBox _uiInfoBox;
    HeroObj _target;

    AniActionState _state;
    float _speed;

    public override int _finalDamage => _attack;

    public override int _finalDefence => _defence;

    public void InitSet(Transform goalRoot, int monIndex, UIMiniInfoMonsterBox box, HeroObj obj)
    {
        _aniController = GetComponent<Animator>();
        _uiInfoBox = box;
        _target = obj;

        //몬스터 정보 설정
        TableBase table = GameTableManager._instance.Get(InfoTableName.MonsterInfoList);
        string name = table.ToStr(monIndex, "MonsterName");
        int att = table.ToInt(monIndex, "Att");
        int def = table.ToInt(monIndex, "Def");
        int hp = table.ToInt(monIndex, "HP");
        _grade = (MonsterGrade)table.ToInt(monIndex, "Grade");
        _stdPerCount = table.ToFloat(monIndex, "PerCount");
        //==
        InitBaseSet(name, att, def, hp);
        PlayEntry(goalRoot);
    }

    public float CalcAttackStartRate(float faildCount)
    {
        if (faildCount >= _stdPerCount)
        {
            faildCount -= _stdPerCount;
            ExchangeAniToAction(AniActionState.ATTACK1);
        }

        _uiInfoBox.SetPerCountRate(1 - (faildCount / _stdPerCount));

        return faildCount;
    }
    public void HittingMon()
    {
        //Debug.Log("흐미~발라버려야~!");
        if (_target.OnHit(_finalDamage))
        {
            IngameManager._instance.EndGame(false);
        }
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
        _uiInfoBox.OpenBox(_name, _attack, _defence, _stdPerCount, _grade);
        yield return new WaitForSeconds(0.5f);

        ExchangeAniToAction(AniActionState.HIT);
        yield return new WaitForSeconds(0.5f);

        IngameManager._instance.CardDeploy();
    }
    public bool OnHit(int finalDamage)
    {
        bool result = false;
        int dam = Mathf.Max(finalDamage - _finalDefence, 1);

        if ((_nowHP -= dam) <= 0)
        {
            _nowHP = 0;
            ExchangeAniToAction(AniActionState.DIE);
            result = true;
        }
        else
        {
            ExchangeAniToAction(AniActionState.HIT);
        }
        _uiInfoBox.SetHPRate(_hpRate);
        return result;
    }
    public void OnDead()
    {
        Destroy(gameObject, 2);
        _uiInfoBox.CloseBox();
        transform.GetChild(1).gameObject.SetActive(false);
    }
    public override void ExchangeAniToAction(AniActionState state)
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

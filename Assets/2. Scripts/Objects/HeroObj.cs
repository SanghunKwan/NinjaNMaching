using UnityEngine;
using DefineEnum;
using System.Collections;

public class HeroObj : CharBase
{
    // stat 관련 정보
    int _level;
    int _nowXP;
    int _targetXP;
    int _prevXP;
    //==

    Animator _aniController;
    SpriteRenderer _model;
    UIMiniStatInfoBox _uiStatBox;
    MonsterObj _target;

    AniActionState _state;
    float _speed;



    public override int _finalDamage => _attack;

    public override int _finalDefence => _defence;

    public float _xpRate
    {
        get
        {
            if (_level >= 10) return 1;

            int requireXp = _targetXP - _prevXP;
            int xp = _nowXP - _prevXP;
            return (float)xp / requireXp;
        }
    }

    IEnumerator EntryDirecting(int number, Vector3 sPos, Vector3 ePos)
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
        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(0.2f);
            _model.flipX = !_model.flipX;
        }
        yield return new WaitForSeconds(0.3f);
        _model.flipX = !_model.flipX;

        _uiStatBox.OpenBox(_name, _attack, _defence, _xpRate);
        ExchangeAniToAction(AniActionState.ENTRY1);
        while (myPos.position != sPos)
        {
            myPos.position = Vector3.MoveTowards(myPos.position, sPos, _speed * Time.deltaTime);
            yield return null;
        }

        ExchangeAniToAction(AniActionState.IDLE);
        _model.flipX = !_model.flipX;

        yield return null;

        IngameManager._instance.Intermission();
    }

    public void InitSet(Transform goalRoot, string name, int level, int xp, UIMiniStatInfoBox box)
    {
        _aniController = GetComponent<Animator>();
        _model = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _uiStatBox = box;

        //Level에 따른 히어로의 스탯 설정.
        TableBase table = GameTableManager._instance.Get(InfoTableName.LevelInfoList);
        int hp = table.ToInt(level, "HP");
        int att = table.ToInt(level, "Attack");
        int def = table.ToInt(level, "Defence");
        _targetXP = table.ToInt(level, "XP");
        _nowXP = xp;
        if (_level > 1)
            _prevXP = table.ToInt(level - 1, "XP");
        else _prevXP = 0;
        //==

        PlayEntry(goalRoot);
        InitBaseSet(name, att, def, hp);

    }
    public void SetTargetMonster(MonsterObj obj)
    {
        _target = obj;
    }

    public void PlayEntry(Transform goalRoot)
    {
        Vector3 spawnPos = goalRoot.GetChild(0).position;
        Vector3 entryPos = goalRoot.GetChild(1).position;

        _speed = (entryPos.x - spawnPos.x) / 1f;
        transform.position = spawnPos;

        StartCoroutine(EntryDirecting(Random.Range(0, 2), goalRoot.position, entryPos));
    }

    public void OrderOfAttack()
    {
        AniActionState state = (Random.Range(0, 2) == 0) ? AniActionState.ATTACK1 : AniActionState.ATTACK2;

        ExchangeAniToAction(state);
    }
    public void HittingMon(int type)            //0 : 일반공격, 1 : 스킬1, 2 : 스킬2
    {
        //Debug.Log("확~때려부려!!");
        int finishDamage = _finalDamage;
        switch (type)
        {
            case 1:
                break;
            case 2:
                break;
        }
        if (_target.OnHit(_finalDamage))
        {
            _target = null;
            IngameManager._instance.DeadDelayTime();
        }
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

    public bool OnHit(int finalDamage)
    {
        bool result = false;
        int dam = Mathf.Max(finalDamage - _finalDefence, 1);

        if ((_nowHP -= dam) <= 0)
        {
            _nowHP = 0;
            ExchangeAniToAction(AniActionState.DIE);
        }
        else
            ExchangeAniToAction(AniActionState.HIT);

        _uiStatBox.SetHPRate(_hpRate);

        return result;
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

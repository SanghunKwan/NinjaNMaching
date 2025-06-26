using DefineEnum;
using UnityEngine;

public abstract class CharBase : MonoBehaviour
{
    protected string _name;
    protected int _hp;
    protected int _attack;
    protected int _defence;
    protected bool _isDead;
    protected int _nowHP;

    public bool _isDeaded => _isDead;
    public int _currentHP => _nowHP;
    public float _hpRate => (float)_nowHP / _hp;

    protected void InitBaseSet(in string name, int att, int def, int hp)
    {
        _isDead = false;
        _name = name;
        _attack = att;
        _defence = def;
        _nowHP = _hp = hp;
    }

    public abstract int _finalDamage { get; }
    public abstract int _finalDefence { get; }

    public abstract void ExchangeAniToAction(AniActionState state);
}

using UnityEngine;

namespace DefineEnum
{
    #region [Manager Define]
    public enum GameState
    {
        INITLOAD,
        READY,
        CARDDEPLOY,
        START,
        PLAYGAME,
        INTERMISSION,
        DEADDELAY,
        ENDGAME,
        RESULTGAME
    }
    public enum InfoTableName
    {
        MonsterInfoList,
        MonsterSpawnList,
        StageInfoList,
        LevelInfoList,

        Max
    }
    public enum PoolDataType
    {
        CARDIMAGEBG,
        CARDIMAGEICON,
    }
    #endregion[Manager Define]

    #region [캐릭터 Define]
    public enum MonsterGrade
    {
        Common,
        Rare,
        Elite,
        Boss
    }
    public enum AniActionState
    {
        IDLE,
        ATTACK1,
        ATTACK2,
        HIT,
        SPECIAL1,
        SPECIAL2,
        ENTRY1,
        ENTRY2,



        DIE = 50,
    }

    #endregion[캐릭터 Define]

    #region [카드 Define]
    public enum CardBGKind
    {
        CARD_BG_NORMAL = 0,
        CARD_BG_POISON = 3,
        CARD_BG_SUN = 17,
        CARD_BG_VISION = 24,
    }

    public enum CardIconType
    {
        Icon1,
        Icon2,
        Icon3,
        Icon4,
        Icon5,
        Icon6,
        Icon7,
        Icon8,
        Icon9,
        Icon10,
        Icon11,
        Icon12,
        Icon13,
        Icon14,
        Icon15,
        Icon16,
        Icon17,
        Icon18,
        Icon19,
        Icon20,
        Icon21,
        Icon22,
        Icon23,
        Icon24,
        Icon25,
        Icon26,
        Icon27,
        Icon28,
        Icon29,
        Icon30,
        Icon31,
        Icon32,
        Icon33,
        Icon34,
        Icon35,
        Icon36,
        Icon37,
        Icon38,
        Icon39,
        Icon40,

        Max
    }
    #endregion[카드 Define]

    #region [UI Define]
    public enum MessageType
    {
        Normal,
        Timer,
        Movement
    }
    #endregion [UI Define]
}

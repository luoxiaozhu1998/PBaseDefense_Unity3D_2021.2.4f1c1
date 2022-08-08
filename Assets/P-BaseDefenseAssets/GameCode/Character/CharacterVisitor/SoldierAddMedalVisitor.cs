using UnityEngine;
using System.Collections;

// 增加Solder勋章
public class SoldierAddMedalVisitor : ICharacterVisitor 
{
    PBaseDefenseGame m_PBDGame = null;

    public SoldierAddMedalVisitor( PBaseDefenseGame PBDGame)
    {
        m_PBDGame = PBDGame;
    }

    public override void VisitSoldier(ISoldier Soldier)
    {
        base.VisitSoldier( Soldier);
        Soldier.AddMedal();

        // 游戏事件
        m_PBDGame.NotifyGameEvent( ENUM_GameEvent.SoldierUpgate, Soldier); 
    }
}
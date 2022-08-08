using UnityEngine;
using System.Collections;

// Soldier单位阵亡
public class SoldierKilledSubject : IGameEventSubject
{
    private    int    m_KilledCount = 0;
    private ISoldier m_Soldier = null;

    public SoldierKilledSubject()
    {}

    // 取得对像
    public ISoldier GetSoldier()
    {
        return m_Soldier;
    }

    // 目前我方单位阵亡数
    public int GetKilledCount()
    {
        return m_KilledCount;
    }

    // 通知我方单位阵亡
    public override void SetParam( System.Object Param )
    {
        base.SetParam( Param);
        m_Soldier = Param as ISoldier;
        m_KilledCount ++;

        // 通知
        Notify();
    }
}
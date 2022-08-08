using UnityEngine;
using System.Collections;

// Soldier升级
public class SoldierUpgateSubject : IGameEventSubject
{
    private    int    m_UpgateCount = 0;
    private ISoldier m_Soldier = null;

    public SoldierUpgateSubject()
    {}

    // 目前升级次数
    public int GetUpgateCount()
    {
        return m_UpgateCount;
    }

    // 通知Soldier单位升级
    public override void SetParam( System.Object Param )
    {
        base.SetParam( Param);
        m_Soldier = Param as ISoldier;
        m_UpgateCount++;

        // 通知
        Notify();
    }

    public ISoldier GetSoldier()
    {
        return m_Soldier;
    }
}
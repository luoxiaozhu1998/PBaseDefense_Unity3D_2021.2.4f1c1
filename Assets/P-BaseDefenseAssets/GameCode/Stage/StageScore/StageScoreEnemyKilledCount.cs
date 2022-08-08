using UnityEngine;
using System.Collections;

// 关卡分数确认:敌人阵亡数
public class StageScoreEnemyKilledCount :  IStageScore
{
    private int m_EnemyKilledCount = 0;
    private StageSystem m_StageSystem = null;
   
    public StageScoreEnemyKilledCount( int KilledCount, StageSystem theStageSystem)
    {
        m_EnemyKilledCount = KilledCount;
        m_StageSystem = theStageSystem;
    }

    // 确认关卡分数是否达成
    public override bool CheckScore()
    {
        return ( m_StageSystem.GetEnemyKilledCount() >=  m_EnemyKilledCount);
    }
}
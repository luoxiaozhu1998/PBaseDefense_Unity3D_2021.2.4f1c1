using UnityEngine;
using System.Collections;

// 关卡分数观测Enemey阵亡事件
public class EnemyKilledObserverStageScore : IGameEventObserver 
{
    private EnemyKilledSubject m_Subject = null;
    private StageSystem    m_StageSystem = null;

    public EnemyKilledObserverStageScore( StageSystem theStageSystem )
    {
        m_StageSystem = theStageSystem;
    }

    // 设定观察的主题
    public override    void SetSubject( IGameEventSubject Subject )
    {
        m_Subject = Subject as EnemyKilledSubject;
    }

    // 通知Subject被更新
    public override void Update()
    {
        //Debug.Log("EnemyKilledObserverUI.Update: Count["+ m_Subject.GetKilledCount() +"]");
        m_StageSystem.SetEnemyKilledCount( m_Subject.GetKilledCount() );
    }

}
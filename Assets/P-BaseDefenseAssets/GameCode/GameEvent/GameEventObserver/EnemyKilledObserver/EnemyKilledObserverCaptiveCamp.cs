using UnityEngine;
using System.Collections;

// 兵营观测Enemey阵亡事件
public class EnemyKilledObserverCaptiveCamp : IGameEventObserver 
{
    private EnemyKilledSubject m_Subject = null;
    private CampSystem m_CampSystem = null;
   
    public EnemyKilledObserverCaptiveCamp(CampSystem  theCampSystem)
    {
        m_CampSystem = theCampSystem;
    }

    // 设定观察的主题
    public override    void SetSubject( IGameEventSubject Subject )
    {
        m_Subject = Subject as EnemyKilledSubject;
    }
   
    // 通知Subject被更新
    public override void Update()
    {
        // 累计阵亡10以上时即示俘兵营
        if( m_Subject.GetKilledCount() > 10 ) 
            m_CampSystem.ShowCaptiveCamp();
    }
   
}
using UnityEngine;
using System.Collections;

// 成就观测Soldier阵亡事件
public class SoldierKilledObserverAchievement : IGameEventObserver 
{
    private SoldierKilledSubject m_Subject = null;
    private AchievementSystem m_AchievementSystem = null;
   
    public SoldierKilledObserverAchievement(AchievementSystem  AchievementSystem)
    {
        m_AchievementSystem = AchievementSystem;
    }

    // 设定观察的主题
    public override    void SetSubject( IGameEventSubject Subject )
    {
        m_Subject = Subject as SoldierKilledSubject;
    }
   
    // 通知Subject被更新
    public override void Update()
    {
        m_AchievementSystem.AddSoldierKilledCount();
    }
   
}
using UnityEngine;
using System.Collections;

// 成就观测新关卡
public class NewStageObserverAchievement : IGameEventObserver 
{
    private NewStageSubject m_Subject = null;
    private AchievementSystem m_AchievementSystem = null;
   
    public NewStageObserverAchievement(AchievementSystem  AchievementSystem)
    {
        m_AchievementSystem = AchievementSystem;
    }

    // 设定观察的主题
    public override    void SetSubject( IGameEventSubject Subject )
    {
        m_Subject = Subject as NewStageSubject;
    }
   
    // 通知Subject被更新
    public override void Update()
    {
        m_AchievementSystem.SetNowStageLevel( m_Subject.GetStageCount() );
    }
   
}
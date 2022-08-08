using UnityEngine;
using System.Collections;

// UI观测Enemey阵亡事件
public class EnemyKilledObserverUI : IGameEventObserver 
{
    private EnemyKilledSubject m_Subject = null;
    private PBaseDefenseGame m_PBDGame = null;

    public EnemyKilledObserverUI(PBaseDefenseGame PBDGame  )
    {
        m_PBDGame = PBDGame;
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
        if(m_PBDGame!=null)
            m_PBDGame.ShowGameMsg("敌方单位阵亡");

    }

}
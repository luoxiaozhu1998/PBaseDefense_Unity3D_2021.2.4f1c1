using UnityEngine;
using System.Collections.Generic;

// 游戏事件
public enum ENUM_GameEvent
{
   Null         = 0,
   EnemyKilled    = 1,// 敌方单位阵亡
   SoldierKilled  = 2,// 玩家单位阵亡
   SoldierUpgate  = 3,// 玩家单位升级
   NewStage      = 4,// 新关卡
}


// 游戏事件系统
public class GameEventSystem : IGameSystem
{
   private Dictionary< ENUM_GameEvent, IGameEventSubject> m_GameEvents = new Dictionary< ENUM_GameEvent, IGameEventSubject>(); 

   public GameEventSystem(PBaseDefenseGame PBDGame):base(PBDGame)
   {
      Initialize();
   }
      
   // 释放
   public override void Release()
   {
      m_GameEvents.Clear();
   }
      
   // 替某一主题注册一个观测者
   public void RegisterObserver(ENUM_GameEvent emGameEvnet, IGameEventObserver Observer)
   {
      // 取得事件
      IGameEventSubject Subject = GetGameEventSubject( emGameEvnet );
      if( Subject!=null)
      {
         Subject.Attach( Observer );
         Observer.SetSubject( Subject );
      }
   }

   // 注册一个事件
   private IGameEventSubject GetGameEventSubject( ENUM_GameEvent emGameEvnet )
   {
      // 是否已经存在
      if( m_GameEvents.ContainsKey( emGameEvnet ))
         return m_GameEvents[emGameEvnet];

      // 产生对映的GameEvent
      IGameEventSubject pSujbect= null;
      switch( emGameEvnet )
      {
      case ENUM_GameEvent.EnemyKilled:
         pSujbect = new EnemyKilledSubject();
         break;
      case ENUM_GameEvent.SoldierKilled:
         pSujbect = new SoldierKilledSubject();
         break;
      case ENUM_GameEvent.SoldierUpgate:
         pSujbect = new SoldierUpgateSubject();
         break;
      case ENUM_GameEvent.NewStage:
         pSujbect = new NewStageSubject();
         break;
      default:
         Debug.LogWarning("还没有针对["+emGameEvnet+"]指定要产生的Subject类别");
         return null;
      }

      // 加入后并回传
      m_GameEvents.Add (emGameEvnet, pSujbect );
      return pSujbect;
   }

   // 通知一个GameEvent更新
   public void NotifySubject( ENUM_GameEvent emGameEvnet, System.Object Param)
   {
      // 是否存在
      if( m_GameEvents.ContainsKey( emGameEvnet )==false)
         return ;
      //Debug.Log("SubjectAddCount["+emGameEvnet+"]");
      m_GameEvents[emGameEvnet].SetParam( Param );
   }
   
}


using UnityEngine;
using System.Collections.Generic;

// 兵营系统
public class CampSystem : IGameSystem
{
   private Dictionary<ENUM_Soldier, ICamp> m_SoldierCamps = new Dictionary<ENUM_Soldier,ICamp>(); 
   private Dictionary<ENUM_Enemy , ICamp> m_CaptiveCamps = new Dictionary<ENUM_Enemy,ICamp>(); 

   public CampSystem(PBaseDefenseGame PBDGame):base(PBDGame)
   {
      Initialize();
   }

   // 初始兵营系统
   public override void Initialize()
   {
      // 加入三个兵营
      m_SoldierCamps.Add ( ENUM_Soldier.Rookie, SoldierCampFactory( ENUM_Soldier.Rookie ));
      m_SoldierCamps.Add ( ENUM_Soldier.Sergeant, SoldierCampFactory( ENUM_Soldier.Sergeant ));
      m_SoldierCamps.Add ( ENUM_Soldier.Captain, SoldierCampFactory( ENUM_Soldier.Captain ));

      // 加入一个俘兵营
      m_CaptiveCamps.Add ( ENUM_Enemy.Elf, CaptiveCampFactory( ENUM_Enemy.Elf ));
      // 注册游戏事件观测者
      m_PBDGame.RegisterGameEvent( ENUM_GameEvent.EnemyKilled, new EnemyKilledObserverCaptiveCamp(this));
   }

   // 更新
   public override void Update()
   {
      // 兵营执行命令
      foreach( SoldierCamp Camp in m_SoldierCamps.Values )
         Camp.RunCommand();
      foreach( CaptiveCamp Camp in m_CaptiveCamps.Values )
         Camp.RunCommand();
   }
   
   // 取得场景中的兵营
   private SoldierCamp SoldierCampFactory( ENUM_Soldier emSoldier )
   {
      string GameObjectName = "SoldierCamp_";
      float CoolDown = 0;
      string CampName = "";
      string IconSprite = "";
      switch( emSoldier )
      {
      case ENUM_Soldier.Rookie:
         GameObjectName += "Rookie";
         CoolDown = 3;
         CampName = "菜鸟兵营";
         IconSprite = "RookieCamp";
         break;
      case ENUM_Soldier.Sergeant:
         GameObjectName += "Sergeant";
         CoolDown = 4;
         CampName = "中士兵营";
         IconSprite = "SergeantCamp";
         break;
      case ENUM_Soldier.Captain:
         GameObjectName += "Captain";
         CoolDown = 5;
         CampName = "上尉兵营";
         IconSprite = "CaptainCamp";
         break;
      default:
         Debug.Log("没有指定["+emSoldier+"]要取得的场景对象名称");
         break;          
      }

      // 取得对象
      GameObject theGameObject = UnityTool.FindGameObject( GameObjectName );

      // 取得集合点
      Vector3 TrainPoint = GetTrainPoint( GameObjectName );

      // 产生兵营
      SoldierCamp NewCamp = new SoldierCamp(theGameObject, emSoldier, CampName, IconSprite, CoolDown, TrainPoint); 
      NewCamp.SetPBaseDefenseGame( m_PBDGame );

      // 设定兵营使用的Script
      AddCampScript( theGameObject, NewCamp);

      return NewCamp;
   }

   // 显示场景中的俘兵营
   public void ShowCaptiveCamp()
   {
      if( m_CaptiveCamps[ENUM_Enemy.Elf].GetVisible()==false)
      {
         m_CaptiveCamps[ENUM_Enemy.Elf].SetVisible(true);
         m_PBDGame.ShowGameMsg("获得俘兵营");
      }
   }

   // 取得场景中的俘兵营
   private CaptiveCamp CaptiveCampFactory( ENUM_Enemy emEnemy )
   {
      string GameObjectName = "CaptiveCamp_";
      float CoolDown = 0;
      string CampName = "";
      string IconSprite = "";
      switch( emEnemy )
      {
      case ENUM_Enemy.Elf :
         GameObjectName += "Elf";
         CoolDown = 3;
         CampName = "精灵俘兵营";
         IconSprite = "CaptiveCamp";
         break;    
      default:
         Debug.Log("没有指定["+emEnemy+"]要取得的场景对象名称");
         break;          
      }

      // 取得对象
      GameObject theGameObject = UnityTool.FindGameObject( GameObjectName );
            
      // 取得集合点
      Vector3 TrainPoint = GetTrainPoint( GameObjectName );

      // 产生兵营
      CaptiveCamp NewCamp = new CaptiveCamp(theGameObject, emEnemy, CampName, IconSprite, CoolDown, TrainPoint); 
      NewCamp.SetPBaseDefenseGame( m_PBDGame );

      // 设定兵营使用的Script
      AddCampScript( theGameObject, NewCamp);
      // 先隐藏
      NewCamp.SetVisible(false);

      // 回传
      return NewCamp;
   }

   // 取得集合点
   private Vector3 GetTrainPoint(string GameObjectName )
   {
      // 取得对象
      GameObject theCamp = UnityTool.FindGameObject( GameObjectName );
      // 取得集合点
      GameObject theTrainPoint = UnityTool.FindChildGameObject( theCamp, "TrainPoint" );
      theTrainPoint.SetActive(false);

      return theTrainPoint.transform.position;
   }

   // 设定兵营使用的Script
   private void AddCampScript(GameObject theGameObject,ICamp Camp)
   {
      // 加入Script
      CampOnClick CampScript = theGameObject.AddComponent<CampOnClick>();
      CampScript.theCamp = Camp;
   }
   
   // 通知训练
   public void UTTrainSoldier( ENUM_Soldier emSoldier ) 
   {
      if( m_SoldierCamps.ContainsKey( emSoldier ))
         m_SoldierCamps[emSoldier].Train();
   }  

   // 通知训练
   /*public void TrainCaptive( ENUM_Enemy emEnemy ) 
   {
      if( m_CaptiveCamps.ContainsKey( emEnemy ))
         m_CaptiveCamps[emEnemy].Train();
   }*/    
   
}


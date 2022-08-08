using UnityEngine;
using System.Collections;

public class PBaseDefenseGame
{
   //------------------------------------------------------------------------
   // Singleton模版
   private static PBaseDefenseGame _instance;
   public static PBaseDefenseGame Instance
   {
      get
      {
         if (_instance == null)       
            _instance = new PBaseDefenseGame();
         return _instance;
      }
   }

   // 场景状态控制
   private bool m_bGameOver = false;
   
   // 游戏系统
   private GameEventSystem m_GameEventSystem = null;   // 游戏事件系统
   private CampSystem m_CampSystem     = null;          // 兵营系统
   private StageSystem m_StageSystem = null;         // 关卡系统
   private CharacterSystem m_CharacterSystem = null;   // 角色管理系统
   private APSystem m_ApSystem = null;              // 行动力系统
   private AchievementSystem m_AchievementSystem = null;// 成就系统
   // 界面
   private CampInfoUI m_CampInfoUI = null;              // 兵营界面
   private SoldierInfoUI m_SoldierInfoUI = null;      // 战士信息界面
   private GameStateInfoUI m_GameStateInfoUI = null;   // 游戏状态界面
   private GamePauseUI m_GamePauseUI = null;         // 游戏暂停界面
      
   private PBaseDefenseGame()
   {}

   // 初始P-BaseDefense游戏相关设定
   public void Initinal()
   {
      // 场景状态控制
      m_bGameOver = false;
      // 游戏系统
      m_GameEventSystem = new GameEventSystem(this); // 游戏事件系统
      m_CampSystem = new CampSystem(this);         // 兵营系统
      m_StageSystem = new StageSystem(this);       // 关卡系统
      m_CharacterSystem = new CharacterSystem(this);     // 角色管理系统
      m_ApSystem = new APSystem(this);            // 行动力系统
      m_AchievementSystem = new AchievementSystem(this); // 成就系统
      // 界面
      m_CampInfoUI = new CampInfoUI(this);         // 兵营信息
      m_SoldierInfoUI = new SoldierInfoUI(this);        // Soldier信息                           
      m_GameStateInfoUI = new GameStateInfoUI(this);     // 游戏资料
      m_GamePauseUI = new GamePauseUI (this);          // 游戏暂停

      // 注入到其它系统
      EnemyAI.SetStageSystem( m_StageSystem );

      // 载入存盘
      LoadData();

      // 注册游戏事件系统
      ResigerGameEvent();
   }

   // 注册游戏事件系统
   private void ResigerGameEvent()
   {
      // 事件注册
      m_GameEventSystem.RegisterObserver( ENUM_GameEvent.EnemyKilled, new EnemyKilledObserverUI(this));

      // Combo
      /*ComboObserver theComboObserver =new ComboObserver(this);
      m_GameEventSystem.RegisterObserver( ENUM_GameEvent.EnemyKilled, theComboObserver);
      m_GameEventSystem.RegisterObserver( ENUM_GameEvent.SoldierKilled, theComboObserver);*/

   }

   // 释放游戏系统
   public void Release()
   {
      // 游戏系统
      m_GameEventSystem.Release();
      m_CampSystem.Release();
      m_StageSystem.Release();
      m_CharacterSystem.Release();
      m_ApSystem.Release();
      m_AchievementSystem.Release();
      // 界面
      m_CampInfoUI.Release();
      m_SoldierInfoUI.Release();
      m_GameStateInfoUI.Release();
      m_GamePauseUI.Release();
      UITool.ReleaseCanvas();

      // 存档
      SaveData();
   }

   // 更新
   public void Update()
   {
      // 玩家输入
      InputProcess();

      // 游戏系统更新
      m_GameEventSystem.Update();
      m_CampSystem.Update();
      m_StageSystem.Update();
      m_CharacterSystem.Update();
      m_ApSystem.Update();
      m_AchievementSystem.Update();

      // 玩家界面更新
      m_CampInfoUI.Update();
      m_SoldierInfoUI.Update();
      m_GameStateInfoUI.Update();
      m_GamePauseUI.Update();
   }

   // 玩家输入
   private void InputProcess()
   {
      //  Mouse左键
      if(Input.GetMouseButtonUp( 0 ) ==false)
         return ;
      
      //由摄影机产生一条射线
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      RaycastHit[] hits = Physics.RaycastAll(ray);      
      
      // 走访每一个被Hit到的GameObject
      foreach (RaycastHit hit in hits)
      {
         // 是否有兵营点击
         CampOnClick CampClickScript = hit.transform.gameObject.GetComponent<CampOnClick>();
         if( CampClickScript!=null )
         {
            CampClickScript.OnClick();
            return;
         }
         
         // 是否有角色点击
         SoldierOnClick SoldierClickScript = hit.transform.gameObject.GetComponent<SoldierOnClick>();
         if( SoldierClickScript!=null )
         {
            SoldierClickScript.OnClick();
            return ;
         }
      }
   }
   
   // 游戏状态
   public bool ThisGameIsOver()
   {
      return m_bGameOver;
   }

   // 换回主选单
   public void ChangeToMainMenu()
   {
      m_bGameOver = true;
   }

   // 增加Soldier
   public void AddSoldier( ISoldier theSoldier)
   {
      if( m_CharacterSystem !=null)
         m_CharacterSystem.AddSoldier( theSoldier );
   }

   // 移除Soldier
   public void RemoveSoldier( ISoldier theSoldier)
   {
      if( m_CharacterSystem !=null)
         m_CharacterSystem.RemoveSoldier( theSoldier );
   }
   
   // 增加Enemy
   public void AddEnemy( IEnemy theEnemy)
   {
      if( m_CharacterSystem !=null)
         m_CharacterSystem.AddEnemy( theEnemy );
   }

   // 移除Enemy
   public void RemoveEnemy( IEnemy theEnemy)
   {
      if( m_CharacterSystem !=null)
         m_CharacterSystem.RemoveEnemy( theEnemy );
   }

   // 目前敌人数量
   public int GetEnemyCount()
   {
      if( m_CharacterSystem !=null)
         return m_CharacterSystem.GetEnemyCount();
      return 0;
   }

   // 增加敌人阵亡数量(不透过GameEventSystem呼叫) 
   public void AddEnemyKilledCount()
   {
      m_StageSystem.AddEnemyKilledCount();
   }

   // 执行角色系统的Visitor
   public void RunCharacterVisitor(ICharacterVisitor Visitor)
   {
      m_CharacterSystem.RunVisitor( Visitor );
   }

   // 注册游戏事件
   public void RegisterGameEvent( ENUM_GameEvent emGameEvent, IGameEventObserver Observer)
   {
      m_GameEventSystem.RegisterObserver( emGameEvent , Observer );
   }

   // 通知游戏事件
   public void NotifyGameEvent( ENUM_GameEvent emGameEvent, System.Object Param )
   {
      m_GameEventSystem.NotifySubject( emGameEvent, Param);
   }

   // 显示兵营信息
   public void ShowCampInfo( ICamp Camp )
   {
      m_CampInfoUI.ShowInfo( Camp );
      m_SoldierInfoUI.Hide();
   }

   // 显示Soldier信息
   public void ShowSoldierInfo( ISoldier Soldier )
   {
      m_SoldierInfoUI.ShowInfo( Soldier );
      m_CampInfoUI.Hide();
   }

   // 通知AP更动
   public void APChange( int NowAP)
   {
      m_GameStateInfoUI.ShowAP( NowAP);
   }

   // 花费AP
   public bool CostAP( int ApValue)
   {
      return m_ApSystem.CostAP( ApValue );
   }

   // 显示关卡
   public void ShowNowStageLv( int Lv)
   {
      m_GameStateInfoUI.ShowNowStageLv(Lv);
   }

   //  游戏讯息
   public void ShowGameMsg( string Msg)
   {
      m_GameStateInfoUI.ShowMsg( Msg );
   }

   // 显示Heart
   public void ShowHeart(int Value)
   {
      m_GameStateInfoUI.ShowHeart( Value);
      ShowGameMsg("阵营被攻击");
   }

   // 显示暂停
   public void GamePause()
   {
      if( m_GamePauseUI.IsVisible()==false)
         m_GamePauseUI.ShowGamePause( m_AchievementSystem.CreateSaveData() );
      else
         m_GamePauseUI.Hide();
   }

   // 存档
   private void SaveData()
   {
      AchievementSaveData SaveData = m_AchievementSystem.CreateSaveData();
      SaveData.SaveData();
   }

   // 取回存档
   private AchievementSaveData LoadData()
   {
      AchievementSaveData OldData = new AchievementSaveData();
      OldData.LoadData();
      m_AchievementSystem.SetSaveData( OldData );
      return OldData;
   }
   
   /*#region 直接取得角色数量的实作方式
   // 目前Soldier数量
   public int GetSoldierCount()
   {
      if( m_CharacterSystem !=null)
         return m_CharacterSystem.GetSoldierCount();
      return 0;
   }

   // 目前Soldier数量
   public int GetSoldierCount( ENUM_Soldier emSoldier)
   {
      if( m_CharacterSystem !=null)
         return m_CharacterSystem.GetSoldierCount(emSoldier);
      return 0;
   }  
   #endregion*/

}


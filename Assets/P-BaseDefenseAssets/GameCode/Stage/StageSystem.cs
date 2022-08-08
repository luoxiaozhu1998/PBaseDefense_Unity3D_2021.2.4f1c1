using UnityEngine;
using System.Collections.Generic;

// 关卡控制系统
public class StageSystem : IGameSystem
{
   public const int MAX_HEART = 3;
   private int m_NowHeart = MAX_HEART;          // 目前玩家阵地存情况
   private int    m_EnemyKilledCount = 0;          // 目前敌方单位阵亡数

   private int            m_NowStageLv  = 1;  // 目前的关卡
   private IStageHandler m_NowStageHandler = null;
   private IStageHandler m_RootStageHandler = null;   
   private List<Vector3> m_SpawnPosition = null;     // 出生点
   private Vector3      m_AttackPos = Vector3.zero;  // 攻击点
   private bool        m_bCreateStage = false;     // 是否需要建立关卡

   public StageSystem(PBaseDefenseGame PBDGame):base(PBDGame)
   {
      Initialize();
   }

   // 
   public override void Initialize()
   {
      // 设定关卡
      InitializeStageData();
      // 指定第一个关卡
      m_NowStageHandler = m_RootStageHandler;    
      m_NowStageLv = 1;
      // 注册游戏事件
      m_PBDGame.RegisterGameEvent( ENUM_GameEvent.EnemyKilled, new EnemyKilledObserverStageScore(this)); 
   }

   // 
   public override void Release ()
   {
      base.Release ();
      m_SpawnPosition.Clear();
      m_SpawnPosition = null;
      m_NowHeart = MAX_HEART;
      m_EnemyKilledCount = 0;
      m_AttackPos = Vector3.zero;
   }
   
   // 更新
   public override void Update()
   {
      // 更新目前的关卡
      m_NowStageHandler.Update();

      // 是否要切换下一个关卡
      if(m_PBDGame.GetEnemyCount() ==  0 )
      {
         // 是否结束
         if( m_NowStageHandler.IsFinished()==false)
            return ;

         // 取得下一关
         IStageHandler NewStageData = m_NowStageHandler.CheckStage();

         // 是否为旧的关卡
         if( m_NowStageHandler == NewStageData)
            m_NowStageHandler.Reset();
         else         
            m_NowStageHandler = NewStageData;

         // 通知进入下一关
         NotiyfNewStage();
      }
   }
   
   // 通知损失
   public void LoseHeart()
   {
      m_NowHeart -= m_NowStageHandler.LoseHeart();
      m_PBDGame.ShowHeart( m_NowHeart );
   }

   // 增加目前击杀数(不透过GameEventSystem呼叫)
   public void AddEnemyKilledCount()
   {
      m_EnemyKilledCount++;
   }

   // 设定目前击杀数(透过GameEventSystem呼叫)
   public void SetEnemyKilledCount( int KilledCount)
   {
      //Debug.Log("StageSysem.SetEnemyKilledCount:"+KilledCount);
      m_EnemyKilledCount = KilledCount;
   }

   // 取得目前击杀数
   public int GetEnemyKilledCount()
   {
      return m_EnemyKilledCount;
   }

   // 通知新的关卡
   private void NotiyfNewStage()
   {
      m_PBDGame.ShowGameMsg("新的关卡");
      m_NowStageLv++;

      //  显示
      m_PBDGame.ShowNowStageLv(m_NowStageLv);

      // 事件
      m_PBDGame.NotifyGameEvent( ENUM_GameEvent.NewStage , m_NowStageLv );

   }
   
   // 初始所有关卡
   private void InitializeStageData()
   {
      if( m_RootStageHandler!=null)
         return ;

      // 参考点
      Vector3 AttackPosition = GetAttackPosition();

      NormalStageData StageData = null; // 关卡要产生的Enemy
      IStageScore StageScore = null; // 关卡过关信息
      IStageHandler NewStage = null;

      // 第1关
      StageData  = new NormalStageData(3f, GetSpawnPosition(), AttackPosition );
      StageData.AddStageData( ENUM_Enemy.Elf, ENUM_Weapon.Gun, 3); 
      StageScore     = new StageScoreEnemyKilledCount(3, this);
      NewStage = new NormalStageHandler(StageScore, StageData );

      // 设定为起始关卡
      m_RootStageHandler = NewStage;

      // 第2关
      StageData  = new NormalStageData(3f, GetSpawnPosition(), AttackPosition);
      StageData.AddStageData( ENUM_Enemy.Elf, ENUM_Weapon.Rifle,3); 
      StageScore     = new StageScoreEnemyKilledCount(6, this);
      NewStage = NewStage.SetNextHandler( new NormalStageHandler( StageScore, StageData) );

      // 第3关
      StageData  = new NormalStageData(3f, GetSpawnPosition(), AttackPosition);
      StageData.AddStageData( ENUM_Enemy.Elf, ENUM_Weapon.Rocket,3); 
      StageScore     = new StageScoreEnemyKilledCount(9, this);
      NewStage = NewStage.SetNextHandler( new NormalStageHandler( StageScore, StageData) );

      // 第4关
      StageData  = new NormalStageData(3f, GetSpawnPosition(), AttackPosition);
      StageData.AddStageData( ENUM_Enemy.Troll, ENUM_Weapon.Gun,3); 
      StageScore     = new StageScoreEnemyKilledCount(12, this);
      NewStage = NewStage.SetNextHandler( new NormalStageHandler( StageScore, StageData) );

      // 第5关
      StageData  = new NormalStageData(3f, GetSpawnPosition(), AttackPosition);
      StageData.AddStageData( ENUM_Enemy.Troll, ENUM_Weapon.Rifle,3); 
      StageScore     = new StageScoreEnemyKilledCount(15, this);
      NewStage = NewStage.SetNextHandler( new NormalStageHandler( StageScore, StageData) );

      // 第5关:Boss关卡
      /*StageData    = new NormalStageData(3f, GetSpawnPosition(), AttackPosition);
      StageData.AddStageData( ENUM_Enemy.Ogre, ENUM_Weapon.Rocket,3); 
      StageScore     = new StageScoreEnemyKilledCount(13, this);
      NewStage = NewStage.SetNextHandler( new BossStageHandler( StageScore, StageData) );*/

      // 第6关
      StageData  = new NormalStageData(3f, GetSpawnPosition(), AttackPosition);
      StageData.AddStageData( ENUM_Enemy.Troll, ENUM_Weapon.Rocket,3); 
      StageScore     = new StageScoreEnemyKilledCount(18, this);
      NewStage = NewStage.SetNextHandler( new NormalStageHandler( StageScore, StageData) );

      // 第7关
      StageData  = new NormalStageData(3f, GetSpawnPosition(), AttackPosition);
      StageData.AddStageData( ENUM_Enemy.Ogre, ENUM_Weapon.Gun,3); 
      StageScore     = new StageScoreEnemyKilledCount(21, this);
      NewStage = NewStage.SetNextHandler( new NormalStageHandler( StageScore, StageData) );
      
      // 第8关
      StageData  = new NormalStageData(3f, GetSpawnPosition(), AttackPosition);
      StageData.AddStageData( ENUM_Enemy.Ogre, ENUM_Weapon.Rifle,3); 
      StageScore     = new StageScoreEnemyKilledCount(24, this);
      NewStage = NewStage.SetNextHandler( new NormalStageHandler( StageScore, StageData) );
      
      // 第9关
      StageData  = new NormalStageData(3f, GetSpawnPosition(), AttackPosition);
      StageData.AddStageData( ENUM_Enemy.Ogre, ENUM_Weapon.Rocket,3); 
      StageScore     = new StageScoreEnemyKilledCount(27, this);
      NewStage = NewStage.SetNextHandler( new NormalStageHandler( StageScore, StageData) );

      // 第10关
      StageData  = new NormalStageData(3f, GetSpawnPosition(), AttackPosition);
      StageData.AddStageData( ENUM_Enemy.Elf, ENUM_Weapon.Rocket,3); 
      StageData.AddStageData( ENUM_Enemy.Troll, ENUM_Weapon.Rocket,3); 
      StageData.AddStageData( ENUM_Enemy.Ogre, ENUM_Weapon.Rocket,3); 
      StageScore     = new StageScoreEnemyKilledCount(30, this);
      NewStage = NewStage.SetNextHandler( new NormalStageHandler( StageScore, StageData) );
   }

   // 取得出生点
   private Vector3 GetSpawnPosition()
   {
      if( m_SpawnPosition == null)
      {
         m_SpawnPosition = new List<Vector3>();

         for(int i=1;i<=3;++i)
         {
            string name = string.Format("EnemySpawnPosition{0}",i);
            GameObject tempObj = UnityTool.FindGameObject( name );
            if( tempObj==null)
               continue;
            tempObj.SetActive(false);
            m_SpawnPosition.Add( tempObj.transform.position );
         }
      }

      // 随机传回
      int index  = UnityEngine.Random.Range(0, m_SpawnPosition.Count -1 );
      return m_SpawnPosition[index];
   }

   // 取得攻击点
   private Vector3 GetAttackPosition()
   {
      if( m_AttackPos == Vector3.zero)
      {
         GameObject tempObj = UnityTool.FindGameObject("EnemyAttackPosition");
         if( tempObj==null)
            return Vector3.zero;
         tempObj.SetActive(false);
         m_AttackPos = tempObj.transform.position;
      }
      return m_AttackPos;
   }

   //===============================================================================
   // 定期更新(没有套用 Chain of Responsibility 模式前)
   /*public override void Update()
   {
      // 是否要开启新关卡
      if(m_bCreateStage)
      {
         CreateStage();
         m_bCreateStage =false;
      }
      
      // 是否要切换下一个关卡
      if(m_PBDGame.GetEnemyCount() ==  0 )
      {
         if( CheckNextStage() )
            m_NowStageLv++ ;
         m_bCreateStage = true;
      }
   }
   
   // 建立关卡
   private void CreateStage()
   {
      // 一次产生一个单位
      ICharacterFactory Factory = PBDFactory.GetCharacterFactory();        
      Vector3 AttackPosition = GetAttackPosition();
      switch( m_NowStageLv)
      {
      case 1:
         Debug.Log("建立第1关");
         Factory.CreateEnemy( ENUM_Enemy.Elf ,ENUM_Weapon.Gun, GetSpawnPosition(), AttackPosition);
         Factory.CreateEnemy( ENUM_Enemy.Elf ,ENUM_Weapon.Gun, GetSpawnPosition(), AttackPosition);
         Factory.CreateEnemy( ENUM_Enemy.Elf ,ENUM_Weapon.Gun, GetSpawnPosition(), AttackPosition);
         break;
      case 2:
         Debug.Log("建立第2关");
         Factory.CreateEnemy( ENUM_Enemy.Elf ,ENUM_Weapon.Gun, GetSpawnPosition(), AttackPosition);
         Factory.CreateEnemy( ENUM_Enemy.Elf ,ENUM_Weapon.Rifle, GetSpawnPosition(), AttackPosition);
         Factory.CreateEnemy( ENUM_Enemy.Troll ,ENUM_Weapon.Gun, GetSpawnPosition(), AttackPosition);
         break;
      case 3:
         Debug.Log("建立第3关");
         Factory.CreateEnemy( ENUM_Enemy.Elf ,ENUM_Weapon.Gun, GetSpawnPosition(), AttackPosition);
         Factory.CreateEnemy( ENUM_Enemy.Troll ,ENUM_Weapon.Gun, GetSpawnPosition(), AttackPosition);
         Factory.CreateEnemy( ENUM_Enemy.Troll ,ENUM_Weapon.Rifle, GetSpawnPosition(), AttackPosition);
         break;
      }  
   }
   
   // 确认是否要切掉到下一个关卡
   private bool CheckNextStage()
   {
      switch( m_NowStageLv)
      {
      case 1:
         if( GetEnemyKilledCount() >=3)
            return true;
         break;
      case 2:
         if( GetEnemyKilledCount() >=6)
            return true;
         break;
      case 3:
         if( GetEnemyKilledCount() >=9)
            return true;
         break;
      }  
      return false;
   }*/
   
}


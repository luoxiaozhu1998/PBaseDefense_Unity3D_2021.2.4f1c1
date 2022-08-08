using UnityEngine;
using System.Collections;

public class PBaseDefenseGameUnitTest : MonoBehaviour {

   // Use this for initialization
   void Start () {
      //UnitTest_CharacterCreate();
      //UnitTest_SoldierValue();
      //UnitTest_Camp();
      //UnitTest_CampSystem();
      //UnitTest_GameEvent();
      //UnitTest_StageSystem();
   }

   void Update()
   {
      Update_Camp();
      Update_CampSystem();
      Update_StageSystem();
   }

   // Update is called once per frame
   void UnitTest_CharacterCreate() {
      
      ICharacterFactory Factory = PBDFactory.GetCharacterFactory();
      
      // 建立Soldier
      ISoldier theSoldier = Factory.CreateSoldier( ENUM_Soldier.Rookie, ENUM_Weapon.Gun, 1,new Vector3(0,0,0) );
            
      // 建立Enemy
      IEnemy theEnemy = Factory.CreateEnemy ( ENUM_Enemy.Elf, ENUM_Weapon.Rocket, new Vector3(0,0,0), new Vector3(100,0,0) );

      // 建立俘兵
      ISoldier theCaptive = new SoldierCaptive(theEnemy );
      
      // Soldier打Enemy
        //theSoldier.Attack( theEnemy );
      
      // Enemy打Soldier
      //theEnemy.Attack( theSoldier );
   }

    // Soldier能力
   void UnitTest_SoldierValue() 
   {

      ICharacterFactory Factory = PBDFactory.GetCharacterFactory();

        // 建立Soldier
      ISoldier theSoldier = Factory.CreateSoldier( ENUM_Soldier.Rookie, ENUM_Weapon.Gun, 1,new Vector3(0,0,0) );

      // 加上前辍能力
      IAttrFactory ValueFactory = PBDFactory.GetAttrFactory();
      SoldierAttr PreAttr = ValueFactory.GetEliteSoldierAttr(ENUM_AttrDecorator.Prefix, 11, theSoldier.GetSoldierValue() );
      theSoldier.SetCharacterAttr( PreAttr );

      // 加上后辍能力
      SoldierAttr SufValue = ValueFactory.GetEliteSoldierAttr(ENUM_AttrDecorator.Suffix, 21, theSoldier.GetSoldierValue() );
      theSoldier.SetCharacterAttr( SufValue );
            
      // 建立Enemy
      IEnemy theEnemy = Factory.CreateEnemy ( ENUM_Enemy.Elf, ENUM_Weapon.Rocket, new Vector3(0,0,0), new Vector3(100,0,0) );

      // 建立俘兵
      ISoldier theCaptive =new SoldierCaptive( theEnemy );

      // 加上前辍能力
      PreAttr = ValueFactory.GetEliteSoldierAttr(ENUM_AttrDecorator.Prefix, 11, theCaptive.GetSoldierValue() );
      theCaptive.SetCharacterAttr( PreAttr );

      // 加上后辍能力
      SufValue = ValueFactory.GetEliteSoldierAttr(ENUM_AttrDecorator.Suffix, 21, theCaptive.GetSoldierValue() );
      theCaptive.SetCharacterAttr( SufValue );
            
      // Soldier打Enemy
        //theSoldier.Attack( theEnemy );

        // Enemy打Soldier
      //theEnemy.Attack( theSoldier );

   }

   // 兵营
   SoldierCamp theSoldierCamp= null;
   CaptiveCamp theCaptiveCamp= null;
   void UnitTest_Camp()
   {
      theSoldierCamp = new SoldierCamp(null, ENUM_Soldier.Rookie, "测试", "", 2, Vector3.zero); 
      theCaptiveCamp = new CaptiveCamp(null, ENUM_Enemy.Elf, "测试", "", 3, Vector3.zero); 

      //  执行训练
      theSoldierCamp.Train();
      theSoldierCamp.Train();
      theSoldierCamp.Train();

      theCaptiveCamp.Train();
      theCaptiveCamp.Train();
      theCaptiveCamp.Train();
   }

   // 执行兵营命令
   void Update_Camp()
   {
      if( theSoldierCamp != null)
         theSoldierCamp.RunCommand();
      if( theCaptiveCamp != null)
         theCaptiveCamp.RunCommand();
   }

   // 兵营系统
   CampSystem theCampSystem=null;
   void UnitTest_CampSystem()
   {
      theCampSystem =new CampSystem(null);
      theCampSystem.Initialize();

      //  执行训练
      theCampSystem.UTTrainSoldier( ENUM_Soldier.Rookie ); 
      theCampSystem.UTTrainSoldier( ENUM_Soldier.Rookie ); 
      theCampSystem.UTTrainSoldier( ENUM_Soldier.Rookie ); 

      theCampSystem.UTTrainSoldier( ENUM_Soldier.Captain ); 
      theCampSystem.UTTrainSoldier( ENUM_Soldier.Captain ); 
      theCampSystem.UTTrainSoldier( ENUM_Soldier.Captain ); 
   }

   // 执行兵营系统命令
   void Update_CampSystem()
   {
      if( theCampSystem != null)
         theCampSystem.Update();
   }

   // 游戏事件
   void UnitTest_GameEvent()
   {
      GameEventSystem EventSys = new GameEventSystem( null );

      // 注册
      EventSys.RegisterObserver( ENUM_GameEvent.EnemyKilled, new EnemyKilledObserverUI(null));
      EventSys.RegisterObserver( ENUM_GameEvent.EnemyKilled, new EnemyKilledObserverAchievement(null));

      // 通知
      EventSys.NotifySubject ( ENUM_GameEvent.EnemyKilled, null );
   }

   // 关卡系统
   StageSystem theStageSystem = null;
   void UnitTest_StageSystem()
   {
      theStageSystem = new StageSystem(null);
      theStageSystem.Initialize();
   }

   // 执行关卡
   void Update_StageSystem()
   {
      if( theStageSystem != null)
         theStageSystem.Update();
   }

}


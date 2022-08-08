using UnityEngine;
using System.Collections.Generic;

// 一般关卡信息
public class NormalStageData : IStageData 
{
   private float m_CoolDown = 0;     // 产生角色的间隔时间
   private float m_MaxCoolDown = 0;   // 
   private Vector3 m_SpawnPosition = Vector3.zero;    // 出生点
   private Vector3 m_AttackPosition = Vector3.zero;// 攻击目标
   private bool   m_AllEnemyBorn = false;
   
   //一般关卡要产生的敌人单位
   class StageData
   {
      public ENUM_Enemy emEnemy = ENUM_Enemy.Null;
      public ENUM_Weapon emWeapon = ENUM_Weapon.Null;
      public bool bBorn = false;
      public StageData( ENUM_Enemy emEnemy, ENUM_Weapon emWeapon )
      {
         this.emEnemy = emEnemy;
         this.emWeapon= emWeapon;
      }
   }
   // 关卡内要产生的敌人单位
   private List<StageData> m_StageData = new List<StageData>(); 
   
   // 设定多久产生一个敌方单位
   public NormalStageData(float CoolDown ,Vector3 SpawnPosition, Vector3 AttackPosition)
   {
      m_MaxCoolDown = CoolDown;
      m_CoolDown = m_MaxCoolDown;
      m_SpawnPosition = SpawnPosition;
      m_AttackPosition = AttackPosition;
   }

   // 增加关卡的敌方单位
   public void AddStageData( ENUM_Enemy emEnemy, ENUM_Weapon emWeapon,int Count)
   {
      for(int i=0;i<Count;++i)
         m_StageData.Add ( new StageData(emEnemy, emWeapon));
   }

   // 重置
   public override    void Reset()
   {
      foreach( StageData pData in m_StageData)
         pData.bBorn = false;      
      m_AllEnemyBorn = false;
   }

   // 更新
   public override void Update()
   {
      if( m_StageData.Count == 0)
         return ;

      // 是否可以产生
      m_CoolDown -= Time.deltaTime;
      if( m_CoolDown > 0)
         return ;
      m_CoolDown = m_MaxCoolDown;

      // 取得上场的角色
      StageData theNewEnemy = GetEnemy();
      if(theNewEnemy == null)
         return;

      // 一次产生一个单位
      ICharacterFactory Factory = PBDFactory.GetCharacterFactory();
      Factory.CreateEnemy( theNewEnemy.emEnemy, theNewEnemy.emWeapon, m_SpawnPosition, m_AttackPosition);
   }

   // 取得还没产出
   private StageData GetEnemy()
   {
      foreach( StageData pData in m_StageData)
      {
         if(pData.bBorn == false)
         {
            pData.bBorn = true;
            return pData;
         }
      }
      m_AllEnemyBorn = true;
      return null;
   }


   // 是否完成
   public override    bool IsFinished()
   {
      return m_AllEnemyBorn;
   }
}


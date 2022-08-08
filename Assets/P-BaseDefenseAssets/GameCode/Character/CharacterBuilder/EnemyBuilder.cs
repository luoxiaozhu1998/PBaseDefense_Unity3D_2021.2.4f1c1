using UnityEngine;
using System.Collections;

// 建立Enemy时所需的参数
public class EnemyBuildParam : ICharacterBuildParam
{
   public Vector3    AttackPosition = Vector3.zero; // 要前往的目标
   public EnemyBuildParam()
   {
   }
}

// Enemy各部位的建立
public class EnemyBuilder : ICharacterBuilder
{
   private EnemyBuildParam m_BuildParam = null;

   public override void SetBuildParam( ICharacterBuildParam theParam )
   {
      m_BuildParam = theParam as EnemyBuildParam;    
   }

   // 加载Asset中的角色模型
   public override void LoadAsset( int GameObjectID )
   {
      IAssetFactory AssetFactory = PBDFactory.GetAssetFactory();
      GameObject EnemyGameObject = AssetFactory.LoadEnemy( m_BuildParam.NewCharacter.GetAssetName() );
      EnemyGameObject.transform.position = m_BuildParam.SpawnPosition;
      EnemyGameObject.gameObject.name = string.Format("Enemy[{0}]",GameObjectID);
      m_BuildParam.NewCharacter.SetGameObject( EnemyGameObject );
   }

   // 加入OnClickScript
   public override void AddOnClickScript()
   {
   }

   // 加入武器
   public override void AddWeapon()
   {
      IWeaponFactory  WeaponFactory = PBDFactory.GetWeaponFactory();
      IWeapon Weapon = WeaponFactory.CreateWeapon( m_BuildParam.emWeapon ); 

      // 设定给角色
      m_BuildParam.NewCharacter.SetWeapon( Weapon );
   }
   
   // 设定角色能力
   public override void SetCharacterAttr()
   {
      // 取得Enemy的数值
      IAttrFactory theAttrFactory = PBDFactory.GetAttrFactory();
      int AttrID = m_BuildParam.NewCharacter.GetAttrID();
      EnemyAttr theEnemyAttr = theAttrFactory.GetEnemyAttr( AttrID ); 

      // 设定数值的计算策略
      theEnemyAttr.SetAttStrategy( new EnemyAttrStrategy() );

      // 设定给角色
        m_BuildParam.NewCharacter.SetCharacterAttr( theEnemyAttr );
   }

   // 加入AI
   public override void AddAI()
   {
      EnemyAI theAI = new EnemyAI( m_BuildParam.NewCharacter, m_BuildParam.AttackPosition );
      m_BuildParam.NewCharacter.SetAI( theAI);
   }

   // 加入管理器
   public override void AddCharacterSystem( PBaseDefenseGame PBDGame)
   {
      PBDGame.AddEnemy( m_BuildParam.NewCharacter as IEnemy );
   }

}


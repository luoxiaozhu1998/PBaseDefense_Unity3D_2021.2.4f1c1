using UnityEngine;
using System.Collections;

// 建立Soldier时所需的参数
public class SoldierBuildParam : ICharacterBuildParam
{
   public int           Lv = 0;
   public SoldierBuildParam()
   {
   }
}

// Soldier各部位的建立
public class SoldierBuilder : ICharacterBuilder
{
   private SoldierBuildParam m_BuildParam = null;

   public override void SetBuildParam( ICharacterBuildParam theParam )
   {
      m_BuildParam = theParam as SoldierBuildParam;  
   }
   
   // 加载Asset中的角色模型
   public override void LoadAsset( int GameObjectID )
   {
      IAssetFactory AssetFactory = PBDFactory.GetAssetFactory();
      GameObject SoldierGameObject = AssetFactory.LoadSoldier( m_BuildParam.NewCharacter.GetAssetName() );
      SoldierGameObject.transform.position = m_BuildParam.SpawnPosition;
      SoldierGameObject.gameObject.name = string.Format("Soldier[{0}]",GameObjectID);
      m_BuildParam.NewCharacter.SetGameObject( SoldierGameObject );
   }

   // 加入OnClickScript
   public override void AddOnClickScript()
   {
      SoldierOnClick Script = m_BuildParam.NewCharacter.GetGameObject().AddComponent<SoldierOnClick>();
      Script.Solder = m_BuildParam.NewCharacter as ISoldier;
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
      // 取得Soldier的数值
      IAttrFactory theAttrFactory = PBDFactory.GetAttrFactory();
      int AttrID = m_BuildParam.NewCharacter.GetAttrID();
      SoldierAttr theSoldierAttr = theAttrFactory.GetSoldierAttr( AttrID ); 

      // 设定
      theSoldierAttr.SetAttStrategy( new SoldierAttrStrategy() );

      // 设定等级
      theSoldierAttr.SetSoldierLv( m_BuildParam.Lv );
      
      // 设定给角色
      m_BuildParam.NewCharacter.SetCharacterAttr( theSoldierAttr );
   }

   // 加入AI
   public override void AddAI()
   {
      SoldierAI theAI = new SoldierAI( m_BuildParam.NewCharacter );
      m_BuildParam.NewCharacter.SetAI( theAI );
   }

   // 加入管理器
   public override void AddCharacterSystem( PBaseDefenseGame PBDGame )
   {
      PBDGame.AddSoldier( m_BuildParam.NewCharacter as ISoldier );
   }
}


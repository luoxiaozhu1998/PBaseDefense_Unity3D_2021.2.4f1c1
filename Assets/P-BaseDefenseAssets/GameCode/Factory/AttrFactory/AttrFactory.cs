using UnityEngine;
using System.Collections.Generic;

// 实作产生游戏用数值
public class AttrFactory : IAttrFactory
{
   private Dictionary<int,BaseAttr>      m_SoldierAttrDB = null;
   private Dictionary<int,EnemyBaseAttr>  m_EnemyAttrDB  = null;
   private Dictionary<int,WeaponAttr>        m_WeaponAttrDB     = null;
   private Dictionary<int,AdditionalAttr>  m_AdditionalAttrDB=null;
   
   public AttrFactory()
   {
      InitSoldierAttr();
      InitEnemyAttr();
      InitWeaponAttr();
      InitAdditionalAttr();
   }

   // 建立所有Soldier的数值
   private void InitSoldierAttr()
   {
      m_SoldierAttrDB = new Dictionary<int,BaseAttr>();
      // 基本数值                         // 生命力,移动速度,数值名称
      m_SoldierAttrDB.Add (  1, new CharacterBaseAttr(10, 3.0f, "新兵")); 
      m_SoldierAttrDB.Add (  2, new CharacterBaseAttr(20, 3.2f, "中士")); 
      m_SoldierAttrDB.Add (  3, new CharacterBaseAttr(30, 3.4f, "上尉")); 
   }

   // 建立所有Enemy的数值
   private void InitEnemyAttr()
   {
      m_EnemyAttrDB  = new Dictionary<int,EnemyBaseAttr>();
                           // 生命力,移动速度,数值名称,爆击率,
      m_EnemyAttrDB.Add (  1, new EnemyBaseAttr(5, 3.0f,"精灵",10) );
      m_EnemyAttrDB.Add (  2, new EnemyBaseAttr(15,3.1f,"山妖",20) ); 
      m_EnemyAttrDB.Add (  3, new EnemyBaseAttr(20,3.3f,"怪物",40) );
   }

   // 建立所有Weapon的数值
   private void InitWeaponAttr()
   {
      m_WeaponAttrDB     = new Dictionary<int,WeaponAttr>();
                                 // 攻击力,距离,数值名称
      m_WeaponAttrDB.Add ( 1, new WeaponAttr( 2, 4 ,"短枪") );
      m_WeaponAttrDB.Add ( 2, new WeaponAttr( 4, 7, "长枪") );
      m_WeaponAttrDB.Add ( 3, new WeaponAttr( 8, 10,"火箭筒") );
   }

   // 建立加乘用的数值
   private void InitAdditionalAttr()
   {
      m_AdditionalAttrDB = new Dictionary<int,AdditionalAttr>();

      // 前缀产生时随机产生
      m_AdditionalAttrDB.Add ( 11, new AdditionalAttr( 3, 0, "勇士")); 
      m_AdditionalAttrDB.Add ( 12, new AdditionalAttr( 5, 0, "猛将")); 
      m_AdditionalAttrDB.Add ( 13, new AdditionalAttr(10, 0, "英雄")); 
      
      // 字尾存活下来即增加
      m_AdditionalAttrDB.Add ( 21, new AdditionalAttr( 5, 1, "◇"));  
      m_AdditionalAttrDB.Add ( 22, new AdditionalAttr( 5, 1, "☆"));  
      m_AdditionalAttrDB.Add ( 23, new AdditionalAttr( 5, 1, "★")); 
   }

   
   // 取得Soldier的数值
   public override SoldierAttr GetSoldierAttr( int AttrID )
   {
      if( m_SoldierAttrDB.ContainsKey( AttrID )==false)
      {
         Debug.LogWarning("GetSoldierAttr:AttrID["+AttrID+"]数值不存在");
         return null;
      }

      // 产生数对象并设定共享的数值数据
      SoldierAttr NewAttr = new SoldierAttr();
        NewAttr.SetSoldierAttr(m_SoldierAttrDB[AttrID]);
        return NewAttr;
   }

   // 取得加乘过的Soldier角色数值
   public override SoldierAttr GetEliteSoldierAttr(ENUM_AttrDecorator emType,int AttrID, SoldierAttr theSoldierAttr)
   {
      // 取得加乘效果的数值
      AdditionalAttr theAdditionalAttr =  GetAdditionalAttr( AttrID );
      if( theAdditionalAttr == null)
      {
         Debug.LogWarning("GetEliteSoldierAttr:加乘数值["+AttrID+"]不存在");
         return theSoldierAttr;
      }

      // 产生装饰者
      BaseAttrDecorator theAttrDecorator = null;
      switch( emType)
      {
      case ENUM_AttrDecorator.Prefix:
         theAttrDecorator = new PrefixBaseAttr();
         break;
      case ENUM_AttrDecorator.Suffix:
         theAttrDecorator = new SuffixBaseAttr();
         break;
      }
      if(theAttrDecorator==null)
      {
         Debug.LogWarning("GetEliteSoldierAttr:无法针对["+emType+"]产生装饰者");
         return theSoldierAttr;
      }

      // 设定装饰对像及加乘数值
      theAttrDecorator.SetComponent( theSoldierAttr.GetBaseAttr());
      theAttrDecorator.SetAdditionalAttr( theAdditionalAttr );

      // 设定新的数值后回传
      theSoldierAttr.SetBaseAttr( theAttrDecorator );
      return theSoldierAttr;// 回传
   }

   // 取得Enemy的数值,传入外部参数CritRate
   public override EnemyAttr GetEnemyAttr( int AttrID )
   {
      if( m_EnemyAttrDB.ContainsKey( AttrID )==false)
      {
         Debug.LogWarning("GetEnemyAttr:AttrID["+AttrID+"]数值不存在");
         return null;
      }
      
      // 产生数对象并设定共享的数值数据
      EnemyAttr NewAttr = new EnemyAttr();
      NewAttr.SetEnemyAttr( m_EnemyAttrDB[AttrID]);     
      return NewAttr;
   }
   
   // 取得武器的数值
   public override WeaponAttr GetWeaponAttr( int AttrID )
   {
      if( m_WeaponAttrDB.ContainsKey( AttrID )==false)
      {
         Debug.LogWarning("GetWeaponAttr:AttrID["+AttrID+"]数值不存在");
         return null;
      }
      // 直接回传共享的武器数值
      return m_WeaponAttrDB[AttrID];
   }

   // 取得加乘用的数值
   public override AdditionalAttr GetAdditionalAttr( int AttrID )
   {
      if( m_AdditionalAttrDB.ContainsKey( AttrID )==false)
      {
         Debug.LogWarning("GetAdditionalAttr:AttrID["+AttrID+"]数值不存在");
         return null;
      }

      // 直接回传加乘用的数值
      return m_AdditionalAttrDB[AttrID];
   }
   
}


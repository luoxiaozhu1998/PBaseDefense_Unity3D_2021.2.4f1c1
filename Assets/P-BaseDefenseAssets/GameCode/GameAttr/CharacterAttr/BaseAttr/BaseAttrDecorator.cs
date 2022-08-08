using UnityEngine;
using System.Collections.Generic;

// 用于加乘用的数值
public class AdditionalAttr
{
   private int    m_Strength;    // 力量
   private int    m_Agility; // 敏捷
   private string     m_Name;       // 数值的名称   
   
   public AdditionalAttr(int Strength,int Agility, string Name)
   {
      m_Strength = Strength;
      m_Agility = Agility;
      m_Name = Name;
   }
   
   public int GetStrength()
   {
      return m_Strength;
   }
   
   public int GetAgility()
   {
      return m_Agility;
   }
   
   public string GetName()
   {
      return m_Name;
   }
}



// 基本角色数值装饰者
public abstract class BaseAttrDecorator : BaseAttr
{
   protected BaseAttr           m_Component;      // 被装饰对像
   protected AdditionalAttr   m_AdditionialAttr;     // 代表额外加乘的数值
   
   // 设定装饰的目标
   public void SetComponent(BaseAttr theComponent)
   {
      m_Component = theComponent;
   }

   // 设定额外使用的值
   public void SetAdditionalAttr (AdditionalAttr theAdditionalAttr)
   {
      m_AdditionialAttr = theAdditionalAttr;
   }

   public override int GetMaxHP()
   {
      return m_Component.GetMaxHP();
   }
   
   public override float GetMoveSpeed()
   {
      return m_Component.GetMoveSpeed();
   }
   
   public override string GetAttrName()
   {
      return m_Component.GetAttrName();
   }
}


// 装饰类型
public enum ENUM_AttrDecorator
{
   Prefix,
   Suffix,
}

// 前缀
public class PrefixBaseAttr : BaseAttrDecorator
{
   public PrefixBaseAttr()
   {}
   
   public override int GetMaxHP()
   {
      return m_AdditionialAttr.GetStrength() + m_Component.GetMaxHP();
   }
   
   public override float GetMoveSpeed()
   {
      return m_AdditionialAttr.GetAgility()*0.2f + m_Component.GetMoveSpeed();
   }
   
   public override string GetAttrName()
   {
      return m_AdditionialAttr.GetName() + m_Component.GetAttrName();
   }  
}

// 字尾
public class SuffixBaseAttr : BaseAttrDecorator
{
   public SuffixBaseAttr()
   {}
   
   public override int GetMaxHP()
   {
      return m_Component.GetMaxHP() + m_AdditionialAttr.GetStrength();
   }
   
   public override float GetMoveSpeed()
   {
      return m_Component.GetMoveSpeed() + m_AdditionialAttr.GetAgility()*0.2f;
   }
   
   public override string GetAttrName()
   {
      return m_Component.GetAttrName() + m_AdditionialAttr.GetName();
   }  
}

// 直接强化
public class StrengthenBaseAttr : BaseAttrDecorator
{
   protected List<AdditionalAttr>     m_AdditionialAttrs;    // 多个强化的数值

   public StrengthenBaseAttr()
   {}
   
   public override int GetMaxHP()
   {
      int MaxHP = m_Component.GetMaxHP();
      foreach(AdditionalAttr theAttr in m_AdditionialAttrs)
         MaxHP += theAttr.GetStrength();
      return MaxHP;
   }
   
   public override float GetMoveSpeed()
   {
      float MoveSpeed = m_Component.GetMoveSpeed();
      foreach(AdditionalAttr theAttr in m_AdditionialAttrs)
         MoveSpeed += theAttr.GetAgility()*0.2f;
      return MoveSpeed;
   }
   
   public override string GetAttrName()
   { 
      return "直接强化" + m_Component.GetAttrName();
   }  
}


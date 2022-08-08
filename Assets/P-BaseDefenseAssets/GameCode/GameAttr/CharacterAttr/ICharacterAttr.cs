using UnityEngine;
using System.Collections;

// 角色数值界面
public abstract class ICharacterAttr
{
    protected BaseAttr m_BaseAttr= null;   // 基本角色数值
    //protected int    m_MaxHP = 0;          // 最高HP值
    //protected float  m_MoveSpeed = 1.0f; // 移动速度
    //protected string m_AttrName = "";       // 数值的名称
   
    protected int   m_NowHP = 0;     // 目前HP值
    protected IAttrStrategy m_AttrStrategy=null;// 数值的计算策略

    public ICharacterAttr(){}

    // 设定基本属性
    public void SetBaseAttr( BaseAttr BaseAttr )
    {
        m_BaseAttr = BaseAttr;
    }

    // 取得基本属性
    public BaseAttr GetBaseAttr()
    {
        return m_BaseAttr;
    }
   
    // 设定数值的计算策略
    public void SetAttStrategy(IAttrStrategy theAttrStrategy)
    {
        m_AttrStrategy = theAttrStrategy;
    }

    // 取得数值的计算策略
    public IAttrStrategy GetAttStrategy()
    {
        return m_AttrStrategy;
    }

    // 目前HP
    public int GetNowHP()
    {
        return m_NowHP;
    }

    // 最大HP
    public virtual int GetMaxHP()
    {
        return m_BaseAttr.GetMaxHP();
    }

    // 回满目前HP值
    public void FullNowHP()
    {
        m_NowHP = GetMaxHP();
    }
   
    // 移动速度
    public virtual float GetMoveSpeed()
    {
        return m_BaseAttr.GetMoveSpeed();
    }
      
    // 取得数值名称
    public virtual string GetAttrName()
    {
        return m_BaseAttr.GetAttrName();
    }

    // 初始角色数值
    public virtual void InitAttr()
    {
        m_AttrStrategy.InitAttr( this ); 
        FullNowHP();
    }

    // 攻击加乘
    public int GetAtkPlusValue()
    {
        return m_AttrStrategy.GetAtkPlusValue( this );
    }

    // 取得被武器攻击后的伤害值
    public void CalDmgValue( ICharacter Attacker )
    {
        // 取得武器功击力
        int AtkValue = Attacker.GetAtkValue();
      
        // 减伤
        AtkValue -= m_AttrStrategy.GetDmgDescValue(this);
      
        // 扣去伤害
        m_NowHP -= AtkValue;
    }

}
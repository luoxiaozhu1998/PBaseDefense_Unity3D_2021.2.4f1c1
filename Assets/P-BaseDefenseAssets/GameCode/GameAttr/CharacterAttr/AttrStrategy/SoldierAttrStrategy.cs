using UnityEngine;
using System.Collections;

// 玩家单位(士兵)的数值计算策略
public class SoldierAttrStrategy : IAttrStrategy 
{
    // 初始的数值
    public override void InitAttr( ICharacterAttr CharacterAttr )
    {
        // 是否为士兵类别
        SoldierAttr theSoldierAttr = CharacterAttr as SoldierAttr;
        if(theSoldierAttr==null)
            return ;

        // 最大生命力有等级加乘
        int AddMaxHP = 0;
        int Lv = theSoldierAttr.GetSoldierLv();
        if(Lv > 0 )
            AddMaxHP = (Lv-1)*2;
   
        // 设定最高HP
        theSoldierAttr.AddMaxHP( AddMaxHP );
    }
   
    // 攻击加乘
    public override int GetAtkPlusValue( ICharacterAttr CharacterAttr )
    {
        // 没有攻击加乘
        return 0;
    }
   
    // 取得减伤害值
    public override int GetDmgDescValue( ICharacterAttr CharacterAttr )
    {
        // 是否为士兵类别
        SoldierAttr theSoldierAttr = CharacterAttr as SoldierAttr;
        if(theSoldierAttr==null)
            return 0;

        // 回传减伤值
        return (theSoldierAttr.GetSoldierLv()-1)*2;;
    }

}
using UnityEngine;
using System.Collections;

// 敌方单位的数值计算策略
public class EnemyAttrStrategy : IAttrStrategy 
{
    // 初始的数值
    public override void InitAttr( ICharacterAttr CharacterAttr )
    {
        // 不用计算
    }
   
    // 攻击加乘
    public override int GetAtkPlusValue( ICharacterAttr CharacterAttr )
    {
        // 是否为敌方数值
        EnemyAttr theEnemyAttr = CharacterAttr as EnemyAttr;
        if(theEnemyAttr==null)
            return 0;

        // 依爆击机率回传攻击加乘值
        int RandValue =  UnityEngine.Random.Range(0,100);
        if( theEnemyAttr.GetCritRate()  >= RandValue )
        {
            theEnemyAttr.CutdownCritRate();       // 减少爆击机率
            return theEnemyAttr.GetMaxHP()*5;  // 血量的5倍值
        }
        return 0;
    }
   
    // 取得减伤害值
    public override int GetDmgDescValue( ICharacterAttr CharacterAttr )
    {
        // 没有减伤
        return 0;
    }

}
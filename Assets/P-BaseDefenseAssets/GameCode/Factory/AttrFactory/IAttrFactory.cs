using UnityEngine;
using System.Collections;

// 产生游戏用数值界面
public abstract class IAttrFactory
{
    // 取得Soldier的数值
    public abstract SoldierAttr GetSoldierAttr( int AttrID );

    // 取得Soldier的数值:有前缀字尾的加乘
    public abstract SoldierAttr GetEliteSoldierAttr(ENUM_AttrDecorator emType,int AttrID, SoldierAttr theSoldierAttr);
       
    // 取得Enemy的数值
    public abstract EnemyAttr GetEnemyAttr(int AttrID);

    // 取得武器的数值
    public abstract WeaponAttr GetWeaponAttr(int AttrID);

    // 取得加乘用的数值
    public abstract AdditionalAttr GetAdditionalAttr( int AttrID );
   
}
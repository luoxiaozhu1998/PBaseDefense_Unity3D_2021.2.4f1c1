using UnityEngine;
using System.Collections;

// 产生武器工厂界面
public abstract class IWeaponFactory
{
    // 建立武器
    public abstract IWeapon CreateWeapon( ENUM_Weapon emWeapon);
}
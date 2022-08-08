using UnityEngine;
using System.Collections;

public class WeaponRifle : IWeapon 
{
    public WeaponRifle()
    {
        m_emWeaponType = ENUM_Weapon.Rifle;
    }
   
    // 显示武器子弹特效
    protected override void DoShowBulletEffect( ICharacter theTarget )
    {
        ShowBulletEffect(theTarget.GetPosition(),0.5f,0.2f);
    }
   
    // 显示音效
    protected override void DoShowSoundEffect()
    {
        ShowSoundEffect("RifleShot");
    }

}
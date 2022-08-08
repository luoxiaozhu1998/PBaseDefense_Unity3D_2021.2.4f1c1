using UnityEngine;
using System.Collections;

public class WeaponGun : IWeapon 
{
    public WeaponGun()
    {
        m_emWeaponType = ENUM_Weapon.Gun;
    }

    // 显示武器子弹特效
    protected override void DoShowBulletEffect( ICharacter theTarget )
    {
        ShowBulletEffect(theTarget.GetPosition(),0.03f,0.2f);
    }
   
    // 显示音效
    protected override void DoShowSoundEffect()
    {
        ShowSoundEffect("GunShot");
    }


}
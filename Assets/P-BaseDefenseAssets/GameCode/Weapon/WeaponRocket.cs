using UnityEngine;
using System.Collections;

public class WeaponRocket : IWeapon 
{
    public WeaponRocket()
    {
        m_emWeaponType = ENUM_Weapon.Rocket;
    }

    // 显示武器子弹特效
    protected override void DoShowBulletEffect( ICharacter theTarget)
    {
        ShowBulletEffect(theTarget.GetPosition(),0.8f,0.5f);
    }
   
    // 显示音效
    protected override void DoShowSoundEffect()
    {
        ShowSoundEffect("RocketShot");
    }

}
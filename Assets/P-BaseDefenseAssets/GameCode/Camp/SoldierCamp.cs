using UnityEngine;
using System.Collections;

// Soldier兵营
public class SoldierCamp : ICamp
{
    const int MAX_LV = 3;  
    ENUM_Weapon     m_emWeapon = ENUM_Weapon.Gun; // 武器等级
    int    m_Lv = 1;                       // 兵营等级
    Vector3 m_Position;                      // 训练完成后的集合点

    // 设定兵营产出的单位及冷
    public SoldierCamp(GameObject theGameObject, ENUM_Soldier emSoldier,string CampName, string IconSprite ,float TrainCoolDown,Vector3 Position):base(theGameObject, TrainCoolDown,CampName,IconSprite )
    {
        m_emSoldier = emSoldier;         
        m_Position = Position;
    }

    // 等级
    public override int GetLevel()
    {
        return m_Lv;
    }

    // 升级花费
    public override int GetLevelUpCost()
    { 
        if( m_Lv >= MAX_LV)
            return 0;
        return 100;
    }

    // 升级
    public override void LevelUp()
    {
        m_Lv++;
        m_Lv = Mathf.Min( m_Lv , MAX_LV);
    }
   
    // 武器等级
    public override ENUM_Weapon GetWeaponType()
    {
        return m_emWeapon;
    }

    // 武器升级花费
    public override int GetWeaponLevelUpCost()
    { 
        if( (m_emWeapon + 1) >= ENUM_Weapon.Max )
            return 0;
        return 100;
    }
   
    // 武器升级
    public override void WeaponLevelUp()
    {
        m_emWeapon++;
        if( m_emWeapon >=ENUM_Weapon.Max)
            m_emWeapon--;
    }

    // 取得训练金额
    public override int GetTrainCost()
    {
        return m_TrainCost.GetTrainCost( m_emSoldier, m_Lv, m_emWeapon) ;
    }

    // 训练Soldier
    public override void Train()
    {
        // 产生一个训练命令
        TrainSoldierCommand NewCommand = new TrainSoldierCommand( m_emSoldier, m_emWeapon, m_Lv, m_Position);  
        AddTrainCommand( NewCommand );
    }
}
using UnityEngine;
using System.Collections;

// 训练Soldier
public class TrainSoldierCommand : ITrainCommand
{  
    ENUM_Soldier   m_emSoldier;   // 兵种
    ENUM_Weapon       m_emWeapon;       // 使用的武器
    int             m_Lv;            // 等级
    Vector3       m_Position;    // 出现位置
      
    // 训练
    public TrainSoldierCommand( ENUM_Soldier emSoldier, ENUM_Weapon emWeapon, int Lv, Vector3 Position)
    {
        m_emSoldier = emSoldier;
        m_emWeapon = emWeapon;
        m_Lv = Lv;
        m_Position = Position;
    }

    //  执行
    public override void Execute()
    {
        // 建立Soldier
        ICharacterFactory Factory = PBDFactory.GetCharacterFactory();
        ISoldier Soldier = Factory.CreateSoldier( m_emSoldier, m_emWeapon, m_Lv , m_Position );
      
        // 依机率产生前辍能力
        int Rate = UnityEngine.Random.Range(0,100);
        int AttrID = 0;
        if( Rate > 90)
            AttrID = 13;
        else if( Rate > 80)
            AttrID = 12;
        else if( Rate > 60)
            AttrID = 11;
        else
            return ;
      
        // 加上前缀能力
        //Debug.Log("加上前辍能力:"+AttrID);
        IAttrFactory AttrFactory = PBDFactory.GetAttrFactory();
        SoldierAttr PreAttr = AttrFactory.GetEliteSoldierAttr(ENUM_AttrDecorator.Prefix, AttrID, Soldier.GetSoldierValue());
        Soldier.SetCharacterAttr(PreAttr);
    }
}
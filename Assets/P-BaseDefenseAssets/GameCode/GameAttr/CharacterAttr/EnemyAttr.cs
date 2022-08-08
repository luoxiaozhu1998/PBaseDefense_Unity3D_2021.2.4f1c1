using UnityEngine;
using System.Collections;

// Enemy数值
public class EnemyAttr : ICharacterAttr
{
    protected int m_CritRate = 0; // 爆击机率

    public EnemyAttr()
    {}

    // 设定角色数值(包含外部参数)
    public void SetEnemyAttr(EnemyBaseAttr EnemyBaseAttr)
    {
        // 共享组件
        base.SetBaseAttr( EnemyBaseAttr );

        // 外部参数
        m_CritRate = EnemyBaseAttr.GetInitCritRate();
    }
   
    // 爆击率
    public int GetCritRate()
    {
        return m_CritRate;
    }

    // 减少爆击率
    public void CutdownCritRate()
    {
        m_CritRate -= m_CritRate/2;
    }

}
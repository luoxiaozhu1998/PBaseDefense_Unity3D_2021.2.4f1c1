using UnityEngine;
using System.Collections.Generic;

// 闲置状态
public class IdleAIState : IAIState 
{
    bool   m_bSetAttackPosition = false; // 是否有设定攻击目标

    public IdleAIState()
    {}       

    // 设定要攻击的目标
    public override void SetAttackPosition( Vector3 AttackPosition )      
    {
        m_bSetAttackPosition = true;
    }

    // 更新
    public override void Update( List<ICharacter> Targets )
    {
        // 没有目标时
        if(Targets == null ||  Targets.Count==0)
        {
            // 有设定目标时,往目标移动
            if( m_bSetAttackPosition )
                m_CharacterAI.ChangeAIState( new MoveAIState());
            return ;
        }

        // 找出最近的目标
        Vector3 NowPosition = m_CharacterAI.GetPosition();
        ICharacter theNearTarget = null;
        float MinDist = 999f;
        foreach(ICharacter Target in  Targets)
        {
            // 已经阵亡的不计算
            if( Target.IsKilled())
                continue;

            float dist = Vector3.Distance( NowPosition, Target.GetGameObject().transform.position);
            if( dist < MinDist)
            {
                MinDist = dist;
                theNearTarget = Target;
            }
        }

        // 没有目标,会不动
        if( theNearTarget==null)
            return;

        // 是否在距离内
        if( m_CharacterAI.TargetInAttackRange( theNearTarget ))
            m_CharacterAI.ChangeAIState( new AttackAIState( theNearTarget ));
        else
            m_CharacterAI.ChangeAIState( new ChaseAIState( theNearTarget ));
    }
}
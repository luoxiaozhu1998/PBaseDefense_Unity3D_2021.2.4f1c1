using UnityEngine;
using System.Collections.Generic;

// 移动的目标状态
public class MoveAIState : IAIState 
{
    private const float MOVE_CHECK_DIST = 1.5f; //
    bool   m_bOnMove = false;
    Vector3    m_AttackPosition = Vector3.zero;
   
    public MoveAIState()
    {} 

    // 设定要攻击的目标
    public override void SetAttackPosition( Vector3 AttackPosition )
    {
        m_AttackPosition = AttackPosition;
    }

    // 更新
    public override void Update( List<ICharacter> Targets )
    {
        // 有目标时,改为待机状态
        if(Targets != null &&  Targets.Count>0)
        {
            m_CharacterAI.ChangeAIState( new IdleAIState() );
            return ;
        }

        // 已经目标移动
        if( m_bOnMove)
        {
            //  是否到达目标
            float dist = Vector3.Distance( m_AttackPosition, m_CharacterAI.GetPosition());
            if( dist < MOVE_CHECK_DIST )
            {
                m_CharacterAI.ChangeAIState( new IdleAIState()); 
                if( m_CharacterAI.IsKilled()==false)
                    m_CharacterAI.CanAttackHeart();//Debug.Log ("攻到目标");
                m_CharacterAI.Killed();
            }
            return ;
        }

        // 往目标移动
        //Debug.Log ("MoveAIState.往目标移动");
        m_bOnMove = true;
        m_CharacterAI.MoveTo( m_AttackPosition );
    }

}
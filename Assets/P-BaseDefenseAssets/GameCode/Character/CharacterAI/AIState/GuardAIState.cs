using UnityEngine;
using System.Collections.Generic;

// 守卫状态
public class GuardAIState : IAIState 
{
    bool   m_bOnMove = false;
    Vector3    m_Position = Vector3.zero;
    const int GUARD_DISTANCE = 3;
   
    public GuardAIState()
    {} 

    // 更新
    public override void Update( List<ICharacter> Targets )
    {
        // 有目标时,改为待机状态
        if(Targets != null &&  Targets.Count>0)
        {
            m_CharacterAI.ChangeAIState( new IdleAIState() );
            return ;
        }

        if( m_Position == Vector3.zero)
            GetMovePosition();

        // 已经目标移动
        if( m_bOnMove)
        {
            //  是否到达目标
            float dist = Vector3.Distance( m_Position, m_CharacterAI.GetPosition());
            if( dist > 0.5f )        
                return ;

            // 换下一个位置
            GetMovePosition();
        }

        // 往目标移动
        m_bOnMove = true;
        m_CharacterAI.MoveTo( m_Position );
    }

    // 设定移动的位置
    private void GetMovePosition()
    {
        m_bOnMove = false;

        // 取得随机位置
        Vector3 RandPos = new Vector3( UnityEngine.Random.Range(-GUARD_DISTANCE,GUARD_DISTANCE), 0, UnityEngine.Random.Range(-GUARD_DISTANCE,GUARD_DISTANCE));

        // 设定为新的位置 
        m_Position = m_CharacterAI.GetPosition() + RandPos;
    }

}
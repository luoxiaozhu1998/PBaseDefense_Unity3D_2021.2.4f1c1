using UnityEngine;
using System.Collections.Generic;

// 玩家角色AI
public class SoldierAI : ICharacterAI 
{  
    public SoldierAI(ICharacter Character):base(Character)
    {
        // 一开始起始的状态
        ChangeAIState(new IdleAIState());
    }

    // 是否可以攻击Heart
    public override bool CanAttackHeart()
    {
        return false;
    }
}
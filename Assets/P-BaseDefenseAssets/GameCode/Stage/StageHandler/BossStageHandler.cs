using UnityEngine;
using System.Collections;

// Boss关卡
public class BossStageHandler : NormalStageHandler 
{
    public BossStageHandler(IStageScore StateScore, IStageData StageData ):base(StateScore,StageData) 
    {}

    // 损失的生命值
    public override int  LoseHeart()
    {
        return StageSystem.MAX_HEART;
    }
}
using UnityEngine;
using System.Collections;

// 一般关卡
public class NormalStageHandler : IStageHandler 
{
    // 设定分数及关卡数据
    public NormalStageHandler(IStageScore StateScore, IStageData StageData )
    {
        m_StageScore  = StateScore;
        m_StatgeData  = StageData;
    }
      
    // 确认关卡
    public override IStageHandler CheckStage()
    {
        // 分数是否足够
        if( m_StageScore.CheckScore()==false)
            return this;

        // 已经是最后一关了
        if(m_NextHandler==null)
            return this;      

        // 确认下一个关卡
        return m_NextHandler.CheckStage();
    }
   
    public override void Update()
    {
        m_StatgeData.Update();
    }

    public override void Reset()
    {
        m_StatgeData.Reset();
    }

    public override bool IsFinished()
    {
        return m_StatgeData.IsFinished();
    }

    // 损失的生命值
    public override int  LoseHeart()
    {
        return 1;
    }
}
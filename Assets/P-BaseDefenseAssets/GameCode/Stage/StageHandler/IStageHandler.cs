using UnityEngine;
using System.Collections;

// 关卡界面
public abstract class IStageHandler
{
    protected IStageHandler m_NextHandler = null;// 下一个关卡
    protected IStageData   m_StatgeData  = null;
    protected IStageScore   m_StageScore  = null;// 关卡的分数

    // 设定下一个关卡
    public IStageHandler SetNextHandler(IStageHandler NextHandler)
    {
        m_NextHandler = NextHandler;
        return m_NextHandler;
    }

    public abstract IStageHandler CheckStage();
    public abstract void Update();
    public abstract void Reset();
    public abstract bool IsFinished();
    public abstract int  LoseHeart();
}
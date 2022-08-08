using UnityEngine;
using System.Collections;

// 关卡信息接口
public abstract class IStageData
{
    public abstract void Update();
    public abstract    bool IsFinished();
    public abstract    void Reset();
}
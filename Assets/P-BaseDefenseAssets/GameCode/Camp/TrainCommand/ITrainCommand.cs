using UnityEngine;
using System.Collections;

// 执行训练命令的接口
public abstract class ITrainCommand
{
    public abstract void Execute();
}
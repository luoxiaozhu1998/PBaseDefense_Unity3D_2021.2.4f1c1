using UnityEngine;
using System.Collections;

// 场景状态
public class ISceneState
{
    // 状态名称
    private string m_StateName = "ISceneState";
    public string StateName
    {
        get{ return m_StateName; }
        set{ m_StateName = value; }
    }

    // 控制者
    protected SceneStateController m_Controller = null;
      
    // 建构者
    public ISceneState(SceneStateController Controller)
    { 
        m_Controller = Controller; 
    }

    // 开始
    public virtual void StateBegin()
    {}

    // 结束
    public virtual void StateEnd()
    {}

    // 更新
    public virtual void StateUpdate()
    {}

    public override string ToString ()
    {
        return string.Format ("[I_SceneState: StateName={0}]", StateName);
    }


}
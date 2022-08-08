using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// 主选单状态
public class MainMenuState : ISceneState
{
    public MainMenuState(SceneStateController Controller):base(Controller)
    {
        this.StateName = "MainMenuState";
    }

    // 开始
    public override void StateBegin()
    {
        // 取得开始按钮
        Button tmpBtn = UITool.GetUIComponent<Button>("StartGameBtn");
        if(tmpBtn!=null)
            tmpBtn.onClick.AddListener( ()=> OnStartGameBtnClick(tmpBtn) );
    }
         
    // 开始战斗
    private void OnStartGameBtnClick(Button theButton)
    {
        //Debug.Log ("OnStartBtnClick:"+theButton.gameObject.name);
        m_Controller.SetState(new BattleState(m_Controller), "BattleScene" );
    }

}
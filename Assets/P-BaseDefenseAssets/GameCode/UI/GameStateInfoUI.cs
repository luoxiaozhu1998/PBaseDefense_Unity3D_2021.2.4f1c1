using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

// 游戏状态信息
public class GameStateInfoUI : IUserInterface
{
   private Text m_MsgText = null;
   private Text m_APText = null;
   private Text m_NowStageLvText = null;
   private Text m_SoldierCountText = null;
   private Text m_EnemyCountText = null;
   
   private GameObject m_GameOverObj = null;
   private Slider m_ApSlider = null;
   private List<Image> m_HeartImages = null; 
   
   private const float MESSAGE_TIMER = 2f;
   private float m_MsgTimer = 0;

   private UnitCountVisitor m_UnitCountVisitor = new UnitCountVisitor(); // 双方角色计数

   //  
   public GameStateInfoUI( PBaseDefenseGame PBDGame ):base(PBDGame)
   {
      Initialize();
   }

   //
   public override void Initialize()
   {
      m_RootUI = UITool.FindUIGameObject( "GameStateInfo" );

      //游戏讯息
      m_MsgText = UITool.GetUIComponent<Text>(m_RootUI,"NotifyText");
      m_MsgText.text = "";
      // 关卡
      m_NowStageLvText = UITool.GetUIComponent<Text>(m_RootUI,"NowStageLvText");
      ShowNowStageLv(1);
      // AP讯息
      m_APText = UITool.GetUIComponent<Text>(m_RootUI,"APText");
      m_ApSlider = UITool.GetUIComponent<Slider>(m_RootUI,"APSlider");
      m_ApSlider.maxValue = APSystem.MAX_AP;
      m_ApSlider.minValue = 0;
      ShowAP( APSystem.MAX_AP );
      // 双方数量
      m_SoldierCountText = UITool.GetUIComponent<Text>(m_RootUI,"SoldierCountText");;
      m_EnemyCountText = UITool.GetUIComponent<Text>(m_RootUI,"EnemyCountText");;

      // Heart
      m_HeartImages = new List<Image>();
      for(int i=1;i<=StageSystem.MAX_HEART;++i)
      {
         string name = string.Format("Heart{0}",i);
         m_HeartImages.Add ( UITool.GetUIComponent<Image>(m_RootUI,name));
      }
      ShowHeart( StageSystem.MAX_HEART );

      // 结束Continue
      Button btn  = UITool.GetUIComponent<Button>(m_RootUI, "ContinueBtn");
      btn.onClick.AddListener( ()=> OnContinueBtnClick() );
      // 结束
      m_GameOverObj = UnityTool.FindChildGameObject( m_RootUI,"GameOver");
      m_GameOverObj.SetActive(false);

      // 暂停
      btn  = UITool.GetUIComponent<Button>(m_RootUI, "PauseBtn");
      btn.onClick.AddListener( ()=> OnPauseBtnClick() );
   }

   public override void Release ()
   {
      base.Release ();
      m_HeartImages = null;
      Time.timeScale = 1;
   }

   //
   public override void Update ()
   {
      base.Update ();

      // 执行角色计算Visitor
      m_UnitCountVisitor.Reset();
      m_PBDGame.RunCharacterVisitor(m_UnitCountVisitor);

      // 双方数量
      m_SoldierCountText.text = string.Format("我方单位数:{0}", m_UnitCountVisitor.GetUnitCount( ENUM_Soldier.Null ));
      m_EnemyCountText.text = string.Format("敌方单位数:{0}", m_UnitCountVisitor.GetUnitCount( ENUM_Enemy.Null ));

      if( m_MsgTimer <=0)
         return ;

      // 消除已显示的讯息
      m_MsgTimer -= Time.deltaTime;
      if(m_MsgTimer > 0)
         return ;
      m_MsgTimer = 0;
      m_MsgText.text = "";
   }

   // 显示AP
   public void ShowAP( int Value)
   {
      m_ApSlider.value = Value;
      m_APText.text = Value.ToString();
   }

   // 显示Heart数
   public void ShowHeart(int Value)
   {
      int i=0;
      for(;i<Value;++i)
         m_HeartImages[i].enabled = true;
      for(;i<StageSystem.MAX_HEART;++i)
         m_HeartImages[i].enabled = false;

      if( Value <=0 )
         ShowGameOver();
   }

   // 显示目前关卡
   public void ShowNowStageLv( int Lv)
   {
      m_NowStageLvText.text = string.Format("目前关卡:{0}",Lv);
   }

   // 显示结束
   private void ShowGameOver()
   {
      m_GameOverObj.SetActive(true);
      Time.timeScale = 0;
   }

   // Continue
   private void OnContinueBtnClick()
   {
      Time.timeScale = 1;
      // 换回开始State
      m_PBDGame.ChangeToMainMenu();
   }

   // Pause
   private void OnPauseBtnClick()
   {
      // 显示暂停
      m_PBDGame.GamePause();
   }

   // 
   public void ShowMsg(string Msg)
   {
      if( m_MsgTimer > 0)
         m_MsgText.text = m_MsgText.text + ","+ Msg;
      else 
         m_MsgText.text = Msg;
      m_MsgTimer = MESSAGE_TIMER;
   }
}


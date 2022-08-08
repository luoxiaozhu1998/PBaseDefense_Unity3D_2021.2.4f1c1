using UnityEngine;
using System.Collections;
using UnityEngine.UI;


// 兵营界面
public class CampInfoUI : IUserInterface
{
   private ICamp m_Camp = null; // 显示的兵营

   // 界面组件
   private Button m_LevelUpBtn = null;
   private Button m_WeaponLvUpBtn = null;
   private Button m_TrainBtn = null;
   private Button m_CancelBtn = null;
   private Text m_AliveCountTxt = null;
   private Text m_CampLvTxt = null;
   private Text m_WeaponLvTxt = null;
   private Text m_TrainCostText = null;
   private Text m_TrainTimerText= null;
   private Text m_OnTrainCountTxt = null;
   private Text m_CampNameTxt = null;
   private Image m_CampImage = null; 

   private UnitCountVisitor m_UnitCountVisitor = new UnitCountVisitor(); // 存活单位计数

   
   public CampInfoUI( PBaseDefenseGame PBDGame ):base(PBDGame)
   {
      Initialize();
   }

   // 初始
   public override void Initialize()
   {
      m_RootUI = UITool.FindUIGameObject( "CampInfoUI" );

      // 显示的讯息
      // 兵营名称
      m_CampNameTxt = UITool.GetUIComponent<Text>(m_RootUI, "CampNameText");
      // 兵营图
      m_CampImage = UITool.GetUIComponent<Image>(m_RootUI, "CampIcon");
      // 存活单位数
      m_AliveCountTxt = UITool.GetUIComponent<Text>(m_RootUI, "AliveCountText");          
      // 等级
      m_CampLvTxt = UITool.GetUIComponent<Text>(m_RootUI, "CampLevelText");
      // 武器等级
      m_WeaponLvTxt = UITool.GetUIComponent<Text>(m_RootUI, "WeaponLevelText");
      // 训练中的数量
      m_OnTrainCountTxt = UITool.GetUIComponent<Text>(m_RootUI, "OnTrainCountText");
      // 训练花费
      m_TrainCostText = UITool.GetUIComponent<Text>(m_RootUI, "TrainCostText");
      // 训练时间
      m_TrainTimerText = UITool.GetUIComponent<Text>(m_RootUI, "TrainTimerText");

      // 玩家的互动
      // 升级
      m_LevelUpBtn = UITool.GetUIComponent<Button>(m_RootUI, "CampLevelUpBtn");
      m_LevelUpBtn.onClick.AddListener( ()=> OnLevelUpBtnClick() );
      // 武器升级
      m_WeaponLvUpBtn = UITool.GetUIComponent<Button>(m_RootUI, "WeaponLevelUpBtn");
      m_WeaponLvUpBtn.onClick.AddListener( ()=> OnWeaponLevelUpBtnClick() );
      // 训练
      m_TrainBtn = UITool.GetUIComponent<Button>(m_RootUI, "TrainSoldierBtn");
      m_TrainBtn.onClick.AddListener( ()=> OnTrainBtnClick() );
      // 取消训练
      m_CancelBtn = UITool.GetUIComponent<Button>(m_RootUI, "CancelTrainBtn");
      m_CancelBtn.onClick.AddListener( ()=> OnCancelBtnClick() );

      Hide();
   }

   // 显示信息
   public void ShowInfo(ICamp Camp)
   {
      //Debug.Log("显示兵营信息");
      Show ();
      m_Camp = Camp;

      // 名称
      m_CampNameTxt.text = m_Camp.GetName();
      // 训练花费
      m_TrainCostText.text = string.Format("AP:{0}",m_Camp.GetTrainCost());
      // 训练中信息
      ShowOnTrainInfo();
      // Icon
      IAssetFactory Factory = PBDFactory.GetAssetFactory();
      m_CampImage.sprite = Factory.LoadSprite( m_Camp.GetIconSpriteName());

      // 升级功能
      if( m_Camp.GetLevel() <= 0 )
         EnableLevelInfo(false);
      else
      {
         EnableLevelInfo(true);
         m_CampLvTxt.text = string.Format("等级:" + m_Camp.GetLevel());
         ShowWeaponLv();// 显示武器等级
      }        
   }

   // 显示武器等级
   private void ShowWeaponLv()
   {
      string WeaponName = "";
      switch(m_Camp.GetWeaponType())
      {
      case ENUM_Weapon.Gun:
         WeaponName = "枪";
         break;
      case ENUM_Weapon.Rifle:
         WeaponName = "长枪";
         break;
      case ENUM_Weapon.Rocket:
         WeaponName = "火箭筒";
         break;
      default:
         WeaponName = "未命名";
         break;
      }
      m_WeaponLvTxt.text = string.Format("武器等级:{0}",WeaponName);

   }

   // 训练中的信息
   private void ShowOnTrainInfo()
   {
      if( m_Camp == null)
         return ;
      int Count = m_Camp.GetOnTrainCount();
      m_OnTrainCountTxt.text = string.Format("训练数量:" + Count);
      if(Count>0)
         m_TrainTimerText.text = string.Format("完成时间:{0:0.00}",m_Camp.GetTrainTimer());
      else
         m_TrainTimerText.text = "";

      // 存活单位
      m_UnitCountVisitor.Reset();
      m_PBDGame.RunCharacterVisitor( m_UnitCountVisitor );
      int UnitCount = m_UnitCountVisitor.GetUnitCount( m_Camp.GetSoldierType());
      m_AliveCountTxt.text = string.Format( "存活单位:{0}",UnitCount );
   }

   // 更新
   public override void Update ()
   {
      ShowOnTrainInfo();
   }

   // 显示详细信息
   private void EnableLevelInfo(bool Value)
   {
      m_CampLvTxt.enabled = Value;
      m_WeaponLvTxt.enabled = Value;
      m_LevelUpBtn.gameObject.SetActive(Value);
      m_WeaponLvUpBtn.gameObject.SetActive( Value);
   }
   
   // 升级
   private void OnLevelUpBtnClick()
   {
      int Cost = m_Camp.GetLevelUpCost();
      if( CheckRule( Cost > 0 , "已达最高等级")==false )
         return ;

      // 是否足够
      string Msg = string.Format("AP不足无法升级,需要{0}点AP",Cost);
      if( CheckRule(  m_PBDGame.CostAP(Cost), Msg ) ==false)
         return ;

      // 升级
      m_Camp.LevelUp();
      ShowInfo( m_Camp );
   }

   // 武器升级
   private void OnWeaponLevelUpBtnClick()
   {
      int Cost = m_Camp.GetWeaponLevelUpCost();
      if( CheckRule( Cost > 0 ,"已达最高等级" )==false )      
         return ;

      // 是否足够
      string Msg = string.Format("AP不足无法升级,需要{0}点AP",Cost);
      if( CheckRule( m_PBDGame.CostAP(Cost), Msg ) ==false)
         return ;
      
      // 升级
      m_Camp.WeaponLevelUp();
      ShowInfo( m_Camp );
   }

   // 训练
   private void OnTrainBtnClick()
   {
      int Cost = m_Camp.GetTrainCost();
      if( CheckRule( Cost > 0 ,"无法训练" )==false )    
         return ;

      // 是否足够
      string Msg = string.Format("AP不足无法训练,需要{0}点AP",Cost);
      if( CheckRule( m_PBDGame.CostAP(Cost), Msg ) ==false)
         return ;

      // 产生训练命令
      m_Camp.Train();
      ShowInfo( m_Camp );
   }

   // 取消训练
   private void OnCancelBtnClick()
   {
      // 取消训练命令
      m_Camp.RemoveLastTrainCommand();
      ShowInfo( m_Camp );
   }

   // 判断条件并显示讯息
   private bool CheckRule( bool bValue, string NotifyMsg )
   {
      if( bValue == false)
         m_PBDGame.ShowGameMsg( NotifyMsg );          
      return bValue;
   }

}


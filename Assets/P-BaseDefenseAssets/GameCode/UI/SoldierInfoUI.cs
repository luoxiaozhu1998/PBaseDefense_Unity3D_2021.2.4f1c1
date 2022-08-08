using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Soldier界面
public class SoldierInfoUI : IUserInterface
{
   private ISoldier m_Soldier = null; // 显示的Soldier

   // 界面组件
   private Image  m_Icon = null;
   private Text   m_NameTxt = null;
   private Text   m_HPTxt = null;
   private Text   m_LvTxt = null;
   private Text   m_AtkTxt = null;
   private Text   m_AtkRangeTxt = null;
   private Text   m_SpeedTxt = null;
   private Slider m_HPSlider = null;

   public SoldierInfoUI( PBaseDefenseGame PBDGame ):base(PBDGame)
   {
      Initialize();
   }
      
   // 初始
   public override void Initialize()
   {
      m_RootUI = UITool.FindUIGameObject( "SoldierInfoUI" );

      // 图像
      m_Icon = UITool.GetUIComponent<Image>(m_RootUI, "SoldierIcon");
      // 名称
      m_NameTxt = UITool.GetUIComponent<Text>(m_RootUI, "SoldierNameText");
      // HP
      m_HPTxt = UITool.GetUIComponent<Text>(m_RootUI, "SoldierHPText");
      // 等级
      m_LvTxt = UITool.GetUIComponent<Text>(m_RootUI, "SoldierLvText");
      // Atk
      m_AtkTxt = UITool.GetUIComponent<Text>(m_RootUI, "SoldierAtkText");
      // Atk 距离
      m_AtkRangeTxt = UITool.GetUIComponent<Text>(m_RootUI, "SoldierAtkRangeText");
      // Speed
      m_SpeedTxt = UITool.GetUIComponent<Text>(m_RootUI, "SoldierSpeedText");
      // HP图示 
      m_HPSlider = UITool.GetUIComponent<Slider>(m_RootUI, "SoldierSlider"); 

      // 注册游戏事者
      m_PBDGame.RegisterGameEvent( ENUM_GameEvent.SoldierKilled, new SoldierKilledObserverUI( this ));
      m_PBDGame.RegisterGameEvent( ENUM_GameEvent.SoldierUpgate, new SoldierUpgateObserverUI( this ));

      Hide();
   }

   // Hide
   public override void Hide ()
   {
      base.Hide ();
      m_Soldier = null;
   }

   // 显示信息
   public void ShowInfo(ISoldier Soldier)
   {
      //Debug.Log("显示Soldier信息");
      m_Soldier = Soldier;
      if( m_Soldier == null || m_Soldier.IsKilled())
      {
         Hide ();
         return ;
      }
      Show ();

      // 显示Soldier信息
      // Icon
      IAssetFactory Factory = PBDFactory.GetAssetFactory();
      m_Icon.sprite = Factory.LoadSprite( m_Soldier.GetIconSpriteName());
      // 名称
      m_NameTxt.text =  m_Soldier.GetName();
      // 等级 
      m_LvTxt.text =string.Format("等级:{0}", m_Soldier.GetSoldierValue().GetSoldierLv());
      // Atk
      m_AtkTxt.text = string.Format( "攻击力:{0}",m_Soldier.GetWeapon().GetAtkValue());
      // Atk距离
      m_AtkRangeTxt.text = string.Format( "攻击距离:{0}",m_Soldier.GetWeapon().GetAtkRange());
      // Speed
      m_SpeedTxt.text = string.Format("移动速度:{0}", m_Soldier.GetSoldierValue().GetMoveSpeed());;

      // 更新HP信息
      RefreshHPInfo();
   }

   // 更新
   public void RefreshSoldier( ISoldier Soldier  )
   {
      if( Soldier==null)
      {
         m_Soldier=null;
         Hide ();
      }
      if( m_Soldier != Soldier)
         return ;
      ShowInfo( Soldier );
   }

   // 更新HP信息
   private void RefreshHPInfo()
   {
      int NowHP = m_Soldier.GetSoldierValue().GetNowHP();
      int MaxHP = m_Soldier.GetSoldierValue().GetMaxHP();

      m_HPTxt.text = string.Format("HP({0}/{1})", NowHP, MaxHP);
      // HP图示 
      m_HPSlider.maxValue = MaxHP;
      m_HPSlider.minValue = 0;
      m_HPSlider.value = NowHP;
   }

   // 更新
   public override void Update ()
   {
      base.Update ();       
      if(m_Soldier==null)
         return ;
      // 是否死亡
      if(m_Soldier.IsKilled())
      {
         m_Soldier = null;
         Hide ();
         return ;
      }
      
      // 更新HP信息
      RefreshHPInfo();
   }
}


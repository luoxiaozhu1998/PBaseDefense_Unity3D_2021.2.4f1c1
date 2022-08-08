using UnityEngine;
using System.Collections.Generic;

// 角色界面
public abstract class ICharacter
{
   protected string m_Name = "";           // 名称
   
   protected GameObject m_GameObject = null;  // 显示的Uniyt模型
   protected UnityEngine.AI.NavMeshAgent m_NavAgent = null;   // 控制角色移动使用
   protected AudioSource  m_Audio   = null;

   protected string   m_IconSpriteName = ""; // 显示Ico
   protected string   m_AssetName = "";     // 模型名称
   protected int     m_AttrID   = 0;          // 使用的角色属性编号
      
   protected bool m_bKilled = false;        // 是否阵亡
   protected bool m_bCheckKilled = false;    // 是否确认过阵亡事件
   protected float m_RemoveTimer = 1.5f;     // 阵亡后多久移除
   protected bool m_bCanRemove = false;      // 是否可以移除

   private IWeapon m_Weapon = null;         // 使用的武器
   protected ICharacterAttr m_Attribute = null;// 数值
   protected ICharacterAI m_AI = null;          // AI


         
   // 建构者
   public ICharacter(){}

   // 设定Unity模型
   public void SetGameObject( GameObject theGameObject )
   {
      m_GameObject = theGameObject ;
      m_NavAgent = m_GameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
      m_Audio    = m_GameObject.GetComponent<AudioSource>();
   }

   // 取得Unity模型
   public GameObject GetGameObject()
   {
      return m_GameObject;
   }

   // 释放
   public void Release()
   {
      if( m_GameObject != null)
         GameObject.Destroy( m_GameObject);
   }

   // 名称
   public string GetName()
   {
      return m_Name;
   }

   // 取得Asset名称
   public string GetAssetName()
   {
      return m_AssetName;
   }

   // 取得Icon名称
   public string  GetIconSpriteName()
   {
      return m_IconSpriteName ;
   }

   // 取得属性ID
   public int GetAttrID()
   {
      return m_AttrID;
   }

   // 取得目前的值
   public ICharacterAttr GetCharacterAttr()
   {
      return m_Attribute;
   }
      
   // 取得角色名称
   public string GetCharacterName()
   {
      return m_Name; 
   }

   // 更新
   public void Update()
   {
      if( m_bKilled)
      {
         m_RemoveTimer -= Time.deltaTime;
         if( m_RemoveTimer <=0 )
            m_bCanRemove = true;
      }
      
      m_Weapon.Update();
   }

   #region 移动及位置  
   // 移动到目标
   public void MoveTo( Vector3 Position )
   {
      m_NavAgent.SetDestination( Position );
   }

   // 停止移动
   public void StopMove()
   {
      m_NavAgent.Stop();
   }

   //  取得位置
   public Vector3 GetPosition()
   {
      return m_GameObject.transform.position;
   }
   #endregion
   
   #region AI
   // 设定AI
   public void SetAI(ICharacterAI CharacterAI)
   {
      m_AI = CharacterAI;
   }

   // 更新AI
   public void UpdateAI(List<ICharacter> Targets)
   {
      m_AI.Update(Targets);
   }

   // 通知AI有角色被移除
   public void RemoveAITarget( ICharacter Targets)
   {
      m_AI.RemoveAITarget(Targets);
   }
   #endregion

   #region 攻击
   // 攻击目标
   public void Attack( ICharacter Target)
   {
      // 设定武器额外攻击加乘
      SetWeaponAtkPlusValue(m_Attribute.GetAtkPlusValue());

      // 攻击
      WeaponAttackTarget( Target);

      // 攻击动作
      m_GameObject.GetComponent<CharacterMovement>().PlayAttackAnim();

      // 面向目标
      m_GameObject.transform.forward = Target.GetPosition() - GetPosition();
   }

   // 被其他角色攻击
   public abstract void UnderAttack( ICharacter Attacker);
   #endregion

   #region 武器
   // 设定使用的武器
   public void SetWeapon(IWeapon Weapon)
   {
      if( m_Weapon != null)
         m_Weapon.Release();
      m_Weapon = Weapon;
      
      // 设定武器拥有者
      m_Weapon.SetOwner(this);
      
      // 设定Unity GameObject的层级
      UnityTool.AttachToRefPos( m_GameObject, m_Weapon.GetGameObject(),"weapon-point" ,Vector3.zero);
   }
   
   // 取得武器
   public IWeapon GetWeapon()
   {
      return m_Weapon;
   }

   // 设定额外攻击力
   protected void SetWeaponAtkPlusValue(int Value)
   {
      m_Weapon.SetAtkPlusValue( Value );
   }

   // 武器攻击目标
   protected void WeaponAttackTarget( ICharacter Target)
   {
      m_Weapon.Fire( Target );
   }
   
   // 计算攻击力
   public int GetAtkValue()
   {
      // 武器攻击力 + 角色数值的加乘
      return m_Weapon.GetAtkValue();
   }

   // 取得攻击距离
   public float GetAttackRange()
   {
      return m_Weapon.GetAtkRange();
   }     
   #endregion

   #region 阵亡及移除
   // 阵亡
   public void Killed()
   {
      if( m_bKilled == true)
         return;
      m_bKilled = true;
      m_bCheckKilled = false;
   }

   // 是否阵亡
   public bool IsKilled()
   {
      return m_bKilled; 
   }

   // 是否确认阵亡过
   public bool CheckKilledEvent()
   {
      if( m_bCheckKilled)
         return true;
      m_bCheckKilled = true;
      return false;
   }

   //  是否可以移除
   public bool CanRemove()
   {
      return m_bCanRemove;
   }
   #endregion

   #region 角色数值
   // 设定角色数值
   public virtual void SetCharacterAttr( ICharacterAttr CharacterAttr)
   {
      // 设定
      m_Attribute = CharacterAttr;
      m_Attribute.InitAttr ();

      // 设定移动速度
      m_NavAgent.speed = m_Attribute.GetMoveSpeed();
      //Debug.Log ("设定移动速度:"+m_NavAgent.speed);

      // 名称
      m_Name = m_Attribute.GetAttrName();
   }
   #endregion

   #region 音效特效

   // 播放音效
   public void PlaySound(string ClipName)
   {
      //  取得音效
      IAssetFactory Factory = PBDFactory.GetAssetFactory();
      AudioClip theClip = Factory.LoadAudioClip( ClipName);
      if(theClip == null)
         return ;
      m_Audio.clip = theClip;
      m_Audio.Play();
   }

   // 播放特效
   public void PlayEffect(string EffectName)
   {
      //  取得特效
      IAssetFactory Factory = PBDFactory.GetAssetFactory();
      GameObject EffectObj = Factory.LoadEffect( EffectName );
      if(EffectObj == null)
         return ;

      // 加入
      UnityTool.Attach( m_GameObject, EffectObj, Vector3.zero); 
   }
   #endregion

   // 执行Visitor
   public virtual void RunVisitor(ICharacterVisitor Visitor)
   {
      Visitor.VisitCharacter(this);
   }
         

} 





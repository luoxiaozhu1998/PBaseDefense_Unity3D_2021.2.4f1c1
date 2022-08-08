using UnityEngine;
using System.Collections;

// 从项目的Resource中,将Unity Asset实体化成GameObject的工厂类别
public class ResourceAssetFactory : IAssetFactory 
{
   public const string SoldierPath = "Characters/Soldier/";
   public const string EnemyPath = "Characters/Enemy/";
   public const string WeaponPath = "Weapons/";
   public const string EffectPath = "Effects/";
   public const string AudioPath  = "Audios/";
   public const string SpritePath = "Sprites/";

   // 产生Soldier
   public override GameObject LoadSoldier( string AssetName )
   {  
      return InstantiateGameObject( SoldierPath + AssetName );
   }
   
   // 产生Enemy
   public override GameObject LoadEnemy( string AssetName )
   {
      return InstantiateGameObject( EnemyPath + AssetName  );
   }

   // 产生Weapon
   public override GameObject LoadWeapon( string AssetName )
   {
      return InstantiateGameObject( WeaponPath +  AssetName );
   }

   // 产生特效
   public override GameObject LoadEffect( string AssetName )
   {
      return InstantiateGameObject( EffectPath + AssetName);
   }

   // 产生AudioClip
   public override AudioClip  LoadAudioClip(string ClipName)
   {
      UnityEngine.Object res = LoadGameObjectFromResourcePath(AudioPath + ClipName );
      if(res==null)
         return null;
      return res as AudioClip;
   }

   // 产生Sprite
   public override Sprite LoadSprite(string SpriteName)
   {
      return Resources.Load(SpritePath + SpriteName,typeof(Sprite)) as Sprite;
   }

   // 产生GameObject
   private GameObject InstantiateGameObject( string AssetName )
   {
      // 从Resrouce中载入
      UnityEngine.Object res = LoadGameObjectFromResourcePath( AssetName );
      if(res==null)
         return null;
      return  UnityEngine.Object.Instantiate(res) as GameObject;
   }

   // 从Resrouce中载入
   public UnityEngine.Object LoadGameObjectFromResourcePath( string AssetPath)
   {
      UnityEngine.Object res = Resources.Load(AssetPath);
      if( res == null)
      {
         Debug.LogWarning("无法加载路径["+AssetPath+"]上的Asset");
         return null;
      }     
      return res;
   }
}


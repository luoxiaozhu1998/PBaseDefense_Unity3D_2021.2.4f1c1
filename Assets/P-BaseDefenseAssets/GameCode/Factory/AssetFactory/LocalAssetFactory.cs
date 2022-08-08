using UnityEngine;
using System.Collections;

// 从本地(储存设备)中,将Unity Asset实体化成GameObject的工厂类别
public class LocalAssetFactory : IAssetFactory 
{  
    // 产生Soldier
    public override GameObject LoadSoldier( string AssetName)
    {
        // 加载放在本地装置的Asset示意
        //string FilePath = "D:/xxx/Characters/Soldier/"+AssetName+".assetbundle";
        // 执行加载
        return null;
    }
   
    // 产生Enemy
    public override GameObject LoadEnemy( string AssetName )
    {
        // 加载放在本地装置上的Asset示意
        //string RemotePath = "D:/xxx/Characters/Enemy/"+AssetName+".assetbundle";
        // 执行加载
        return null;
    }

    // 产生Weapon
    public override GameObject LoadWeapon( string AssetName )
    {
        // 加载放在本地装置上的Asset示意
        //string RemotePath = "D:/xxx/Weapons/"+AssetName+".assetbundle";
        // 执行加载
        return null;
    }

    // 产生特效
    public override GameObject LoadEffect( string AssetName )
    {
        // 加载放在本地装置上的Asset示意
        //string RemotePath = "D:/xxx/Effects/"+AssetName+".assetbundle";
        // 执行加载
        return null;
    }

    // 产生AudioClip
    public override AudioClip  LoadAudioClip(string ClipName)
    {
        // 加载放在本地装置上的Asset示意
        //string RemotePath = "D:/xxx/Audios/"+AssetName+".assetbundle";
        // 执行加载
        return null;
    }

    // 产生Sprite
    public override Sprite LoadSprite(string SpriteName)
    {
        // 加载放在本地装置上的Asset示意
        //string RemotePath = "D:/xxx/Sprites/"+SpriteName+".assetbundle";
        // 执行加载
        return null;
    }
}
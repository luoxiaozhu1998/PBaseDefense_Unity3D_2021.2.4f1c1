using UnityEngine;
using System.Collections;

// 从远程(网络WebServer)中,将Unity Asset实体化成GameObject的工厂类别
public class RemoteAssetFactory : IAssetFactory {

    // 产生Soldier
    public override GameObject LoadSoldier( string AssetName)
    {
        // 加载放在网络上的Asset示意
        //string RemotePath = "http://127.0.0.1/PBDResource/Characters/Soldier/"+AssetName+".assetbundle";
        //WWW.LoadFromCacheOrDownload( RemotePath,0); 
        return null;
    }
   
    // 产生Enemy
    public override GameObject LoadEnemy( string AssetName )
    {
        // 加载放在网络上的Asset示意
        //string RemotePath = "http://127.0.0.1/PBDResource/Characters/Enemy/"+AssetName+".assetbundle";
        //WWW.LoadFromCacheOrDownload( RemotePath,0);
        return null;
    }

    // 产生Weapon
    public override GameObject LoadWeapon( string AssetName )
    {
        // 加载放在网络上的Asset示意
        //string RemotePath = "http://127.0.0.1/PBDResource/Weapons/"+AssetName+".assetbundle";
        //WWW.LoadFromCacheOrDownload( RemotePath,0);
        return null;
    }

    // 产生特效
    public override GameObject LoadEffect( string AssetName )
    {
        // 加载放在网络上的Asset示意
        //string RemotePath = "http://127.0.0.1/PBDResource/Effects/"+AssetName+".assetbundle";
        //WWW.LoadFromCacheOrDownload( RemotePath,0);
        return null;
    }

    // 产生AudioClip
    public override AudioClip  LoadAudioClip(string ClipName)
    {
        // 加载放在网络上的Asset示意
        //string RemotePath = "http://127.0.0.1/PBDResource/Audios/"+ClipName+".assetbundle";
        //WWW.LoadFromCacheOrDownload( RemotePath,0);
        return null;
    }

    // 产生Sprite
    public override Sprite LoadSprite(string SpriteName)
    {
        // 加载放在网络上的Asset示意
        //string RemotePath = "http://127.0.0.1/PBDResource/Sprites/"+SpriteName+".assetbundle";
        //WWW.LoadFromCacheOrDownload( RemotePath,0);
        return null;
    }
}
using UnityEngine;
using System.Collections;

// 将Unity Asset实体化成GameObject的工厂类别
public abstract class IAssetFactory
{
    // 产生Soldier
    public abstract GameObject LoadSoldier( string AssetName );

    // 产生Enemy
    public abstract GameObject LoadEnemy( string AssetName );

    // 产生Weapon
    public abstract GameObject LoadWeapon( string AssetName );

    // 产生特效
    public abstract GameObject LoadEffect( string AssetName );

    // 产生AudioClip
    public abstract AudioClip  LoadAudioClip(string ClipName);

    // 产生Sprite
    public abstract Sprite    LoadSprite(string SpriteName);
   
}

/*
 * 使用Abstract Factory Patterny简化版,
 * 让GameObject的产生可以依Uniyt Asset放置的位置来加载Asset
 * 先实作放在Resource目录下的Asset及Remote(Web Server)上的
 * 当Unity随着版本的演进，也许会提供不同的加载方式，那么我们就可以
 * 再先将一个IAssetFactory的子类别来因应变化
 */
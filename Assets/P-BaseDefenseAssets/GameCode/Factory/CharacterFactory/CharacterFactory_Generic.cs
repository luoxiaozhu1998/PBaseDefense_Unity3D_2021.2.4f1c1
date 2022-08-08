using UnityEngine;
using System.Collections;

// 产生游戏角色工厂(Generic版
public class CharacterFactory_Generic : TCharacterFactory_Generic
{
    // 角色建立指导者
    private CharacterBuilderSystem m_BuilderDirector = new CharacterBuilderSystem( PBaseDefenseGame.Instance );
   
    // 建立Soldier(Generice版)
    public ISoldier CreateSoldier<T>(ENUM_Weapon emWeapon, int Lv, Vector3 SpawnPosition) where T: ISoldier,new()
    {
        // 产生Soldier的参数
        SoldierBuildParam SoldierParam = new SoldierBuildParam();
      
        // 产生对应的T类别
        SoldierParam.NewCharacter = new T();
        if( SoldierParam.NewCharacter == null)
            return null;
      
        // 设定共享参数
        SoldierParam.emWeapon = emWeapon;
        SoldierParam.SpawnPosition = SpawnPosition;
        SoldierParam.Lv         = Lv;
      
        //  产生对应的Builder及设定参数
        SoldierBuilder theSoldierBuilder = new SoldierBuilder();
        theSoldierBuilder.SetBuildParam( SoldierParam ); 
      
        // 产生
        m_BuilderDirector.Construct( theSoldierBuilder );
        return SoldierParam.NewCharacter  as ISoldier;
    }
   
    // 建立Enemy(Generice版)
    public IEnemy CreateEnemy<T>(ENUM_Weapon emWeapon, Vector3 SpawnPosition, Vector3 AttackPosition) where T: IEnemy,new()
    {
        // 产生Enemy的参数
        EnemyBuildParam EnemyParam = new EnemyBuildParam();
      
        // 产生对应的Character
        EnemyParam.NewCharacter = new T();
        if( EnemyParam.NewCharacter == null)
            return null;
      
        // 设定共享参数
        EnemyParam.emWeapon = emWeapon;
        EnemyParam.SpawnPosition = SpawnPosition;
        EnemyParam.AttackPosition = AttackPosition;
      
        //  产生对应的Builder及设定参数
        EnemyBuilder theEnemyBuilder = new EnemyBuilder();
        theEnemyBuilder.SetBuildParam( EnemyParam ); 
      
        // 产生
        m_BuilderDirector.Construct( theEnemyBuilder );
        return EnemyParam.NewCharacter  as IEnemy;
    }
   
}
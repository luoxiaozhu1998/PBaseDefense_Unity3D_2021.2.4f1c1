using UnityEngine;
using System.Collections;

// 武器数值类别
public class WeaponAttr
{
    public int        Atk    {get; private set;}    // 攻击力
    public float   AtkRange{get; private set;}    // 攻击距离
    public string  AttrName{get; private set;} // 属性名称

    public     WeaponAttr(int AtkValue,float Range,string AttrName)
    {
        this.Atk = AtkValue;
        this.AtkRange = Range;
        this.AttrName = AttrName;
    }
}
using UnityEngine;
using System.Collections;

// 兵营点击成功后通知显示
public class CampOnClick : MonoBehaviour 
{
    public ICamp theCamp = null;

    // Use this for initialization
    void Start () {}
   
    // Update is called once per frame
    void Update () {}

    public void OnClick()
    {
        // 显示兵营信息
        PBaseDefenseGame.Instance.ShowCampInfo( theCamp );
    }
}
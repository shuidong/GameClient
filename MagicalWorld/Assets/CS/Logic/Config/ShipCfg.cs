using UnityEngine;
using System.Collections;
using CG3D.Config;
/// <summary>
/// 舰船数据
/// </summary>
public class ShipCfg : BaseCfg
{
    private static ShipCfg _instance;
    public static ShipCfg GetInstance()
    {
        if(_instance == null)
        {
            _instance = new ShipCfg();
        }
        return _instance;
    }
}

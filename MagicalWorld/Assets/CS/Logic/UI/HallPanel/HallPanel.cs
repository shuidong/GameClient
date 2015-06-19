using UnityEngine;
using System.Collections;
/// <summary>
/// 主城面板
/// </summary>
public class HallPanel : BasePanel
{
    private static HallPanel _inst;
    public static HallPanel GetInstance()
    {
        if (_inst == null)
        {
            GameObject go = Resources.Load<GameObject>("UI/Panel/HallPanel");
            if (go != null)
            {
                _inst = Util.AddChild<HallPanel>(uiCenterRoot.gameObject, go, false);
            }
        }
        return _inst;
    }
    public override void Start()
    {
        isOpen = false;
        isRealOpen = false;
    }
}

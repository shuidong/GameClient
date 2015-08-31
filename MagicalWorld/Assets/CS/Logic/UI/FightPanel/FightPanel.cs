using UnityEngine;
using System.Collections.Generic;

public class FightPanel : BasePanel
{
    public List<Transform> upPos,downPos;
    private static FightPanel _inst;
    public static FightPanel GetInstance()
    {
        if (_inst == null)
        {
            GameObject go = Resources.Load<GameObject>("UI/Panel/FightPanel");
            if (go != null)
            {
                _inst = Util.AddChild<FightPanel>(uiCenterRoot.gameObject, go, false);
            }
        }
        return _inst;
    }
    // Use this for initialization
    override public void Start () {
	
	}
	
}

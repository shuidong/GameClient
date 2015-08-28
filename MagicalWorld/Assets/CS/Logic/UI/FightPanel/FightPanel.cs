using UnityEngine;
using System.Collections;

public class FightPanel : BasePanel
{
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
	
	// Update is called once per frame
	void Update () {
	
	}
}

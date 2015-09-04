using UnityEngine;
using System.Collections.Generic;

public class FightPanel : BasePanel
{
    public List<Transform> upPos,downPos;
    /// <summary>
    /// 游戏角色模板
    /// </summary>
    public GameObject actorPrefab;
    /// <summary>
    /// 出生点
    /// </summary>
    
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
    protected override void RealOpen()
    {
        MonoActor actor = Util.AddChild<MonoActor>(upPos[0].gameObject,actorPrefab);
        actor.IsPos = true;
        actor.actorData = new ActorData(ShipCfg.GetInstance().GetById(100001));
        actor.Init();
    }

}

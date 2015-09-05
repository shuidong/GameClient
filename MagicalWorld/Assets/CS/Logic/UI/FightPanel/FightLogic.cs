using UnityEngine;
using System.Collections.Generic;

public class FightLogic : MonoBehaviour
{
    public List<Transform> upPos,downPos;
    /// <summary>
    /// 游戏角色模板
    /// </summary>
    private GameObject actorPrefab;
    /// <summary>
    /// 出生点
    /// </summary>
    private Vector3 BPUP = new Vector3(391f, 297f, 0f), BPDOWN = new Vector3(-391f, -297f, 0);
    void Start()
    {
        actorPrefab = GameUtil.LoadRole("Warrior");
        Simulation();
        Init();
    }
    void Update()
    {

    }
    /// <summary>
    /// 初始化战斗
    /// </summary>
    private void Init()
    {
        //创建敌人
        CreatRole(MdlFight.GetInstance().enemyList, upPos, BPUP);
        //创建己方
        CreatRole(MdlFight.GetInstance().ourList, downPos, BPDOWN);
    }
    private void CreatRole(List<ActorData> list, List<Transform> parents, Vector3 BP)
    {
        MonoActor actor = null;
        for (int i = 0; i < 6; ++i)
        {
            actor = Util.AddChild<MonoActor>(parents[i].gameObject, actorPrefab);
            actor.Init(list[i],BP);
        }
    }
    /// <summary>
    /// 模拟数据
    /// </summary>
    private void Simulation()
    {
        MdlFight.GetInstance().enemyList.Clear();
        MdlFight.GetInstance().enemyList.Add(new ActorData(ShipCfg.GetInstance().GetById(100003)));
        MdlFight.GetInstance().enemyList.Add(new ActorData(ShipCfg.GetInstance().GetById(100004)));
        MdlFight.GetInstance().enemyList.Add(new ActorData(ShipCfg.GetInstance().GetById(100002)));
        MdlFight.GetInstance().enemyList.Add(new ActorData(ShipCfg.GetInstance().GetById(100003)));
        MdlFight.GetInstance().enemyList.Add(new ActorData(ShipCfg.GetInstance().GetById(100004)));
        MdlFight.GetInstance().enemyList.Add(new ActorData(ShipCfg.GetInstance().GetById(100002)));

        MdlFight.GetInstance().ourList.Clear();
        MdlFight.GetInstance().ourList.Add(new ActorData(ShipCfg.GetInstance().GetById(100001)));
        MdlFight.GetInstance().ourList.Add(new ActorData(ShipCfg.GetInstance().GetById(100001)));
        MdlFight.GetInstance().ourList.Add(new ActorData(ShipCfg.GetInstance().GetById(100001)));
        MdlFight.GetInstance().ourList.Add(new ActorData(ShipCfg.GetInstance().GetById(100001)));
        MdlFight.GetInstance().ourList.Add(new ActorData(ShipCfg.GetInstance().GetById(100001)));
        MdlFight.GetInstance().ourList.Add(new ActorData(ShipCfg.GetInstance().GetById(100001)));
    }
}

using CG3D.Config;
/// <summary>
/// 角色数据类
/// </summary>
public class ActorData:BaseData
{
    public string name;
    public int level;
    public int HP;
    public int attack;
    /// <summary>
    /// 移动速度
    /// </summary>
    public int moveSpeed;
    /// <summary>
    /// 攻击速度
    /// </summary>
    public int attackSpeed;
    public int maxHP;
    public ActorData (DataAdapter _cfg)
    {
        cfg = _cfg;
        name = cfg.stringOf("name");
        RefData(0, cfg.intOf("hp"), cfg.intOf("attack"), cfg.intOf("moveSpeed"), cfg.intOf("attackSpeed"), cfg.intOf("hp"));
    }
    /// <summary>
    /// 刷新数据
    /// </summary>
    public void RefData(int addLevel = 0,int addHP = 0, int addAttack = 0, int addMoveSpeed = 0, int addAttackSpeed = 0, int addMaxHP = 0)
    {    
        level += addLevel;
        HP += addHP;
        attack += addAttack;
        moveSpeed += addMoveSpeed;
        attackSpeed += addAttackSpeed;
        maxHP += addMaxHP;
    }
    public int ID { get { return cfg.intOf("id"); } }
    public string icon { get { return cfg.stringOf("icon"); } }
    /// <summary>
    /// 拷贝
    /// </summary>
    /// <param name="data"></param>
    public void CopyTo(ActorData data)
    {
        data.cfg = cfg;
        data.name = name;
        data.level = level;
        data.HP = HP;
        data.attack = attack;
        data.moveSpeed = moveSpeed;
        data.attackSpeed = attackSpeed;


    }
   /// <summary>
   /// 比较
   /// </summary>
   /// <param name="data"></param>
   /// <returns></returns>
    public bool CompareTo(ActorData data)
    {
        return false;
    }
}

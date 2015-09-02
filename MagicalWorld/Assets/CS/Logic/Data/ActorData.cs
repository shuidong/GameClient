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
    public ActorData(string _name,int _level,int _hp,int _attack,int _moveSpeed,int _attackSpeed)
    {
        name = _name;
        level = _level;
        HP = _hp;
        attack = _attack;
        moveSpeed = _moveSpeed;
        attackSpeed = _attackSpeed;
    }
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

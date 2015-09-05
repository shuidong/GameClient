using CG3D.Model;
using System.Collections.Generic;
/// <summary>
/// 战斗数据
/// </summary>
public class MdlFight : BaseModel
{
    /// <summary>
    /// 敌军
    /// </summary>
    public List<ActorData> enemyList = new List<ActorData>();
    /// <summary>
    /// 我军
    /// </summary>
    public List<ActorData> ourList = new List<ActorData>();
    private static MdlFight _instance;
    public static MdlFight GetInstance()
    {
        if(_instance == null)
        {
            _instance = new MdlFight();
        }
        return _instance;
    }
}

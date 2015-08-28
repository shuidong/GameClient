using UnityEngine;
using System.Collections;
using CG3D.Utils;
/// <summary>
/// 场景管理类
/// </summary>
public class SceneManager : Single<SceneManager>
{
    /// <summary>
    /// 当前场景 0-GameLogin,1-GameHall，2-GameFight
    /// </summary>
    public MyEnum.SCENE currScene = MyEnum.SCENE.GameLogin;
    /// <summary>
    /// 登陆场景
    /// </summary>
    public static string gameLogin = "GameLogin";
    /// <summary>
    /// 主城场景
    /// </summary>
    public static string gameHall = "GameHall";
    /// <summary>
    /// 战斗场景
    /// </summary>
    public static string gameFight = "GameFight";
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void Load(string name)
    {
        Application.LoadLevel(name);
    }
    public void Load(int index)
    {
        Application.LoadLevel(index);
    }
}


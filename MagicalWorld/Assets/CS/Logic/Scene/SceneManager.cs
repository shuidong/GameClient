using UnityEngine;
using System.Collections;
/// <summary>
/// 场景管理类
/// </summary>
public class SceneManager : Single<SceneManager>
{
    /// <summary>
    /// 当前场景 0-GameLogin,1-GameHall
    /// </summary>
    public int currScene = 0;
    /// <summary>
    /// 登陆场景
    /// </summary>
    public static string gameLogin = "GameLogin";
    /// <summary>
    /// 主城场景
    /// </summary>
    public static string gameHall = "GameHall";
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

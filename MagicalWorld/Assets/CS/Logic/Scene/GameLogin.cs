using UnityEngine;
using System.Collections;
using CG3D.Utils;
/// <summary>
/// 游戏登陆场景
/// </summary>
public class GameLogin : MonoBehaviour {

    public Transform uiCenter;
	// Use this for initialization
	void Start () 
    {
        BasePanel.uiCenterRoot = uiCenter;
        SceneManager.GetInstance().currScene = MyEnum.SCENE.GameLogin;
        LoginPanel.GetInstance().Open();
	}
	
	// Update is called once per frame
	void Update () 
    {
	    
	}
}

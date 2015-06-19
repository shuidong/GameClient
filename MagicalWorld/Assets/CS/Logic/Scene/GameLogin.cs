using UnityEngine;
using System.Collections;
/// <summary>
/// 游戏登陆场景
/// </summary>
public class GameLogin : MonoBehaviour {

    public Transform uiCenter;
	// Use this for initialization
	void Start () 
    {
        BasePanel.uiCenterRoot = uiCenter;
        SceneManager.GetInstance().currScene = 0;
        LoginPanel.GetInstance().Open();
	}
	
	// Update is called once per frame
	void Update () 
    {
	    
	}
}

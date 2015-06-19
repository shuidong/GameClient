using UnityEngine;
using System.Collections;
/// <summary>
/// 主城场景
/// </summary>
public class GameHall : MonoBehaviour 
{
    public Transform uiCenter;
	// Use this for initialization
	void Start () {
        BasePanel.uiCenterRoot = uiCenter;
        SceneManager.GetInstance().currScene = 1;
        HallPanel.GetInstance().Open();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

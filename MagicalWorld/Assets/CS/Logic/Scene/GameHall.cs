using UnityEngine;
using System.Collections;
using CG3D.Utils;
/// <summary>
/// 主城场景
/// </summary>
public class GameHall : MonoBehaviour 
{
    public Transform uiCenter;
	// Use this for initialization
	void Start () {
        BasePanel.uiCenterRoot = uiCenter;
        SceneManager.GetInstance().currScene = MyEnum.SCENE.GameHall;
        HallPanel.GetInstance().Open();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

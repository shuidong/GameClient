using UnityEngine;
using CG3D.Utils;
public class GameFight : MonoBehaviour {
    public Transform uiCenter;
    // Use this for initialization
    void Start () {
        BasePanel.uiCenterRoot = uiCenter;
        SceneManager.GetInstance().currScene = MyEnum.SCENE.GameFight;
        FightPanel.GetInstance().Open();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;
/// <summary>
/// 登陆面板
/// </summary>
public class LoginPanel : BasePanel
{
    private static LoginPanel _inst;
    public static LoginPanel GetInstance()
    {
        if (_inst == null)
        {
            GameObject go = Resources.Load<GameObject>("UI/Panel/LoginPanel");
            if (go != null)
            {
                _inst = Util.AddChild<LoginPanel>(uiCenterRoot.gameObject, go, false);
            }
        }
        return _inst;
    }
    public override void Start()
    {
        isOpen = false;
        isRealOpen = false;
    }
    /// <summary>
    /// 注册
    /// </summary>
    public void OnRegister()
    {

    }
    /// <summary>
    /// 登陆
    /// </summary>
    public void OnLogin()
    {
        SceneManager.GetInstance().Load(SceneManager.gameHall);
    }
}

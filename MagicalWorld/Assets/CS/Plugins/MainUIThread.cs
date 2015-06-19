using UnityEngine;
using System.Collections;
using CLRSharp;
using Utils;
/// <summary>
/// 此类进行L#主逻辑通知
/// </summary>
public class MainUIThread : MonoBehaviour {
    private static MainUIThread ui;
    private NotifysDelegate callBack = new NotifysDelegate();
    public NotifysDelegate CallBack
    {
        get { return callBack; }
    }
    public static MainUIThread GetInstance()
    {
        if (ui == null)
        {
            ui = GameObject.FindObjectOfType(typeof(MainUIThread)) as MainUIThread;
            if (!ui)
            {
                GameObject obj = new GameObject("_MainUIThread");
                GameObject.DontDestroyOnLoad(obj);
                ui = obj.AddComponent<MainUIThread>();
                ICLRType program = LSharpModule.env.GetType("GameUI.Program");
                IMethod main = program.GetMethod("GetInstance", MethodParamList.constEmpty());
                object program_object = main.Invoke(ThreadContext.activeContext, null, null);
                IMethod method_AddNotifys = program.GetMethod("AddNotifys", MethodParamList.Make(LSharpModule.env.GetType(typeof(MainUIThread))));
                method_AddNotifys.Invoke(ThreadContext.activeContext, program_object, new object[] { ui });
            }     
        }
        return ui;
    }
    void Awake()
    {
        
        callBack.Notify(eMonoMessageType.Awake);
    }
	// Use this for initialization
	void Start () 
    {
        callBack.Notify(eMonoMessageType.Start);
	}
	// Update is called once per frame
	void Update () {
        callBack.Notify(eMonoMessageType.Update);
	}
    void OnEnable()
    {
        callBack.Notify(eMonoMessageType.OnEnable);
    }
    void OnDestory()
    {
        callBack.Notify(eMonoMessageType.OnDestory);
    }
    void FixedUpdate()
    {
        callBack.Notify(eMonoMessageType.FixedUpdate);
    }
    void OnDisable()
    {
        callBack.Notify(eMonoMessageType.OnDisable);
    }
    void OnGUI()
    {
        callBack.Notify(eMonoMessageType.OnGUI);
    }
}

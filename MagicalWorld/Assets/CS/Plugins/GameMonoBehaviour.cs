using UnityEngine;
using System.Collections;
using CLRSharp;
using System.Collections.Generic;
namespace Utils
{
    /// <summary>
    /// 此类与L#的逻辑类ScriptMonoBehaviour绑定
    /// </summary>
    public class GameMonoBehaviour : MonoBehaviour
    {
        private IMethod method_notify;
        private static ICLRType class_type;
        private static object inst;//L#传递过来的L#脚本对象
        private static string class_name_L = "";//从L#脚本类传递过来的类名
        public string default_class_name = "GameUI.ScriptMonoBehaviour";//默认实例化L#类
        private NotifysDelegate callBack = new NotifysDelegate();
        public NotifysDelegate CallBack
        {
            get { return callBack; }
        }
        public static GameMonoBehaviour AddComponent(GameObject obj, object _inst, string _class_name)
        {
            GameMonoBehaviour ui = null;
            
            if (obj != null)
            {
                 ui = obj.GetComponent<GameMonoBehaviour>();
                 if (ui == null)
                 {
                    ui = obj.AddComponent<GameMonoBehaviour>();
                 }          
            }
            inst = _inst;
            class_name_L = _class_name;
            return ui;
        }
        void Awake()
        {
            Init();
            Notify(eMonoMessageType.Awake);
        }
        void OnEnable()
        {
            Notify(eMonoMessageType.OnEnable);
        }
        void Start()
        {
            Notify(eMonoMessageType.Start);
        }
        void Update()
        {
            Notify(eMonoMessageType.Update);
        }
        void OnDestory()
        {
            Notify(eMonoMessageType.OnDestory);
        }
        void FixedUpdate()
        {
            Notify(eMonoMessageType.FixedUpdate);
        }
        void OnDisable()
        {
            Notify(eMonoMessageType.OnDisable);
        }
        void OnGUI()
        {
            Notify(eMonoMessageType.OnGUI);
        }
        private void Init()
        {
            if (class_name_L == "")
            {
                class_name_L = default_class_name;
                Debug.LogWarning("传入的L#类名为空,将使用L#基类进行绑定!");
            }
            class_type = LSharpModule.env.GetType(class_name_L);
            if (inst == null)
            {
                IMethod ctor = class_type.GetMethod(".ctor",MethodParamList.Make(LSharpModule.env.GetType(typeof(GameObject))));
                inst = ctor.Invoke(ThreadContext.activeContext,null,new object[]{gameObject});
            }
        }
        public void Notify(eMonoMessageType mType,params object[] data)
        {
            callBack.Notify(mType,data);
            if (method_notify == null)
            {
                method_notify = class_type.GetMethod("Notify", MethodParamList.Make(LSharpModule.env.GetType(typeof(eMonoMessageType))));  
            }
            method_notify.Invoke(ThreadContext.activeContext, inst, new object[] { mType });
        }
    }
}


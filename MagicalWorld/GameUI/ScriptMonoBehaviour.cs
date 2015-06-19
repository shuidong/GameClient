using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;
using CLRSharp;
using Utils;
namespace GameUI
{
    /// <summary>
    /// L#脚本逻辑基类
    /// </summary>
    public class ScriptMonoBehaviour : MonoInterface
    {
        protected GameMonoBehaviour mono_ui;
        protected string class_name = "GameUI.ScriptMonoBehaviour";//自身类名(包含命名空间)，必须是继承自L#的 ScriptMonoBehaviour
        public ScriptMonoBehaviour()
        {
            Init(null);
        }
        public ScriptMonoBehaviour(GameObject obj)
        {
            if (obj == null)
            {
                obj = new GameObject();
                obj.name = class_name;
            }
            Init(obj);
        }
        //添加需要通知的函数
        protected virtual void AddNotifys()
        {
            if (mono_ui != null)
            {
                mono_ui.CallBack.Add(eMonoMessageType.Awake, Awake);
                mono_ui.CallBack.Add(eMonoMessageType.OnEnable, OnEnable);
                mono_ui.CallBack.Add(eMonoMessageType.Start, Start);
                mono_ui.CallBack.Add(eMonoMessageType.Update, Update);
                mono_ui.CallBack.Add(eMonoMessageType.OnDestory, OnDestory);
                mono_ui.CallBack.Add(eMonoMessageType.FixedUpdate, FixedUpdate);
                mono_ui.CallBack.Add(eMonoMessageType.OnDisable, OnDisable);
                mono_ui.CallBack.Add(eMonoMessageType.OnGUI, OnGUI);
            }
           
        }
        protected virtual void RemoveNotifys()
        {
            if (mono_ui != null)
            {
                mono_ui.CallBack.Remove(eMonoMessageType.Awake, Awake);
                mono_ui.CallBack.Remove(eMonoMessageType.OnEnable, OnEnable);
                mono_ui.CallBack.Remove(eMonoMessageType.Start, Start);
                mono_ui.CallBack.Remove(eMonoMessageType.Update, Update);
                mono_ui.CallBack.Remove(eMonoMessageType.OnDestory, OnDestory);
                mono_ui.CallBack.Remove(eMonoMessageType.FixedUpdate, FixedUpdate);
                mono_ui.CallBack.Remove(eMonoMessageType.OnDisable, OnDisable);
                mono_ui.CallBack.Remove(eMonoMessageType.OnGUI, OnGUI);
            }
        }
        protected virtual void Init(GameObject obj)
        { 
            mono_ui = GameMonoBehaviour.AddComponent(obj, this,class_name);
            AddNotifys();
        }
        //常规L#通知方法
        public virtual void Notify(eMonoMessageType message)
        {
           
        }
        public virtual void Awake(params object[] data)
        {
           
        }
        public virtual void OnEnable(params object[] data)
        {

        }
        public virtual void Start(params object[] data)
        {

        }
        public virtual void Update(params object[] data)
        {
            Debug.Log("进行刷新!");
        }
        public virtual void OnDestory(params object[] data)
        {
            RemoveNotifys();
        }
        public virtual void FixedUpdate(params object[] data)
        {

        }
        public virtual void OnDisable(params object[] data)
        {

        }
        public virtual void OnGUI(params object[] data)
        {

        }
    }
}

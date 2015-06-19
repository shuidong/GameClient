using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Utils;
using Core.Utils;
using GameUI.Message;
namespace GameUI
{
    /// <summary>
    /// 此类为L#主逻辑线程，生命周期为从L#模块加载完成创建此类实例一直到游戏结束
    /// </summary>
    public class Program : MonoInterface
    {
        private static Program instance;
        private MainUIThread main_mono;
        protected Program()
        {

        }
        public static Program GetInstance()
        {
            if (instance == null)
            {
                instance = new Program();
            }
            return instance;
        }
        public void AddNotifys(MainUIThread mono)
        {
            main_mono = mono;
            if (main_mono != null)
            {
                main_mono.CallBack.Add(eMonoMessageType.Awake, instance.Awake);
                main_mono.CallBack.Add(eMonoMessageType.OnEnable, instance.OnEnable);
                main_mono.CallBack.Add(eMonoMessageType.Start, instance.Start);
                main_mono.CallBack.Add(eMonoMessageType.Update, instance.Update);
                main_mono.CallBack.Add(eMonoMessageType.OnDestory, instance.OnDestory);
                main_mono.CallBack.Add(eMonoMessageType.FixedUpdate, instance.FixedUpdate);
                main_mono.CallBack.Add(eMonoMessageType.OnDisable, instance.OnDisable);
                main_mono.CallBack.Add(eMonoMessageType.OnGUI, instance.OnGUI);
            }
          
        }
        private void RemoveNotifys()
        {
            if (main_mono != null)
            {
                main_mono.CallBack.Remove(eMonoMessageType.Awake, Awake);
                main_mono.CallBack.Remove(eMonoMessageType.OnEnable, OnEnable);
                main_mono.CallBack.Remove(eMonoMessageType.Start, Start);
                main_mono.CallBack.Remove(eMonoMessageType.Update, Update);
                main_mono.CallBack.Remove(eMonoMessageType.OnDestory, OnDestory);
                main_mono.CallBack.Remove(eMonoMessageType.FixedUpdate, FixedUpdate);
                main_mono.CallBack.Remove(eMonoMessageType.OnDisable, OnDisable);
                main_mono.CallBack.Remove(eMonoMessageType.OnGUI, OnGUI);
            }
        }
        public void Awake(params object[] data)
        {
            MyDebug.isShowLog = true;
        }
        public void OnEnable(params object[] data)
        {

        }
        public void Start(params object[] data)
        {
            
        }
        public void Update(params object[] data)
        {
            //Debug.Log("Program进行刷新!");
        }
        public void OnDestory(params object[] data)
        {
            RemoveNotifys();
        }
        public void FixedUpdate(params object[] data)
        {

        }
        public void OnDisable(params object[] data)
        {

        }
        public void OnGUI(params object[] data)
        {

        }

    }
}

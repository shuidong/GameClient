using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Utils
{
    /// <summary>
    /// 此类进行委托调用
    /// </summary>
    public class NotifysDelegate
    {
        public delegate void CallBackDelegate(params object[] data);
        private static Dictionary<eMonoMessageType, CallBackDelegate> notifys;
        private static NotifysDelegate instance;
        public NotifysDelegate()
        {
            notifys = new Dictionary<eMonoMessageType, CallBackDelegate>();
        }
        /// <summary>
        /// 添加需要回调的委托
        /// </summary>
        /// <param name="mType"></param>
        /// <param name="call"></param>
        public void Add(eMonoMessageType mType, CallBackDelegate call)
        {
            CallBackDelegate notity = null;
            notifys.TryGetValue(mType, out notity);
            notity += call;
            notifys.Add(mType, call);            
        }
        /// <summary>
        /// 移除委托
        /// </summary>
        /// <param name="mType"></param>
        /// <param name="call"></param>
        public void Remove(eMonoMessageType mType, CallBackDelegate call)
        {
            CallBackDelegate notity = null;
            notifys.TryGetValue(mType, out notity);
            if (notity == null)
            {
                if (notifys.ContainsKey(mType))
                {
                    notifys.Remove(mType);
                }
                return;  
            }
            notity -= call;
        }
        /// <summary>
        /// 移除所有委托
        /// </summary>
        public void RemoveAll()
        {
            notifys.Clear();
        }
        /// <summary>
        /// 调用需要通知的委托
        /// </summary>
        /// <param name="mType"></param>
        /// <param name="data"></param>
        public void Notify(eMonoMessageType mType, params object[] data)
        {
            CallBackDelegate call = null;
            notifys.TryGetValue(mType, out call);
            if (call != null)
            {
                call(data);
            }
        }
        /// <summary>
        /// 调用所有委托
        /// </summary>
        /// <param name="data"></param>
        public void NotifyAll(params object[] data)
        {
            CallBackDelegate call = null;
            List<eMonoMessageType> key_list = new List<eMonoMessageType>();
            key_list.Clear();
            key_list.AddRange(notifys.Keys);
            for (int i=0;i<key_list.Count;++i)
            {
                notifys.TryGetValue(key_list[i],out call);
                if (call != null)
                {
                    call(data);
                }
                
            }
        }
    }
    /// <summary>
    /// 委托类型
    /// </summary>
    public enum eMonoMessageType
    {
        Awake,
        OnEnable,
        Start,
        Update,
        FixedUpdate,
        OnDestory,
        OnDisable,
        OnGUI,
    }
}
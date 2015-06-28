using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;
/// <summary>
/// 系统监听方式
/// </summary>
public class UGUIEventListener
{
    public static EventTrigger Get(GameObject go)
    {
        if (go == null)
        {
            return null;
        }
        EventTrigger trigger;
        trigger = go.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = go.gameObject.AddComponent<EventTrigger>();
        }
        return trigger;
    }
    private static EventTrigger.Entry NewTrigger(EventTriggerType id,UnityAction<BaseEventData> callback)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = id;
        entry.callback.AddListener(callback);
        return entry;
    }
    /// <summary>
    /// 添加监听
    /// </summary>
    /// <param name="mono"></param>
    /// <param name="eventType"></param>
    /// <param name="callback"></param>
    public static void OnEvent(GameObject go, EventTriggerType eventType, UnityAction<BaseEventData> callback)
    {
        EventTrigger trigger = Get(go);
        if (trigger == null)
        {
            return;
        }
        trigger.triggers.Add(NewTrigger(eventType, callback));
    }
    /// <summary>
    /// 移除指定类型的监听
    /// </summary>
    /// <param name="mono"></param>
    /// <param name="type"></param>
    /// <param name="callback"></param>
    public static void RemoveTrigger(GameObject go, EventTriggerType type, UnityAction<BaseEventData> callback)
    {
        EventTrigger trigger = Get(go);
        if (trigger == null)
        {
            return;
        }
        for(int i=0;i<trigger.triggers.Count;++i)
        {
            if (trigger.triggers[i].eventID == type)
            {
                trigger.triggers[i].callback.RemoveListener(callback);
            }
        }
    }
    /// <summary>
    /// 移除所有该函数的监听
    /// </summary>
    /// <param name="mono"></param>
    /// <param name="callback"></param>
    public static void RemoveTrigger(GameObject go, UnityAction<BaseEventData> callback)
    {
        EventTrigger trigger = Get(go);
        if (trigger == null)
        {
            return;
        }
        for (int i = 0; i < trigger.triggers.Count; ++i)
        {
            trigger.triggers[i].callback.RemoveListener(callback);
        }
    }
    /// <summary>
    /// 开始拖动
    /// </summary>
    /// <param name="mono"></param>
    /// <param name="callback"></param>
    public static void OnBeginDrag(GameObject go, UnityAction<BaseEventData> callback)
    {
        OnEvent(go, EventTriggerType.BeginDrag, callback);
    }
    /// <summary>
    /// 取消
    /// </summary>
    /// <param name="mono"></param>
    /// <param name="callback"></param>
    public static void OnCancel(GameObject go, UnityAction<BaseEventData> callback)
    {
        OnEvent(go, EventTriggerType.Cancel, callback);
    }
    public static void OnDeselect(GameObject go, UnityAction<BaseEventData> callback)
    {
        OnEvent(go, EventTriggerType.Deselect, callback);
    }
    /// <summary>
    /// 拖动
    /// </summary>
    /// <param name="mono"></param>
    /// <param name="callback"></param>
    public static void OnDrag(GameObject go, UnityAction<BaseEventData> callback)
    {
        OnEvent(go, EventTriggerType.Drag, callback);
    }
    public static void OnDrop(GameObject go, UnityAction<BaseEventData> callback)
    {
        OnEvent(go, EventTriggerType.Drop, callback);
    }
    public static void OnEndDrag(GameObject go, UnityAction<BaseEventData> callback)
    {
        OnEvent(go, EventTriggerType.EndDrag, callback);
    }
    public static void OnInitializeDrag(GameObject go, UnityAction<BaseEventData> callback)
    {
        OnEvent(go, EventTriggerType.InitializePotentialDrag, callback);
    }
    public static void OnMove(GameObject go, UnityAction<BaseEventData> callback)
    {
        OnEvent(go, EventTriggerType.Move, callback);
    }
    public static void OnClick(GameObject go, UnityAction<BaseEventData> callback)
    {
        OnEvent(go, EventTriggerType.PointerClick, callback);
    }

    public static void OnDown(GameObject go, UnityAction<BaseEventData> callback)
    {
        OnEvent(go, EventTriggerType.PointerDown, callback);
    }
    public static void OnEnter(GameObject go, UnityAction<BaseEventData> callback)
    {
        OnEvent(go, EventTriggerType.PointerEnter, callback);
    }
    public static void OnExit(GameObject go, UnityAction<BaseEventData> callback)
    {
        OnEvent(go, EventTriggerType.PointerExit, callback);
    }
    public static void OnUp(GameObject go, UnityAction<BaseEventData> callback)
    {
        OnEvent(go, EventTriggerType.PointerUp, callback);
    }
    public static void OnScroll(GameObject go, UnityAction<BaseEventData> callback)
    {
        OnEvent(go, EventTriggerType.Scroll, callback);
    }
    public static void OnSelect(GameObject go, UnityAction<BaseEventData> callback)
    {
        OnEvent(go, EventTriggerType.Select, callback);
    }
    public static void OnSubmit(GameObject go, UnityAction<BaseEventData> callback)
    {
        OnEvent(go, EventTriggerType.Submit, callback);
    }
    public static void OnUpdateSelected(GameObject go, UnityAction<BaseEventData> callback)
    {
        OnEvent(go, EventTriggerType.UpdateSelected, callback);
    }
}

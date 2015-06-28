using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Events;
/// <summary>
/// UGUI事件监听
/// </summary>
public class EventTriggerListener : EventTrigger
{
    public delegate void BaseEventDelegate(BaseEventData eventData);
    public delegate void PointerEventDelegate(PointerEventData eventData);
    public delegate void AxisEventDelegate(AxisEventData eventData);
    public PointerEventDelegate onBeginDrag;
    public BaseEventDelegate onCancel;
    public BaseEventDelegate onDeselect;
    public PointerEventDelegate onDrag;
    public PointerEventDelegate onDrop;
    public PointerEventDelegate onEndDrag;
    public PointerEventDelegate onInitializeDrag;
    public AxisEventDelegate onMove;
    public PointerEventDelegate onClick;
    public PointerEventDelegate onDown;
    public PointerEventDelegate onEnter;
    public PointerEventDelegate onExit;
    public PointerEventDelegate onUp;
    public PointerEventDelegate onScroll;
    public BaseEventDelegate onSelect;
    public BaseEventDelegate onSubmit;
    public BaseEventDelegate onUpdateSelected;
    public static EventTriggerListener Get(GameObject go)
    {
        if (go == null)
        {
            return null;
        }
        EventTriggerListener trigger;
        trigger = go.GetComponent<EventTriggerListener>();
        if (trigger == null)
        {
            trigger = go.gameObject.AddComponent<EventTriggerListener>();
        }
        return trigger;
    }
        public override void OnPointerEnter(PointerEventData eventData)
        {   
            base.OnPointerEnter(eventData);
            if (onEnter != null) onEnter(eventData);
            
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            if (onExit != null) onExit(eventData);
        }

        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);
            if (onDrag != null) onDrag(eventData);
        }

        public override void OnDrop(PointerEventData eventData)
        {
            base.OnDrop(eventData);
            if (onDrop != null) onDrop(eventData);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            if (onDown != null) onDown(eventData);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            if (onUp != null) onUp(eventData);
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            if (onClick != null) onClick(eventData);
        }

        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            if (onSelect != null) onSelect(eventData);
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
            if (onDeselect != null) onDeselect(eventData);
        }

        public override void OnScroll(PointerEventData eventData)
        {
            base.OnScroll(eventData);
            if (onScroll != null) onScroll(eventData);
        }

        public override void OnMove(AxisEventData eventData)
        {
            base.OnMove(eventData);
            if (onMove != null) onMove(eventData);
        }

        public override void OnUpdateSelected(BaseEventData eventData)
        {
            base.OnUpdateSelected(eventData);
            if (onUpdateSelected != null) onUpdateSelected(eventData);
        }

        public override void OnInitializePotentialDrag(PointerEventData eventData)
        {
            base.OnInitializePotentialDrag(eventData);
            if (onInitializeDrag != null) onInitializeDrag(eventData);
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
            if (onBeginDrag != null) onBeginDrag(eventData);
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            if (onEndDrag != null) onEndDrag(eventData);
        }

        public override void OnSubmit(BaseEventData eventData)
        {
            base.OnSubmit(eventData);
            if (onSubmit != null) onSubmit(eventData);
        }

        public override void OnCancel(BaseEventData eventData)
        {
            base.OnCancel(eventData);
            if (onCancel != null) onCancel(eventData);
        }
}

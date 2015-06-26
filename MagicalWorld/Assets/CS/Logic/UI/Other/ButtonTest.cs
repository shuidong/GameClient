using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
public class ButtonTest : MonoBehaviour
{
   void Start()
    {
       EventTrigger trigger = gameObject.AddComponent<EventTrigger>();
       EventTrigger.Entry click =  new EventTrigger.Entry();
       click.eventID = EventTriggerType.PointerClick;
       click.callback = new ClickEvet();
       trigger.delegates.Add(click);
    }
}
public class ClickEvet:EventTrigger.TriggerEvent
{

}

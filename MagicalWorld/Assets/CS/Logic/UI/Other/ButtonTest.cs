using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class ButtonTest : MonoBehaviour
{
   void Start()
    {
        EventTriggerListener.Get(gameObject).onClick = OnClick;
    }
   private void OnClick(PointerEventData eventData)
   {
       Debug.Log("被点击");
   }
  
}

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
/// <summary>
/// 主城面板
/// </summary>
public class HallPanel : BasePanel
{
    /// <summary>
    /// 地图
    /// </summary>
    public Transform map;
    /// <summary>
    /// 菜单按钮
    /// </summary>
    public Transform menu;
    /// <summary>
    /// 功能按钮
    /// </summary>
    public Transform mIconA;

    private static HallPanel _inst;
    public static HallPanel GetInstance()
    {
        if (_inst == null)
        {
            GameObject go = Resources.Load<GameObject>("UI/Panel/HallPanel");
            if (go != null)
            {
                _inst = Util.AddChild<HallPanel>(uiCenterRoot.gameObject, go, false);
            }
        }
        return _inst;
    }
    public override void Start()
    {
        isOpen = false;
        isRealOpen = false;
    }
    protected override void RealOpen()
    {
        if (map != null)
        {
            //EventTriggerListener.Get(map.gameObject).onDrag = OnDragMap;
        }
        if(menu != null)
        {
            EventTriggerListener.Get(menu.gameObject).onClick = OnClickMenu;
        }
    }
    /// <summary>
    /// 拖动地图
    /// </summary>
    /// <param name="eventData"></param>
    float xMin = -509f, xMax = 512f, yMin = -381f, yMax = 453f;
    private void OnDragMap(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("拖动地图:"+eventData.delta);
            if(map != null)
            {
                float x = 0,y=0;
                x = map.localPosition.x + eventData.delta.x;
                if (x < xMin)
                {
                    x = xMin;
                }
                if (x > xMax)
                {
                    x = xMax;
                }
                y = map.localPosition.y+eventData.delta.y;
                if (y < yMin)
                {
                    y = yMin;
                }
                if (y > yMax)
                {
                    y = yMax;
                }
                map.localPosition = new Vector3(x,y,map.localPosition.z);
            }
        }
    }
    bool IsRot = true;
    private void OnClickMenu(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            Vector3 pos = new Vector3(442f,-341f,0);
            Debug.Log("打开菜单");
            if(menu != null)
            {
                float z = 0;
                if(IsRot)
                {
                    IsRot = false;
                    z = 15f;
                    pos = new Vector3(-183.7f, -341f, 0);
                }else
                {
                    IsRot = true;
                }
                TweenRotation.Begin(menu.gameObject, 0.2f, new Quaternion(0,0,z,0));
            }
            if(mIconA != null)
            {
                TweenPosition.Begin(mIconA.gameObject, 0.5f, pos);
            }
        }
    }
}

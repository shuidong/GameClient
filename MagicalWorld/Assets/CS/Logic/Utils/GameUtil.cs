using UnityEngine;
using System.Collections;
using Core.Utils;
/// <summary>
/// 游戏工具类
/// </summary>
public class GameUtil
{
    /// <summary>
    /// 加载Sprite
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static Sprite LoadSprite(string name)
    {
        Sprite sprite = null;
        GameObject obj = Resources.Load("Sprite/" + name) as GameObject;
        if (obj == null)
        {
            MyDebug.LogError("未找到Sprite：" + name);
            return null;
        }
        SpriteRenderer render = obj.GetComponent<SpriteRenderer>();
        sprite = render.sprite;
        return sprite;
    }
    /// <summary>
    /// 加载角色模板
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static GameObject LoadRole(string name)
    {
        GameObject obj = null;
        obj = Resources.Load("Role/" + name) as GameObject;
        if (obj == null)
        {
            MyDebug.LogError("未找到Role：" + name);
            return null;
        }
        return obj;
    }
}

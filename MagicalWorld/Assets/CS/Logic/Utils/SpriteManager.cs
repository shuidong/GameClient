using UnityEngine;
using Core.Utils;
/// <summary>
/// 加载Sprite类
/// </summary>
public class SpriteManager:Single<SpriteManager>
{
    /// <summary>
    ///加载Sprite
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public  Sprite Load(string path)
    {
        Sprite sprite = null;
        GameObject obj = Resources.Load("Sprite/" + path) as GameObject;
        if (obj == null)
        {
            MyDebug.LogError("未找到：" + path);
            return null;
        }
        SpriteRenderer render = obj.GetComponent<SpriteRenderer>();
        sprite = render.sprite;
        return sprite;
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 界面管理工具
/// </summary>
public class PanelUtil
{
    /// <summary>
    /// 一个每次打开界面就加1的变量
    /// </summary>
    private static int currMaxIndex = 1;
    private static bool isClosingPanel = false;
    /// <summary>
    /// 记录打开的界面
    /// 数字最大的表示最近打开的界面，为0表示已经关闭了的
    /// </summary>
    private static Dictionary<BasePanel, int> dictOpenedPanel = new Dictionary<BasePanel, int>();
    /// <summary>
    /// 记录打开的界面
    /// </summary>
    /// <param name="panel"></param>
    public static void RecordOpenPanel(BasePanel panel)
    {
        if (isClosingPanel)
        {
            Debug.LogError("关闭界面时打开：" + panel);
            return;
        }
        //Debug.Log("open:" + panel);
        if (dictOpenedPanel.ContainsKey(panel))
        {
            dictOpenedPanel[panel] = currMaxIndex;
        }
        else
        {
            dictOpenedPanel.Add(panel, currMaxIndex);
        }
        currMaxIndex++;
    }
    /// <summary>
    /// 记录关闭的界面
    /// </summary>
    /// <param name="panel"></param>
    public static void RecordClosePanel(BasePanel panel)
    {
        if (isClosingPanel)
        {
            return;
        }
        //Debug.Log("close:" + panel);
        if (dictOpenedPanel.ContainsKey(panel))
        {
            dictOpenedPanel.Remove(panel);
        }
    }
    /// <summary>
    /// 将所有已经记录为打开的界面关闭
    /// </summary>
    public static void CloseAllPanel()
    {
        isClosingPanel = true;
        BasePanel.backPanel = null;
        foreach (BasePanel panel in dictOpenedPanel.Keys)
        {
            if (dictOpenedPanel[panel] > 0)
            {
                //Debug.Log("close all:" + panel);
                panel.Close();
            }
        }
        dictOpenedPanel.Clear();
        isClosingPanel = false;
    }
    /// <summary>
    /// 清除所有记录
    /// </summary>
    public static void ClearRecord()
    {
        dictOpenedPanel.Clear();
    }
}

using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
public class ToolsEditor :Editor 
{
    /// <summary>
    /// 制作图片的预设
    /// </summary>
    [MenuItem("MyMenu/制作图片prefab")]
    static private void MakeAtlas()
    {
        string spriteDir = Application.dataPath + "/Resources/Sprite";

        if (!Directory.Exists(spriteDir))
        {
            Directory.CreateDirectory(spriteDir);
        }

        DirectoryInfo rootDirInfo = new DirectoryInfo(Application.dataPath + "/MyUI/Atlas");
        foreach (DirectoryInfo dirInfo in rootDirInfo.GetDirectories())
        {
            foreach (FileInfo pngFile in dirInfo.GetFiles("*.png", SearchOption.AllDirectories))
            {
                string allPath = pngFile.FullName;
                string assetPath = allPath.Substring(allPath.IndexOf("Assets"));
                Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);
                GameObject go = new GameObject(sprite.name);
                go.AddComponent<SpriteRenderer>().sprite = sprite;
                allPath = spriteDir + "/" + sprite.name + ".prefab";
                string prefabPath = allPath.Substring(allPath.IndexOf("Assets"));
                PrefabUtility.CreatePrefab(prefabPath, go);
                GameObject.DestroyImmediate(go);
            }
        }
    }
    /// <summary>
    /// 获取选择文件路径
    /// </summary>

    static public string GetSelectionFolder()
    {
        if (Selection.activeObject != null)
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject.GetInstanceID());

            if (!string.IsNullOrEmpty(path))
            {
                int dot = path.LastIndexOf('.');
                int slash = Mathf.Max(path.LastIndexOf('/'), path.LastIndexOf('\\'));
                if (slash > 0) return (dot > slash) ? path.Substring(0, slash + 1) : path + "/";
            }
        }
        return "Assets/";
    }
}

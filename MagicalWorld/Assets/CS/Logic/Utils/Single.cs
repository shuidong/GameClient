using UnityEngine;
using System.Collections;
/// <summary>
/// 单例类
/// </summary>
/// <typeparam name="T"></typeparam>
public class Single<T> : MonoBehaviour  where T :MonoBehaviour
{
    private static T _inst;
    public static T GetInstance()
    {
        if (_inst == null)
        {
            GameObject goInstance = GameObject.Find(Const.STR_STATIC_INSTANCE_NAME);
            if (goInstance == null)
            {
                goInstance = new GameObject(Const.STR_STATIC_INSTANCE_NAME);
                Object.DontDestroyOnLoad(goInstance);
            }
            _inst = goInstance.GetComponent<T>();
            if (_inst == null)
            {
                _inst = goInstance.AddComponent<T>();
            }
        }
        return _inst;
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

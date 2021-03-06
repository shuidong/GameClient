using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.IO;
using System.Collections;
using System.Xml;
using Core.Utils;
namespace Core.Resource
{
	public class ResourceManager : MonoBehaviour
	{
		public enum CALLBACK_TYPE
		{
			SUCCESS,
			PROGRESS,
			ERROR
		}
		public delegate void CallbackDelegate(string url, CALLBACK_TYPE type, object data);
		public delegate void WWWLoadDelegate(string url, byte[] data);

		private static ResourceManager _inst;
		//private static bool isShuttingDown = false;
		private bool hasStartup = false;
		private string _baseWWWUrl = "";
		private string _baseLocalUrl = "";
		public uint maxLoadingNum = 5;
		private List<ResourceInfo> listResourceInfo = new List<ResourceInfo>();
		/** 资源加载字典（url，WWW） */
		private Dictionary<string, WWW> dictWWW = new Dictionary<string, WWW>();
		private Dictionary<string, List<CallbackDelegate>> dictCallback = new Dictionary<string, List<CallbackDelegate>>();
		//private float gcTime = 0.0f;

		public static ResourceManager GetInstance()
		{
			if (_inst == null)
			{
				GameObject goInstance = GameObject.Find(Const.STR_STATIC_INSTANCE_NAME);
				if (goInstance == null)
				{
					goInstance = new GameObject(Const.STR_STATIC_INSTANCE_NAME);
					UnityEngine.Object.DontDestroyOnLoad(goInstance);
				}
				_inst = goInstance.AddComponent<ResourceManager>();
			}
			return _inst;
		}
		void OnApplicationQuit()
		{
			//isShuttingDown = true;
		}

		void Update()
		{
			// 调用资源回收
			//gcTime += Time.deltaTime;
			//if(gcTime>10.0f)
			//{
			//	gcTime = 0.0f;
			//	Resources.UnloadUnusedAssets();
			//	System.GC.Collect();
			//}
			
			//检查加载情况，通知进度或结果
			foreach(string url in dictWWW.Keys)
			{
				if(dictWWW[url].isDone)
				{
					ResourceInfo info = GetResourceInfo(url);
					if(dictWWW[url].error != null)
					{
						info.loadState = ResourceInfo.LOAD_STATE.ERROR;
						SendToCallback(url, CALLBACK_TYPE.ERROR, dictWWW[url].url);
						MyDebug.LogError("URL ERROR:" + dictWWW[url].url + "(" + dictWWW[url].error + ")");
						listResourceInfo.Remove(info);
					}
					else
					{
						MyDebug.Log("加载成功:" + dictWWW[url].url);
						info.loadState = ResourceInfo.LOAD_STATE.LOADED;
						info.data = Decryption(dictWWW[url].bytes);
						SendToCallback(url, CALLBACK_TYPE.SUCCESS, info.data);
					}
					dictWWW.Remove(url);
					dictCallback[url].Clear();
					break;//如果不加break会出现异常（InvalidOperationException: out of sync），应该是remove后引起Dictionary的MoveNext方法为空？
				}
				else
				{
					SendToCallback(url, CALLBACK_TYPE.PROGRESS, dictWWW[url].progress);
				}
			}
			//是否要加载下一个资源
			if(dictWWW.Count < maxLoadingNum)
			{
				ResourceInfo info = null;
				for (int i = 0; i < listResourceInfo.Count; i++)
				{
					if (listResourceInfo[i].loadState == ResourceInfo.LOAD_STATE.WAIT)
					{
						info = listResourceInfo[i];
						break;
					}
				}
				if(info != null)
				{
					if(File.Exists(info.fileLocalUrl))
					{
						if(Application.platform == RuntimePlatform.IPhonePlayer)
						{
							StartCoroutine(WWWload(info.url, _baseLocalUrl + info.url));
						}
						else
						{
							StartCoroutine(WWWload(info.url, "file:///" + _baseLocalUrl + info.url));
						}
					}
					else
					{
						string file = info.url.Substring(0, info.url.LastIndexOf("."));
						TextAsset t = (TextAsset)Resources.Load(file);

						if(t != null)
						{
							MyDebug.Log("从Resources加载：" + info.url);
							info.data = t.text;
							info.loadState = ResourceInfo.LOAD_STATE.LOADED;
							SendToCallback(info.url, CALLBACK_TYPE.SUCCESS, info.data);
						}
						else
						{
							StartCoroutine(WWWload(info.url, (info.isFullUrl ? "" : _baseWWWUrl) + info.url));
						}
					}

					info.loadState = ResourceInfo.LOAD_STATE.LOADING;
				}
			}
		}
		IEnumerator WWWload(string key, string url)
		{
			//MyDebug.Log("WWWLoad：" + url);
			WWW www = null;
			try
			{
				www = new WWW(url);
			}
			catch(System.Exception ex)
			{
				MyDebug.LogWarning("url:" + url + "exception:" + ex.ToString());
			}
			dictWWW[key] = www;
			yield return www;
		}
		private ResourceInfo GetResourceInfo(string url)
		{
			for (int i = 0; i < listResourceInfo.Count; i++)
			{
				if (listResourceInfo[i].url == url)
				{
					return listResourceInfo[i];
				}
			}
			return null;
		}
		/** 返回资源信息给回调方法 */
		private void SendToCallback(string url, CALLBACK_TYPE type, object data)
		{
			foreach(string key in dictCallback.Keys)
			{
				if(key == url)
				{
					List<CallbackDelegate> list = dictCallback[url];
					foreach(CallbackDelegate d in list)
					{
						d(url, type, data);
					}
					break;
				}
			}
		}
		public void LoadResourceAsyc(string url, CallbackDelegate callback)
		{
			LoadResourceAsyc(url, callback, false);
		}
		public void LoadResourceAsyc(string url, CallbackDelegate callback, bool fullUrl)
		{
			//MyDebug.Log("LoadResourceAsyc:" + url);
			ResourceInfo info = GetResourceInfo(url);
			if(info != null && info.loadState == ResourceInfo.LOAD_STATE.LOADED)
			{
				callback(url, CALLBACK_TYPE.SUCCESS, info.data);
				return;
			}
			if(info == null)
			{
				info = new ResourceInfo();
				info.isFullUrl = fullUrl;
				info.url = url;
				listResourceInfo.Add(info);
			}

			if(callback != null)
			{
				if(!dictCallback.ContainsKey(url))
				{
					dictCallback.Add(url, new List<CallbackDelegate>());
				}
				dictCallback[url].Add(callback);
			}
		}
		private byte[] Decryption(byte[] data)
		{
			return data;
		}
		public void Startup(string host)
		{
			if(hasStartup)
			{
				return;
			}
			_baseWWWUrl = "http://" + host + "/CG3D/";
			switch(Application.platform)
			{
				case RuntimePlatform.Android:
					_baseWWWUrl += "android/";
					break;
				case RuntimePlatform.IPhonePlayer:
                    //if (SDKToIOS.currPlatform == SDKToIOS.SDK_PLATFORM.NEIGOU)
                    //{
                    //    _baseWWWUrl += "app/";//苹果正版
                    //}
                    //else
                    //{
                    //    _baseWWWUrl += "ios/";//苹果越狱版
                    //}
					break;
				default:
					_baseWWWUrl += "windows/";
					//_baseWWWUrl += "ios/";
					break;
			}

			_baseLocalUrl = Application.persistentDataPath + "/";

			hasStartup = true;
		}
		public string baseWWWUrl { get { return _baseWWWUrl; } }
		class ResourceInfo
		{
			public enum LOAD_STATE
			{
				WAIT = 0,
				LOADING,
				LOADED,
				ERROR
			}
			public bool isFullUrl;
			public string url = null;
			public object data;
			/** 加载状态 */
			public LOAD_STATE loadState = LOAD_STATE.WAIT;
			public string fileLocalUrl
			{
				get
				{
					return Application.persistentDataPath + "/" + url;
				}
			}
		}
	}
}

using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.IO;
using Core.Utils;

namespace Core.Resource
{
	/// <summary>
	/// 断点下载
	/// </summary>
	public class BPLoad : MonoBehaviour
	{
		public enum CALLBACK_TYPE
		{
			SUCCESS,
			PROGRESS,
			ERROR
		}
		public delegate void CallbackDelegate(string url, CALLBACK_TYPE type, object data, object data2, float progress);

		private static BPLoad _inst;

		private CallbackDelegate callBack;
		void Start()
		{
			// Application.backgroundLoadingPriority = ThreadPriority.Low;
			// l =  Application.LoadLevelAsync("game");
			//StartCoroutine(FPointDown("http://127.0.0.1:8888/index.rar", "d:/a.rar"));
			//StartCoroutine(FPointDown(@"http://dl_dir.qq.com/qqfile/qq/QQ2012/QQ2012Beta3.exe", "d:/qq.rar"));
		}
		public static BPLoad GetInstance()
		{
			if (_inst == null)
			{
				GameObject goInstance = GameObject.Find(Const.STR_STATIC_INSTANCE_NAME);
				if (goInstance == null)
				{
					goInstance = new GameObject(Const.STR_STATIC_INSTANCE_NAME);
					UnityEngine.Object.DontDestroyOnLoad(goInstance);
				}
				_inst = goInstance.AddComponent<BPLoad>();
			}
			return _inst;
		}
		void OnApplicationQuit()
		{
			StopCoroutine("FPointDown");
		}
		public void ReqLoadFile(string url, string saveFile, CallbackDelegate _callBack)
		{
			MyDebug.Log("ReqLoadFile:url=" + url + ",saveFile=" + saveFile);
			callBack = _callBack;
			StartCoroutine(FPointDown(url, saveFile));
		}
		/// <summary>
		/// 断点下载,只有PC上能用，放android上抛出NotSupportException....
		/// </summary>
		/// <param name="url"></param>
		/// <param name="saveFile"></param>
		/// <returns></returns>
		IEnumerator FPointDown(string url, string saveFile)
		{
			MyDebug.Log("-1");
			System.Net.HttpWebRequest request = null;
			System.IO.FileStream fs = null;
			long contentLength = 0;
			//打开网络连接
			try
			{
				MyDebug.Log("0");
				request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url);
				MyDebug.Log("1");
				System.Net.HttpWebRequest requestGetCount = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url);
				MyDebug.Log("2");
				contentLength = requestGetCount.GetResponse().ContentLength;
				MyDebug.Log("3,len=" + contentLength);
				//打开上次下载的文件或新建文件
				long lStartPos = 0;
				if (System.IO.File.Exists(saveFile))
				{
					MyDebug.Log("4");
					fs = System.IO.File.OpenWrite(saveFile);
					lStartPos = fs.Length;
					MyDebug.Log("5,lStartPos=" + lStartPos);
					if (contentLength - lStartPos <= 0)
					{
						fs.Close();
						if (callBack != null)
						{
							callBack(saveFile, CALLBACK_TYPE.SUCCESS, contentLength, contentLength, 1);//下载完成
						}
						yield break;
					}
					fs.Seek(lStartPos, System.IO.SeekOrigin.Current); //移动文件流中的当前指针
				}
				else
				{
					MyDebug.Log("6");
					fs = new System.IO.FileStream(saveFile, System.IO.FileMode.Create);
				}
				if (lStartPos > 0)
				{
					MyDebug.Log("7");
					request.AddRange((int)lStartPos); //设置Range值
					MyDebug.Log("上次已经下载：" + lStartPos);
					//print(lStartPos);
				}
			}
			catch (Exception e)
			{
				MyDebug.LogError("8," + e.ToString());
				yield break;
			}
			//向服务器请求，获得服务器回应数据流
			System.IO.Stream ns = request.GetResponse().GetResponseStream();
			int len = 1024 * 128;
			byte[] nbytes = new byte[len];
			int nReadSize = 0;
			nReadSize = ns.Read(nbytes, 0, len);
			Debug.LogError("9,nReadSize=" + nReadSize);
			while (nReadSize > 0)
			{
				fs.Write(nbytes, 0, nReadSize);
				nReadSize = ns.Read(nbytes, 0, len);
				if (callBack != null)
				{
					callBack(url, CALLBACK_TYPE.PROGRESS, fs.Length, contentLength, (fs.Length * 0.001f) / (contentLength * 0.001f));//正在下载
				}
				//strInfo = "已下载" + fs.Length / 1024 + "kb /" + countLength / 1024 + "kb";
				yield return false;
			}
			ns.Close();
			fs.Close();
			Debug.LogError("10");
			if (callBack != null)
			{
				callBack(saveFile, CALLBACK_TYPE.SUCCESS, contentLength, contentLength, 1);//下载完成
			}
		}
		/// <summary>
		/// 普通下载
		/// </summary>
		/// <param name="url"></param>
		/// <param name="LocalPath"></param>
		/// <returns></returns>
		IEnumerator DownLoadFile(string url, string LocalPath)
		{
			Uri u = new Uri(url);
			HttpWebRequest mRequest = (HttpWebRequest)WebRequest.Create(u);
			mRequest.Method = "GET";
			mRequest.ContentType = "application/x-www-form-urlencoded";
			HttpWebResponse wr = (HttpWebResponse)mRequest.GetResponse();
			Stream sIn = wr.GetResponseStream();
			FileStream fs = new FileStream(LocalPath, FileMode.Create, FileAccess.Write);
			long length = wr.ContentLength;
			long i = 0;
			decimal j = 0;
			while (i < length)
			{
				byte[] buffer = new byte[1024];
				i += sIn.Read(buffer, 0, buffer.Length);
				fs.Write(buffer, 0, buffer.Length);
				if ((i % 1024) == 0)
				{
					j = Math.Round(Convert.ToDecimal((Convert.ToDouble(i) / Convert.ToDouble(length)) * 100), 4);
					Debug.Log("当前下载文件大小:" + length.ToString() + "字节   当前下载大小:" + i + "字节 下载进度" + j.ToString() + "%");
				}
				else
				{
					Debug.Log("当前下载文件大小:" + length.ToString() + "字节   当前下载大小:" + i + "字节");
				}
				yield return false;
			}
			sIn.Close();
			wr.Close();
			fs.Close();
		}
	}
}
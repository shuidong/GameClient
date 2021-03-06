using UnityEngine;
using System;
using System.IO;
using System.Collections;
using Core.Resource;
using System.Text;
using Core.Utils;
using CG3D.Model;
//using CG3D.UI.Common;
using CG3D.Config;

namespace CG3D.Utils
{
	public class Locale
	{
		protected static Hashtable mLocales = null;
		protected static string mCurrentLocale;
		//protected Hashtable 		mTextTable;
		private static bool mInited = false;
		/// <summary>
		/// 重试次数
		/// </summary>
		private static int reTryCount;
		public static bool Inited
		{
			get { return mInited; }
		}
		private static void AddLocale(string locale)
		{
			if (!ConfigManager.dictCfgBytes.ContainsKey("General.txt"))
			{
				MyDebug.LogError("Zip 未解析到：General.txt");
				return;
			}
			string str = Encoding.UTF8.GetString(ConfigManager.dictCfgBytes["General.txt"]);
			//Debug.Log("Locale parse1=" + Time.realtimeSinceStartup);
			Parse(str);
			//Debug.Log("Locale parse2=" + Time.realtimeSinceStartup);
			MyDebug.Log("本地化完成：" + locale);
		}
		private static void ReTrySetLocale()
		{
			SetLocale(mCurrentLocale);
		}
		private static void Parse(string str)
		{
			if (mLocales.Contains(mCurrentLocale))
			{
				return;
			}
			Hashtable hash = new Hashtable();
			string[] arrStr = str.Split('\n');//请勿使用StringReader，慢啊
			int count = arrStr.Length;
			string line;
			string key;
			string val;
			for (int i = 0; i < count; i++)
			{
				line = arrStr[i];
				if (line != null)
				{
					if (line.Length > 0 && line.IndexOf("=") != -1)
					{
						string[] broken = line.Split("="[0]);
						key = broken[0].Trim();
						val = broken[1].Trim();
						//Debug.Log ("key : " + key + " value : " + val);
						if (hash.Contains(key))
						{
							MyDebug.Log("本地化重复：" + key);
						}
						else
						{
							hash.Add(key, val);
						}
					}
				}
				else
				{
					break;
				}
			}
			mLocales.Add(mCurrentLocale, hash);
			mInited = true;
		}
		public static string GetLocale()
		{
			return mCurrentLocale;
		}

		public static void SetLocale(string locale)
		{
			mCurrentLocale = locale;
			if (null == mLocales)
			{
				mLocales = new Hashtable();
			}
			if (null == mLocales[locale])
			{
				AddLocale(locale);
			}
		}

		/// <summary>
		/// Fetches a localized string with the given key
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static string GetString(string key)
		{
			if (null == mLocales)
			{
				SetLocale("zh_CN");
			}
			if (null != mLocales[mCurrentLocale])
			{
				string ret = (string)(mLocales[mCurrentLocale] as Hashtable)[key];
				if (ret == null) { MyDebug.LogWarning("本地化失败：" + key); }
				return ret != null ? ret : "L:" + key;
			}
			return "L." + key;
		}

		/// <summary>
		/// Replaces {1}, {2}, {n} in the localized string with the given strings
		/// </summary>
		/// <param name="key"></param>
		/// <param name="replace"></param>
		/// <returns></returns>
		public static string GetString(string key, params string[] arrParams)
		{
			if (null == mLocales)
			{
				SetLocale("zh_CN");
			}
			string loc = GetString(key);
			for (int i = 0; i < arrParams.Length; i++)
			{
				loc = loc.Replace("{" + i + "}", arrParams[i]);
			}
			return loc;
		}
		public static string GetString(string key, string color, params string[] arrParams)
		{
			if (null == mLocales)
			{
				SetLocale("zh_CN");
			}
			string loc = GetString(key);
			color = color.Length >= 3 ? "[" + color + "]" : color;
			for (int i = 0; i < arrParams.Length; i++)
			{
				loc = loc.Replace("{" + i + "}", color + arrParams[i] + (color.Length >= 3 ? "[-]" : ""));
			}
			return loc;
		}
	}
}
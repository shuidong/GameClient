using Core.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CG3D.Config
{
	class ConfigManager
	{
		private static ConfigManager _inst;
		public static Dictionary<string, byte[]> dictCfgBytes = new Dictionary<string, byte[]>();
		/// <summary>
		/// 配置文件的版本
		/// </summary>
		public static int configVersion = 0;
		/// <summary>
		/// 需要初始化的配置数量
		/// </summary>
		private int totalCount = 0;
		/// <summary>
		/// 当前初始化好的配置数量
		/// </summary>
		internal int currCount = 0;

		public static ConfigManager GetInstance()
		{
			if (_inst == null)
			{
				_inst = new ConfigManager();
			}
			return _inst;
		}
		/// <summary>
		/// 是否全部初始化完成
		/// </summary>
		public bool hasInit
		{
			get { return totalCount > 0 && currCount >= totalCount; }
		}
		public IEnumerator InitCfg()
		{
			//Debug.Log("开始初始化配置:" + Time.time);
			currCount = -1;
			totalCount = 0;
            ShipCfg.GetInstance().Init("ship.txt", ref totalCount); yield return new WaitForSeconds(0.02f);
			
            //MyDebug.Log("初始化配置结束:" + Time.time);
			currCount += 1;
			yield break;
		}
	}
}

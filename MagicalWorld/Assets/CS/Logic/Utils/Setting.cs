using Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using CG3D.Utils;

namespace CG3D.Utils
{
	public class Setting
	{
		public static string resUrl = "";
		public static string UI_SHADER_GRAY = "";
		public static Color UI_GRAY_COLOR = Color.grey;
		public static string[] arrLocale = new string[2] { "zh_CN", "en_US" };
		/// <summary>
		/// 默认使用的语言
		/// </summary>
		public static MyEnum.LOCALE_TYPE localeType = MyEnum.LOCALE_TYPE.ZH_CN;
		/// <summary>
		/// 初始设置性能参数
		/// </summary>
		public static void InitPerformanceLevel()
		{
			int level = 0;//对应于Edit-Project Settings-Quality中的6个级别（0-5）
			if (Application.platform == RuntimePlatform.IPhonePlayer)
			{
				if (SystemInfo.processorCount == 1) { level = 0; }
				else { level = 2; }
			}
			else if (Application.platform == RuntimePlatform.WindowsPlayer
				|| Application.platform == RuntimePlatform.WindowsEditor) { level = 5; }
			else if (SystemInfo.processorCount <= 2) { level = 1; }
			else if (SystemInfo.processorCount > 2) { level = 2; }
#if UNITY_IPHONE
			iPhoneGeneration gen = iPhone.generation;
			if (gen == iPhoneGeneration.iPhone4) level = 0;
#endif
			//level = 1;
			QualitySettings.SetQualityLevel(level);
			switch (level)
			{
				case 0:
					QualitySettings.masterTextureLimit = 1;
					break;
				case 1:
					QualitySettings.masterTextureLimit = 1;
					break;
				case 2:
				case 3:
				case 4:
				case 5:
					UI_SHADER_GRAY = "(TrueGray)";
					UI_GRAY_COLOR = Color.black;
					break;
			}
			MyDebug.Log("质量级别：" + level + ",gray=" + UI_SHADER_GRAY);
		}
	}
}

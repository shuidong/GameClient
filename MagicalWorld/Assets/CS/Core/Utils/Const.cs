using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
    public class Const
	{
		/// <summary>
		/// 是否单机
		/// </summary>
		public static bool isSOLO = false;
		/// <summary>
		/// 客户端版本，前两位作为主版本号，主版本号不同则需要强制更新客户端
		/// AppStore版本则总的只有三位，去掉4位版本号的第3位
		/// 默认第三位都为0
		/// </summary>
		public static string CLIENT_VERSION = "1.1.6";
		/// <summary>
		/// 植入资源的版本，为植入时资源服务器上的版本号最大值
		/// </summary>
		public static int EMBED_RES_VERSION = 1008;

		public const int LAYER_UI = 14;
		public const int LAYER_MAP = 15;
		public const int LAYER_EFFECT = 16;
		public const int LAYER_ACTOR = 17;
        /// <summary>
        /// 单例
        /// </summary>
		public static string STR_STATIC_INSTANCE_NAME = "_StaticInstance";
    }
	public enum LOGIN_ERROR
	{
		SUCCESS = 0,
		USERNAME_IS_NULL = 1,       	// 用户名为空
		PASSWORD_IS_NULL = 2,       	// 密码为空
		USERNAME_IS_USED = 3,       	// 用户名已经存在
		USERNAMEORPASSWORD_WRONG = 4,   // 用户名或密码错误
		MAINTANCESERVER = 5,       	// 停服维护中
		FAILED_HOST = 99,				// 主机访问失败，客户端加的
		UNKNOWN = 100,				// 未知错误
	}
	public enum SERVER_STATUS
	{
		NORMAL = 0,                    // 服务器处于正常状态
		STOP = 1,                    // 服务器已经停服
		EXCEPTION = 2,                  // 服务器异常
	}

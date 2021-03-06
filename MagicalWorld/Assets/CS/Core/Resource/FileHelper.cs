using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;
namespace Core.Resource
{
	/// <summary>
	/// 使用Application.persistentDataPath方式来创建文件，读写Xml文件.
	/// 注Application.persistentDataPath末尾没有“/”符号
	/// </summary>
	public class FileHelper
	{
		/// <summary>
		/// 读取文件.
		/// </summary>
		/// <returns>The file.</returns>
		/// <param name="path">完整文件路径.</param>
		public static byte[] LoadFile(string path)
		{
			return File.ReadAllBytes(Application.persistentDataPath + "/" + path);
		}
		public static void SaveFile(string path, byte[] info)
		{
			path = Application.persistentDataPath + "/" + path;
			//文件流信息
			FileInfo t = new FileInfo(path);
			DeleteFile(path);
			Stream sw = t.Create();
			sw.Write(info, 0, info.Length);
			sw.Close();
			sw.Dispose();
		}
		//读取本地AssetBundle文件
		IEnumerator LoadAssetbundleFromLocal(string path)
		{
			WWW w = new WWW("file:///" + path);
			yield return w;
			//TODO:将w.bytes返回
		}

		public static void DeleteFile(string path)
		{
			if(File.Exists(path))
				File.Delete(path);
		}
		/// <summary>
		/// 获取文件下所有文件大小
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public static int GetAllFileSize(string path)
		{
			int sum = 0;
			if (!Directory.Exists(path))
			{
				return 0;
			}
			DirectoryInfo dti = new DirectoryInfo(path);
			FileInfo[] fi = dti.GetFiles();
			foreach (FileInfo f in fi)
			{
				sum += Convert.ToInt32(f.Length / 1024);
			}
			DirectoryInfo[] di = dti.GetDirectories();
			if (di.Length > 0)
			{
				for (int i = 0; i < di.Length; i++)
				{
					sum += GetAllFileSize(di[i].FullName);
				}
			}
			return sum;
		}
		/// <summary>
		/// 获取指定文件大小
		/// </summary>
		/// <param name="FilePath"></param>
		/// <param name="FileName"></param>
		/// <returns></returns>
		public static int GetFileSize(string path)
		{
			int sum = 0;
			if (!Directory.Exists(path))
			{
				return 0;
			}
			else
			{
				FileInfo Files = new FileInfo(@path);
				sum += Convert.ToInt32(Files.Length / 1024);
			}
			return sum;
		}
		/// <summary>
		/// 动态创建文件夹.
		/// </summary>
		/// <returns>The folder.</returns>
		/// <param name="path">文件创建目录.</param>
		/// <param name="FolderName">文件夹名(不带符号).</param>
		public static string CreateFolder(string path, string FolderName)
		{
			string FolderPath = path + FolderName;
			if (!Directory.Exists(FolderPath))
			{
				Directory.CreateDirectory(FolderPath);
			}
			return FolderPath;
		}
		/// <summary>
		/// 创建文件.
		/// </summary>
		/// <param name="path">完整文件夹路径.</param>
		/// <param name="name">文件的名称.</param>
		/// <param name="info">写入的内容.</param>
		public static void CreateFile(string path, string info)
		{
			StreamWriter sw;
			FileInfo t = new FileInfo(path);
			if (!t.Exists)
			{
				sw = t.CreateText();
			}
			else
			{
				sw = t.AppendText();
			}
			sw.WriteLine(info);
			sw.Close();
			sw.Dispose();
		}
	}
}
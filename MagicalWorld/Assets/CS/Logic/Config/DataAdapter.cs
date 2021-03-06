using Core.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CG3D.Config
{
	/// <summary>
	/// 配置文件数据适配器
	/// </summary>
	public class DataAdapter
	{
		public string fileName;
		public Dictionary<string, int> dictHead;
		public string[] arrStrData;

		public string stringOf(string key)
		{
			if (!dictHead.ContainsKey(key))
			{
				throw new Exception(fileName + "未找到配置数据键：" + key);
			}
			int index = dictHead[key];
			if (index < 0 || index >= arrStrData.Length)
			{
				throw new Exception(fileName + "数据键索引超出范围：" + index);
			}
			return arrStrData[index];
		}
		public int intOf(string key)
		{
			string v = stringOf(key);
			try
			{
				return int.Parse(v);
			}
			catch
			{
				MyDebug.LogError("数据不是int：file=" + fileName + ",key=" + key + ",value=" + v);
			}
			return 0;
		}
		public float floatOf(string key)
		{
			string v = stringOf(key);
			return float.Parse(v);
		}
		public Vector3 vector3Of(string key)
		{
			string v = stringOf(key);
			string[] arr = v.Split("|"[0]);
			if (arr.Length < 3)
			{
				throw new Exception(fileName + "," + v + " 长度小于3，不能到vector3的转换");
			}
			return new Vector3(float.Parse(arr[0]), float.Parse(arr[1]), float.Parse(arr[2]));
		}
		public Vector3 vector2Of(string key)
		{
			string v = stringOf(key);
			string[] arr = v.Split("|"[0]);
			if (arr.Length < 2)
			{
				throw new Exception(fileName + "," + v + " 长度小于2，不能到vector2的转换");
			}
			return new Vector2(float.Parse(arr[0]), float.Parse(arr[1]));
		}
		public int[] intArrayOf(string key)
		{
			string v = stringOf(key);
			string[] arr = v.Split("|"[0]);
			int[] arrInt = new int[arr.Length];
			int count = 0;
			foreach (string s in arr)
			{
				arrInt[count] = int.Parse(s);
				count++;
			}
			return arrInt;
		}
		public string[] stringArrayOf(string key)
		{
			string v = stringOf(key);
			return v.Split("|"[0]);
		}
		public float[] floatArrayOf(string key)
		{
			string v = stringOf(key);
			string[] arr = v.Split("|"[0]);
			float[] arrFloat = new float[arr.Length];
			int count = 0;
			foreach (string s in arr)
			{
				arrFloat[count] = float.Parse(s);
				count++;
			}
			return arrFloat;
		}
		public Color colorOf(string key)
		{
			int[] arrInt = intArrayOf(key);
			if (arrInt.Length < 3)
			{
				MyDebug.Log(fileName + "在列：" + key + ",不能转换成Color");
			}
			else if (arrInt.Length == 3)
			{
				return new Color32((byte)arrInt[0], (byte)arrInt[1], (byte)arrInt[2], 255);
			}
			else
			{
				return new Color32((byte)arrInt[0], (byte)arrInt[1], (byte)arrInt[2], (byte)arrInt[3]);
			}
			return Color.black;
		}
		public int randomIntOf(string key)
		{
			string v = stringOf(key);
			string[] arr = v.Split("|"[0]);
			return UnityEngine.Random.Range(int.Parse(arr[0]), int.Parse(arr[1]) + 1);
		}
		public override bool Equals(object obj)
		{
			if(obj==null)return false;
			DataAdapter da = obj as DataAdapter;
			return (dictHead == da.dictHead && fileName == da.fileName && arrStrData[0] == da.arrStrData[0]);
		}
		public static bool operator ==(DataAdapter a, DataAdapter b)
		{
			if (System.Object.ReferenceEquals(a, b)) return true;
			if (((object)a == null) && ((object)b != null)) return false;
			if (((object)a != null) && ((object)b == null)) return false;
			if (((object)a == null) && ((object)b == null)) return true;

			return a.Equals(b);
		}

		public static bool operator !=(DataAdapter a, DataAdapter b)
		{
			return !(a == b);
		}
		public override int GetHashCode()
		{
			int result = 17;
			result = 31 * result + dictHead.Count;
			result = 31 * result + fileName.Length;
			result = 31 * result + arrStrData[0].Length;
			return result;
		}
	}
}

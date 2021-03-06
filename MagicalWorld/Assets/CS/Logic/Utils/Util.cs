using CG3D.Config;
using CG3D.Model;
using Core.Utils;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using System.Security.Cryptography;
    public class Util
    {

        public static int IPToInteger(int A, int B, int C, int D)
        {
            return (D << 24) + (C << 16) + (B << 8) + A;
        }
        public static string GetBytesString(byte[] data)
        {
            return GetBytesString(data, 0, 0);
        }
        public static string GetBytesString(byte[] data, int startIndex, int len)
        {
            len = len == 0 ? data.Length : len;
            string str = "";
            for (int i = startIndex; i < len; i++)
            {
                str += data[i].ToString("X") + ",";
            }
            return str;
        }
        /// <summary>
        /// 结构体转byte数组
        /// </summary>
        /// <param name="structObj">要转换的结构体</param>
        /// <returns>转换后的byte数组</returns>
        public static byte[] StructToBytes(object structObj)
        {
            //得到结构体的大小
            int size = Marshal.SizeOf(structObj);
            //创建byte数组
            byte[] bytes = new byte[size];
            //分配结构体大小的内存空间
            IntPtr structPtr = Marshal.AllocHGlobal(size);
            //将结构体拷到分配好的内存空间
            Marshal.StructureToPtr(structObj, structPtr, false);
            //从内存空间拷到byte数组
            Marshal.Copy(structPtr, bytes, 0, size);
            //释放内存空间
            Marshal.FreeHGlobal(structPtr);
            //返回byte数组
            return bytes;
        }
        ////// <summary>
        ///// byte数组转结构体
        ///// </summary>
        ///// <param name="bytes">byte数组</param>
        ///// <param name="type">结构体类型</param>
        ///// <returns>转换后的结构体</returns>
        //public static object[] BytesToStuct(MessageEx msg, Type type, int count)
        //{
        //    object[] arr = new object[count];
        //    //得到结构体的大小
        //    int size = Marshal.SizeOf(type);
        //    byte[] bytes = new byte[size * count];
        //    msg.Read(ref bytes);
        //    for (int i = 0; i < count; i++)
        //    {
        //        //分配结构体大小的内存空间
        //        IntPtr structPtr = Marshal.AllocHGlobal(size);
        //        //将byte数组拷到分配好的内存空间
        //        Marshal.Copy(bytes, i * size, structPtr, size);
        //        //将内存空间转换为目标结构体
        //        object obj = Marshal.PtrToStructure(structPtr, type);
        //        //释放内存空间
        //        Marshal.FreeHGlobal(structPtr);
        //        arr[i] = obj;
        //    }
        //    //返回结构体
        //    return arr;
        //}
        /// <summary>
        /// 将时间转换为中文计时（如：3天05时28分09秒）
        /// </summary>
        /// <param name="time">秒</param>
        /// <returns></returns>
        public static string ConvertTime(int time)
        {
            if (time < 0)
            {
                return "00分00秒";
            }
            string str = "";
            int temp;
            if (time >= 86400)//60*60*24
            {
                temp = time / 86400;
                time = time % 86400;
                str = temp + "天";
            }
            if (time >= 3600)
            {
                temp = time / 3600;
                str += temp < 10 ? "0" + temp + "时" : temp + "时";
                time = time % 3600;
            }
            temp = time / 60;
            str += temp < 10 ? "0" + temp + "分" : temp + "分";
            time = time % 60;
            str += time < 10 ? "0" + time + "秒" : time + "秒";
            return str;
        }

        /// <summary>
        /// 将时间转换为中文计时（如：3天05时28分09秒）
        /// </summary>
        /// <param name="time">秒</param>
        /// <returns></returns>
        public static string ConvertDayTime(int time)
        {
            if (time < 0)
            {
                return "00分00秒";
            }
            string str = "";
            int temp;
            if (time >= 86400)//60*60*24
            {
                temp = time / 86400;
                time = time % 86400;
                str = temp + "天";
            }
            return str;
        }
        public static string ConvertDate(int time)
        {
            DateTime d = DateTime.Parse("01/01/1970");
            d = d.AddSeconds(time);
            return d.Year + "年" + d.Month + "月" + d.Day + "日" + d.Hour + "时" + d.Minute + "分" + d.Second + "秒";
        }
        public static string ConvertDateDay(int time)
        {
            DateTime d = DateTime.Parse("01/01/1970");
            d = d.AddSeconds(time);
            return d.Year + "年" + d.Month + "月" + d.Day + "日";
        }

        public static string ConvertDateNumDay(int time)
        {
            DateTime d = DateTime.Parse("01/01/1970");
            d = d.AddSeconds(time);
            return d.Year + "." + d.Month + "." + d.Day + "";
        }

        public static string ConvertNumDateDay(int time)
        {
            DateTime d = DateTime.Parse("01/01/1970");
            d = d.AddSeconds(time);
            return d.Year + "." + d.Month + "." + d.Day;
        }

        /// <summary>
        /// 将时间转换为数字　计时（如：233:05:28)，不显示秒
        /// </summary>
        /// <param name="time">秒</param>
        /// <returns></returns>
        public static string ConvertNumTime(int time)
        {
            if (time < 0)
            {
                return "00:00";
            }
            string str = "";
            int temp;
            if (time >= 86400)//60*60*24
            {
                temp = time / 86400;
                time = time % 86400;
                str = temp + ":";
            }
            if (time >= 3600)
            {
                temp = time / 3600;
                str += temp < 10 ? "0" + temp + ":" : temp + ":";
                time = time % 3600;
            }
            else
            {
                str += "00:";
            }

            temp = time / 60;
            //if (time < 3600)
            //{
            //    str += temp < 10 ? "0" + temp + ":" : temp + ":";
            //    time = time % 60;
            //    str += time < 10 ? "0" + time  : time + "";
            //}
            //else
            //{
            str += temp < 10 ? "0" + temp : temp + "";
            //}
            return str;
        }
        /// <summary>
        /// 转换活动用的时间
        /// </summary>
        /// <param name="time">1970开始的时间</param>
        /// <returns>HH:MM</returns>
        public static string ConvertActionTime(int time)
        {
            if (time <= 0)
            {
                return "00:00";
            }
            string str = "";
            int temp;
            if (time >= 86400)//60*60*24
            {
                time = time % 86400;
            }
            if (time >= 3600)
            {
                temp = time / 3600;
                str += temp < 10 ? "0" + temp + ":" : temp + ":";
                time = time % 3600;
            }
            else
            {
                str = "00:";
            }

            temp = time / 60;
            str += temp < 10 ? "0" + temp : temp + "";
            return str;
        }

        /// <summary>
        /// 将时间转换为数字　计时（如：233:05:28:09),显示秒
        /// </summary>
        /// <param name="time">秒</param>
        /// <returns></returns>
        public static string ConvertAllNumTime(int time)
        {
            if (time < 0)
            {
                return "00:00:00";
            }
            string str = "";
            int temp;
            if (time >= 86400)//60*60*24
            {
                temp = time / 86400;
                time = time % 86400;
                str = temp + ":";
            }
            if (time >= 3600)
            {
                temp = time / 3600;
                str += temp < 10 ? "0" + temp + ":" : temp + ":";
                time = time % 3600;
            }
            else
            {
                str += "00:";
            }
            temp = time / 60;
            str += temp < 10 ? "0" + temp + ":" : temp + ":";
            time = time % 60;
            str += time < 10 ? "0" + time + "" : time + "";
            return str;
        }

        public static int GetConvertHour(int time)
        {
            return time / 3600;
        }

        public static string ConvertString(string str, params string[] arrParams)
        {
            for (int i = 0; i < arrParams.Length; i++)
            {
                str = str.Replace("{" + i + "}", arrParams[i]);
            }
            return str;
        }
        public static List<Transform> GetMultiChildTransformByName(Transform parent, string name)
        {
            //Debug.Log("role name=" + parent.name + ",name=" + name);
            Component[] componentArray = parent.GetComponentsInChildren(typeof(Transform));
            List<Transform> list = new List<Transform>();
            string[] arrName = name.Split("|"[0]);
            bool flag;
            foreach (Transform t in componentArray)
            {
                //Debug.Log("t name=" + t.name);
                flag = false;
                foreach (string strName in arrName)
                {
                    if (strName == t.name)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    //Debug.Log("----add t name=" + t.name);
                    list.Add(t);
                }
            }
            return list;
        }
        /// <summary>
        /// 将UI的坐标转换为特效摄像机使用的坐标
        /// </summary>
        /// <returns></returns>
        //public static Vector3 UITransform2EffectPos(Transform uiTrans)
        //{
        //    Vector3 pos = uiTrans.localPosition;
        //    while ((uiTrans = uiTrans.parent) != null)
        //    {
        //        if (uiTrans.GetComponent<UICamera>() != null) break;
        //        pos += uiTrans.localPosition;
        //    }
        //    float height = Screen.height * 0.5f;
        //    return pos / height / UIRoot.adjustment;
        //}

        public static string GetActionTime(string cfgTime)
        {
            string[] arr = cfgTime.Split("|"[0]);
            return arr[0] + ":" + arr[1];
        }

        public static string GetActionYearMonthTime(string cfgTime)
        {
            string[] arr = cfgTime.Split("|"[0]);
            return arr[0] + "." + arr[1] + "." + arr[2];
        }
        /// <summary>
        /// 从Resources里获取角色资源
        /// </summary>
        /// <param name="resName"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public static GameObject GetResActor(string resName, int version)
        {
            if (version > Const.EMBED_RES_VERSION)
            {
                MyDebug.Log("从WWW加载：" + resName + ",v=" + version);
                return null;
            }
            MyDebug.Log("从Resources加载：" + resName + ",v=" + version);
            resName = resName.Replace(".upg", "");
            UnityEngine.Object o = Resources.Load("Actor/" + resName);
            if (o == null) return null;
            GameObject go = (GameObject)GameObject.Instantiate(o);
            return go;
        }
        /// <summary>
        /// 向指定对象添加自节点
        /// </summary>
        /// <param name="parent">父节点</param>
        /// <param name="prefab">子对象模板</param>
        ///<param name="offsetPos">位置偏移</param>
        /// <returns></returns>
        static public GameObject AddChild(GameObject parent, GameObject prefab)
        {
            GameObject go = GameObject.Instantiate(prefab) as GameObject;

            if (go != null && parent != null)
            {
                Transform t = go.transform;
                t.parent = parent.transform;
                t.localPosition = Vector3.zero;
                t.localRotation = Quaternion.identity;
                t.localScale = Vector3.one;
            }
            return go;
        }
        /// <summary>
        /// 向指定对象添加子节点
        /// </summary>
        /// <param name="parent">父节点</param>
        /// <param name="prefab">子对象模板</param>
        ///<param name="active">是否启用</param>
        /// <returns></returns>
        static public T AddChild<T>(GameObject parent, GameObject prefab, bool active = true) where T : Component
        {
            GameObject go = GameObject.Instantiate(prefab) as GameObject;
            T com = null;
            if (go != null && parent != null)
            {
                com = go.GetComponent<T>();
                if (com == null)
                {
                    com = go.AddComponent<T>();
                }
                Transform t = go.transform;
                t.SetParent(parent.transform,false);
                go.SetActive(active);
            }
            return com;
        }
        /// <summary>
        /// 加载Sprite
        /// </summary>
        /// <param name="spriteName"></param>
        /// <returns></returns>
        private Sprite LoadSprite(string spriteName)
        {
            GameObject go = Resources.Load<GameObject>("Sprite/" + spriteName);
            Sprite sprite = null;
            if(go != null)
            {
                sprite = go.GetComponent<SpriteRenderer>().sprite;
            }
            else
            {
                MyDebug.LogError("spriteName:" + spriteName + "加载失败!");
            }
            return sprite;
        }
    }

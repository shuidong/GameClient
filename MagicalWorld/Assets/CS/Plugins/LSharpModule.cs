using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;
using CLRSharp;
//L#模块工具
public class LSharpModule:MonoBehaviour
{
    public delegate void Call();
    public static string dll_str = ".dll",pdb_str = ".pdb";
    public static CLRSharp_Environment env;
    ThreadContext context;
    string path = "http://localhost/asset/UIDll/";
    string dll_suffix = ".dll.bytes", pdb_suffix = ".pdb.bytes";
    void Awake()
    {
        //創建CLRSharp環境
        if (env == null)
        {
            env = new CLRSharp_Environment(new Logger());
        }
        //1、建立一个线程上下文，用来模拟L#线程模型
        context = new ThreadContext(env);
        Debug.Log("Create ThreadContext for L#.");
        StartCoroutine(WWWLoad("GameUI"));

    }
    public static void To(Call call)
    {
        call();
    }
    IEnumerator WWWLoad(string name)
    {
        WWW www = new WWW(path + name + dll_suffix);
        yield return www;
        if (www.isDone)
        {
            System.IO.MemoryStream msDll = new System.IO.MemoryStream(www.bytes);
            www = new WWW(path + name + pdb_suffix);
            yield return www;
            if (www.isDone)
            {
                System.IO.MemoryStream msPdb = new System.IO.MemoryStream(www.bytes);
                env.LoadModule(msDll, msPdb, new Mono.Cecil.Pdb.PdbReaderProvider());
                Debug.Log("LoadModule MyHelloWorld.dll is done.");
                MainUIThread.GetInstance();
                Application.LoadLevel("Hall");
            }
        }

    }
}
//實現L#的LOG接口
public class Logger : ICLRSharp_Logger
{
    public void Log(string str)
    {
        Debug.Log(str);
    }
    public void Log_Error(string str)
    {

        Debug.LogError(str);

    }
    public void Log_Warning(string str)
    {

        Debug.LogWarning(str);

    }
}

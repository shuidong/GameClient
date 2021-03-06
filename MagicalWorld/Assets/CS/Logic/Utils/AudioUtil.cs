using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CG3D.Utils
{
	public class AudioUtil
	{
		public const int LAYER_BACKGROUD = 1;
		public const int LAYER_SKILL = 10;
		public const int LAYER_NPC = 11;
		public const int LAYER_FIGHT_RESULT = 12;
		public const int LAYER_UI = 13;

		private static Dictionary<int, AudioSource> dictAudioSource = new Dictionary<int,AudioSource>();
		private static Dictionary<string, AudioClip> dictAudioClip = new Dictionary<string, AudioClip>();
		private static Transform transAudioRoot;

		public static void Play(int layer, string audioName, bool loop)
		{
			if (string.IsNullOrEmpty(audioName) || audioName == "0")
			{
				return;
			}
			//UnityEngine.Debug.Log("play audio:" + audioName);
			if (!dictAudioClip.ContainsKey(audioName))
			{
				dictAudioClip[audioName] = (AudioClip)Resources.Load("Audio/" + audioName);
			}
			if (!dictAudioSource.ContainsKey(layer))
			{
				GameObject go = new GameObject("AudioSource_" + layer);
				GameObject.DontDestroyOnLoad(go);
				dictAudioSource[layer] = go.AddComponent<AudioSource>();
				if (transAudioRoot == null)
				{
					GameObject goRoot = new GameObject("AudioRoot");
					GameObject.DontDestroyOnLoad(goRoot);
					transAudioRoot = goRoot.transform;
				}
				go.transform.parent = transAudioRoot;
			}
			if (dictAudioSource[layer].clip == dictAudioClip[audioName] && dictAudioSource[layer].isPlaying)
			{
				dictAudioSource[layer].time = 0;
				dictAudioSource[layer].volume = 1;
				return;
			}
			dictAudioSource[layer].gameObject.transform.position = Camera.main.transform.position;
			dictAudioSource[layer].Stop();
			dictAudioSource[layer].clip = dictAudioClip[audioName];
			dictAudioSource[layer].loop = loop;
			dictAudioSource[layer].volume = 1;
			dictAudioSource[layer].Play();
		}
		public static GameObject GetLayerGameObject(int layer)
		{
			if (dictAudioSource.ContainsKey(layer))
			{
				return dictAudioSource[layer].gameObject;
			}
			return null;
		}
		public static void SetVolume(int layer, float value)
		{
			if (isAudioOff) return;
			if (dictAudioSource.ContainsKey(layer))
			{
				dictAudioSource[layer].volume = value;
			}
		}
		public static bool IsPlay(string audioName)
		{
			foreach (AudioSource audioSource in dictAudioSource.Values)
			{
				if (audioSource.isPlaying && dictAudioClip.ContainsKey(audioName) && dictAudioClip[audioName] == audioSource.clip)
				{
					return true;
				}
			}
			return false;
		}
		public static void StopAll()
		{
			foreach (AudioSource audioSource in dictAudioSource.Values)
			{
				audioSource.Stop();
			}
		}
		public static void Stop(int layer)
		{
			if (dictAudioSource.ContainsKey(layer) && dictAudioSource[layer] != null)
			{
				dictAudioSource[layer].Stop();
			}
		}
		public static void Stop(int layer, string audioName)
		{
			if (!dictAudioSource.ContainsKey(layer))
			{
				return;
			}
			if (dictAudioSource[layer] != null && dictAudioSource[layer].clip == dictAudioClip[audioName])
			{
				dictAudioSource[layer].Stop();
			}
		}
		private static bool _isAudioOff = false;
		/// <summary>
		/// 是否关闭所有声音
		/// </summary>
		public static bool isAudioOff
		{
			get { return _isAudioOff; }
			set
			{
				_isAudioOff = value;
				AudioListener.volume = value ? 0 : 1;
				PlayerPrefs.SetInt("isAudioOff", value ? 1 : 0);
				PlayerPrefs.Save();
			}
		}
	}
}

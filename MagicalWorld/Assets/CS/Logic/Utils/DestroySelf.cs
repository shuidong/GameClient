using CG3D.Config;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace CG3D.Utils
{
	public class DestroySelf : MonoBehaviour
	{
		public float delta = 9;
		void Update()
		{
			delta -= Time.deltaTime;
			if (delta <= 0)
			{
				GameObject.DestroyImmediate(gameObject);
			}
		}
	}
}

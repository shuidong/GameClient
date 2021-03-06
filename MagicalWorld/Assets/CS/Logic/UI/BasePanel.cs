using Core.I;
using Core.Utils;
using UnityEngine;
	public class BasePanel : MonoBehaviour, IObserver 
	{
		public static Transform uiCenterRoot;
		public static Transform uiBottomRoot;

		protected bool isOpen;
		protected bool isRealOpen;
		protected TweenScale tweenScale;
		/// <summary>
		/// 记录需要返回的窗口
		/// </summary>
		public static BasePanel backPanel;
		public static int openPanelCount = 0;
		public virtual void Start()
		{
			isOpen = false;
			isRealOpen = false;		
			InitTweenScale();
		}
		private void OnNextFrame()
		{
			if (!isRealOpen)
			{
				isRealOpen = true;
				RealOpen();
			}
		}
		protected virtual void InitTweenScale()
		{
			tweenScale = GetComponent<TweenScale>();
			if (tweenScale == null)
			{
				tweenScale = gameObject.AddComponent<TweenScale>();
			}
			tweenScale.from = new Vector3(0.2f, 0.2f, 0.2f);
			tweenScale.to = Vector3.one;
			tweenScale.eventReceiver = gameObject;
			tweenScale.duration = 0.2f;
			tweenScale.callWhenFinished = "OnTweenScaleEnd";
		}
		private void OnTweenScaleEnd()
		{
			if (isOpen)
			{
				TimerUtil.SetTimeOut(0.1f, new TimerUtil.Callback(OnNextFrame));
			}
			else
			{
				RealClose();
			}
		}
		public virtual void Open()
		{
			if (isOpen)
			{
				return;
			}
			isOpen = true;
			gameObject.SetActive(true);
			if (tweenScale != null)
			{
				//gameObject.transform.localScale = Vector3.one * 0.001f;//如果去掉,ngui在DrawCall重绘时，检测到缩放是0就不会生成mesh
				tweenScale.Toggle();
			}
			else
			{
				isRealOpen = true;
				RealOpen();
			}

			RecordOpen();
			openPanelCount++;
		}
		public virtual void Home()
		{
			backPanel = null;
			Close();
		}
		public virtual void Close()
		{
			if (!isOpen)
			{
				return;
			}
			isOpen = false;
			isRealOpen = false;
			if (tweenScale != null)
			{
				tweenScale.Toggle();
			}
			else
			{
				RealClose();
			}

			RecordClose();
			openPanelCount--;
		}
		protected virtual void RealOpen()
		{
		}
		
		protected virtual void RealClose()
		{
			gameObject.SetActive(false);
		}
		public virtual void Notifyed()
		{

		}
		public static void OpenBackPanel()
		{
			if (backPanel != null)
			{
				backPanel.Open();
				backPanel = null;
			}
		}
		public bool GetIsOpen()
		{
			return isRealOpen;
		}
		public virtual void OnDestroy()
		{

		}
		public virtual void RecordClose()
		{
            PanelUtil.RecordClosePanel(this);
        }
		public virtual void RecordOpen()
		{
            PanelUtil.RecordOpenPanel(this);
        }
	}

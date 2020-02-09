using UnityEngine;
using UnityEngine.Events;

using System.Collections.Generic;

namespace Scripts.Framework.EventSystem
{
	public abstract class EventListener<T> : MonoBehaviour
	{
		protected Event<T>[] events;
		protected UnityEvent<T> response;

		protected abstract void LinkEvents();
		protected abstract void LinkResponse();

		public void Subscribe(UnityAction<T> method)
		{
			response.AddListener(method);
		}

		public void Unsubscribe(UnityAction<T> method)
		{
			response.RemoveListener(method);
		}

		public void UnsubscribeAll()
		{
			response.RemoveAllListeners();
		}

		public void OnInvoke(T param)
		{
			response.Invoke(param);
		}

		public void Enable()
		{
			foreach (var ev in events)
			{
				ev.Subscribe(this);
			}
		}

		public void Disable()
		{
			foreach (var ev in events)
			{
				ev.Unsubscribe(this);
			}
		}

		private void Awake()
		{
			LinkEvents();
			LinkResponse();
		}

		private void OnEnable()
		{
			Enable();
		}

		private void OnDisable()
		{
			Disable();
		}
	}
}

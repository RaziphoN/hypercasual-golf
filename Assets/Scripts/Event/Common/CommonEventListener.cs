using UnityEngine;
using UnityEngine.Events;

using System.Collections.Generic;

namespace Scripts.Framework.EventSystem.Common
{
	public class EventListener : MonoBehaviour
	{
		protected Event[] events;
		protected UnityEvent response;

		public void Subscribe(UnityAction method)
		{
			response.AddListener(method);
		}

		public void OnInvoke()
		{
			response.Invoke();
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

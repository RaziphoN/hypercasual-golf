using UnityEngine;

using System.Collections.Generic;

namespace Scripts.Framework.EventSystem.Common
{
	[CreateAssetMenu(fileName = "CommonEvent", menuName = "Scriptable/EventSystem/CommonEvent")]
	public class Event : EventBase
	{
		private readonly List<EventListener> listeners = new List<EventListener>();

		public override void Invoke()
		{
			for (int i = listeners.Count - 1; i >= 0; i--)
				listeners[i].OnInvoke();
		}

		public void Subscribe(EventListener listener)
		{
			if (!listeners.Contains(listener))
				listeners.Add(listener);
		}

		public void Unsubscribe(EventListener listener)
		{
			if (listeners.Contains(listener))
				listeners.Remove(listener);
		}
	}
}

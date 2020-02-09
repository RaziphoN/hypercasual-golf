using System.Collections.Generic;

namespace Scripts.Framework.EventSystem
{
	public class Event<T> : EventBase
	{
		private readonly List<EventListener<T>> listeners = new List<EventListener<T>>();
		public T param;

		public override void Invoke()
		{
			for (int i = listeners.Count - 1; i >= 0; i--)
				listeners[i].OnInvoke(param);
		}

		public void Subscribe(EventListener<T> listener)
		{
			if (!listeners.Contains(listener))
				listeners.Add(listener);
		}

		public void Unsubscribe(EventListener<T> listener)
		{
			if (listeners.Contains(listener))
				listeners.Remove(listener);
		}
	}
}

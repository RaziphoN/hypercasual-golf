using UnityEngine;
using UnityEngine.Events;

using System.Collections.Generic;

namespace Scripts.Framework.EventSystem.Common
{
	[System.Serializable]
	public class IntUnityEvent : UnityEvent<int> { }

	public class IntEventListener : EventListener<int>
	{
		[Tooltip("Event to register with.")]
		[SerializeField] private new IntEvent[] events = null;

		[Tooltip("Response to invoke when Event is raised.")]
		[SerializeField] private new IntUnityEvent response = null;

		protected override void LinkEvents()
		{
			base.events = this.events;
		}

		protected override void LinkResponse()
		{
			base.response = this.response;
		}
	}
}

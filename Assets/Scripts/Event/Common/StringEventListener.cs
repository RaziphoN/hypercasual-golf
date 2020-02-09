using UnityEngine;
using UnityEngine.Events;

using System.Collections.Generic;
using System;

namespace Scripts.Framework.EventSystem.Common
{
	[System.Serializable]
	public class StringUnityEvent : UnityEvent<string> { }

	public class StringEventListener : EventListener<string>
	{
		[Tooltip("Event to register with.")]
		[SerializeField] private new StringEvent[] events = null;

		[Tooltip("Response to invoke when Event is raised.")]
		[SerializeField] public new StringUnityEvent response = null;

		protected override void LinkEvents()
		{
			base.events = events;
		}

		protected override void LinkResponse()
		{
			base.response = response;
		}
	}
}

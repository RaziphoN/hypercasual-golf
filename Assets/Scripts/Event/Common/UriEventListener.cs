using UnityEngine;
using UnityEngine.Events;

using System.Collections.Generic;
using System;

namespace Scripts.Framework.EventSystem.Common
{
	[System.Serializable]
	public class UriUnityEvent : UnityEvent<System.Uri> { }

	public class UriEventListener : EventListener<System.Uri>
	{
		[Tooltip("Event to register with.")]
		[SerializeField] private new UriEvent[] events = null;

		[Tooltip("Response to invoke when Event is raised.")]
		[SerializeField] private new UriUnityEvent response = null;

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

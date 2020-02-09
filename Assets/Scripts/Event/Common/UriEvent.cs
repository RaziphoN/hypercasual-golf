using UnityEngine;

namespace Scripts.Framework.EventSystem.Common
{
	[CreateAssetMenu(fileName = "UriEvent", menuName = "Scriptable/EventSystem/UriEvent")]
	public class UriEvent : Event<System.Uri> { }
}

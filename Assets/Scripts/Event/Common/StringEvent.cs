using UnityEngine;

namespace Scripts.Framework.EventSystem.Common
{
	[CreateAssetMenu(fileName = "StringEvent", menuName = "Scriptable/EventSystem/StringEvent")]
	public class StringEvent : Event<string> { }
}

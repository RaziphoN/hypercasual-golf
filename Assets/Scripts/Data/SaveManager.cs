using UnityEngine;

using Scripts.Framework.EventSystem.Common;

namespace Scripts.Data
{
	[RequireComponent(typeof(EventListener))]
	[RequireComponent(typeof(EventListener))]
	public class SaveManager : MonoBehaviour
	{
		[SerializeField] private ScriptablePreference[] preferences = null;

		public void Save()
		{
			if (preferences != null)
			{
				foreach (var pref in preferences)
					pref.Save();
			}
		}

		public void Load()
		{
			if (preferences != null)
			{
				foreach (var pref in preferences)
					pref.Load();
			}
		}

		// handlers are for more clarity over game architecture. It's preferrable to use handler methods as response to events rather than plain save and load
		public void OnSaveRequiredHandler()
		{
			Save();
		}

		public void OnLoadREquiredHandler()
		{
			Load();
		}
	}
}

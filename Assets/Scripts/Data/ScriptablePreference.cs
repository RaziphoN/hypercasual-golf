using UnityEngine;

namespace Scripts.Data
{
	public abstract class ScriptablePreference : ScriptableObject, IPreference
	{
		public abstract string GetSaveKey(string fieldName);
		public abstract void Load();
		public abstract void Save();
	}
}

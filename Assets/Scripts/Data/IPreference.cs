namespace Scripts.Data
{
	public interface IPreference
	{
		string GetSaveKey(string fieldName);
		void Save();
		void Load();
	}
}

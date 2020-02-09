using UnityEngine;
using UnityEngine.SceneManagement;

using System.Linq;

using Scripts.Data;

namespace Scripts.Gameplay
{
	[CreateAssetMenu(fileName = "Level", menuName = "Scriptable/Level", order = 1)]
	public class Level : ScriptableObject, IPreference
	{
		private static readonly string coinsContainerName = "coins";

		[SerializeField] private int starsToUnlock = 0;
		[SerializeField] private Scene scene;

		private int stars = 0;
		private int coins = 0;
		private int minStrokeCount = int.MaxValue;
		private int totalCoinCount = 0;

		public int MinStrokeCount { get { return minStrokeCount; } }
		public int StarCountToUnlock { get { return starsToUnlock; } }
		public int StarCountClaimed { get { return stars; } }
		public int CoinCountClaimed { get { return coins; } }
		public int TotalCoinCount { get { return totalCoinCount; } }

		public string GetSaveKey(string fieldName)
		{
			return "level_" + name + "_" + fieldName;
		}

		public void Save()
		{
			PlayerPrefs.SetInt(GetSaveKey(nameof(stars)), stars);
			PlayerPrefs.SetInt(GetSaveKey(nameof(coins)), coins);
			PlayerPrefs.SetInt(GetSaveKey(nameof(minStrokeCount)), minStrokeCount);
		}

		public void Load()
		{
			stars = PlayerPrefs.GetInt(GetSaveKey(nameof(stars)), 0);
			coins = PlayerPrefs.GetInt(GetSaveKey(nameof(coins)), 0);
			minStrokeCount = PlayerPrefs.GetInt(GetSaveKey(nameof(minStrokeCount)), int.MaxValue);
		}

		private void Awake()
		{
			var gameObjects = scene.GetRootGameObjects();
			var coinObjectContainer = gameObjects.Where(go => go.name == coinsContainerName).First();

			totalCoinCount = coinObjectContainer.transform.childCount;
		}
	}
}

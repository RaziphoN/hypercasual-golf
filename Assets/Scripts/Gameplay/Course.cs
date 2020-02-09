using UnityEngine;

using Scripts.Data;

namespace Scripts.Gameplay
{
	[CreateAssetMenu(fileName = "Course", menuName = "Scriptable/Course", order = 0)]
	public class Course : ScriptablePreference
	{
		[SerializeField] private int starsToUnlock = 0;
		[SerializeField] private string courseName = "Untitled";
		[SerializeField] private Sprite courseBackground = null;
		[SerializeField] private Level[] levels = null;

		private int totalStrokeCount = 0;
		private int number = 1;

		public void SetIndexNumber(int number)
		{
			this.number = number;
		}

		public Sprite Background { get { return courseBackground; } }

		public int StarsToUnlock { get { return starsToUnlock; } }

		public int StarCountClaimed
		{
			get
			{
				int starCount = 0;
				foreach (var level in levels)
					starCount += level.StarCountClaimed;

				return starCount;
			}
		}

		public int CoinCountClaimed
		{
			get
			{
				int coinCount = 0;
				foreach (var level in levels)
					coinCount += level.CoinCountClaimed;

				return coinCount;
			}
		}

		public int TotalCoinCount
		{
			get
			{
				int coinCount = 0;
				foreach (var level in levels)
					coinCount += level.TotalCoinCount;

				return coinCount;
			}
		}

		public string CourseName { get { return courseName; } }

		public int IndexNumber { get { return number; } }

		public int LevelCount { get { return levels.Length; } }

		public Level GetLevel(int index)
		{
			return levels[index];
		}

		public override string GetSaveKey(string fieldName)
		{
			return "course_" + name + "_" + "fieldName";
		}

		public override void Load()
		{
			PlayerPrefs.GetInt(GetSaveKey(nameof(totalStrokeCount)), 0);

			foreach (var level in levels)
				level.Load();
		}

		public override void Save()
		{
			PlayerPrefs.SetInt(GetSaveKey(nameof(totalStrokeCount)), totalStrokeCount);

			foreach (var level in levels)
				level.Save();
		}
	}
}

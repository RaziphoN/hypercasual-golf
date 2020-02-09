using UnityEngine;
using UnityEngine.UI;

using Scripts.Gameplay;
using Scripts.Localization;

namespace Scripts.UI
{
	public class LevelWidget : MonoBehaviour
	{
		[SerializeField] private Text courseLabel = null;
		[SerializeField] private Text strokeLabel = null;
		[SerializeField] private Text coinLabel = null;
		[SerializeField] private Text starsCountLabel = null;
		[SerializeField] private Text starCountToToUnlockLabel = null;

		//[SerializeField] private Button playBtn = null;
		//[SerializeField] private Button settingBtn = null;
		//[SerializeField] private Button nextCourseBtn = null;
		//[SerializeField] private Button levelSelectionBtn = null;

		[SerializeField] private RectTransform nextCourseSelectionContainer = null;
		[SerializeField] private RectTransform nextCourseSelectionLock = null;

		public void FillInfo(Course course, int levelIndex)
		{
			var level = course.GetLevel(levelIndex);

			courseLabel.text = string.Format(LocalizationStrings.CourseLabelString, course.IndexNumber);
			strokeLabel.text = string.Format(LocalizationStrings.StrokeLabelString, level.MinStrokeCount);
			coinLabel.text = string.Format(LocalizationStrings.CoinLabelString, course.CoinCountClaimed, course.TotalCoinCount);
			starsCountLabel.text = string.Format(LocalizationStrings.StarsCountLabelString, course.StarCountClaimed);

			if (levelIndex + 1 < course.LevelCount)
			{
				var nextLevel = course.GetLevel(levelIndex + 1);
				var starLeftToUnlockCount = nextLevel.StarCountToUnlock - course.StarCountClaimed;

				if (starLeftToUnlockCount > 0)
				{
					nextCourseSelectionLock.gameObject.SetActive(true);
					starCountToToUnlockLabel.text = string.Format(LocalizationStrings.StarCountToUnlockLabelString, starLeftToUnlockCount);
				}
				else
				{
					nextCourseSelectionLock.gameObject.SetActive(false);
				}
			}
			else
			{
				nextCourseSelectionContainer.gameObject.SetActive(false);
			}
		}

		// handlers
		public void OnLevelSelectionButtonClickedHandler()
		{

		}

		public void OnPlayButtonClickedHandler()
		{

		}

		public void OnNextCourseButtonClickedHandler()
		{

		}

		public void OnSettingsButtonCLickedHandler()
		{

		}
	}
}

using UnityEngine;

using Scripts.Localization;

namespace Scripts.UI
{
	public class IngameWidget : MonoBehaviour
	{
		[SerializeField] private LayeredLabel collectedCoinCountLabel = null;
		[SerializeField] private LayeredLabel strokeCountLabel = null;

		public void SetStrokeCount(int count)
		{
			strokeCountLabel.SetText(string.Format(LocalizationStrings.StrokeLabelString, count));
		}

		public void SetCoinCount(int collected, int total)
		{
			collectedCoinCountLabel.SetText(string.Format(LocalizationStrings.CoinLabelString, collected, total));
		}
	}
}

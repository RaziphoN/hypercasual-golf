using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
	[RequireComponent(typeof(Image))]
	public class StarWidget : MonoBehaviour
	{
		public Sprite oneStar;
		public Sprite twoStar;
		public Sprite threeStar;

		[Range(0.01f, 0.99f)]
		public float twoStarPercentage = 0.5f;
		[Range(0.02f, 1f)]
		public float threeStarPercentage = 1.0f;

		private Image m_image;

		private void Awake()
		{
			m_image = GetComponent<Image>();
			m_image.sprite = oneStar;
		}

		public void SetCompletionPercent(float percent)
		{
			percent = Mathf.Clamp(percent, 0f, 1f);

			if (percent >= twoStarPercentage && percent <= threeStarPercentage)
				m_image.sprite = twoStar;
			else if (percent >= threeStarPercentage)
				m_image.sprite = threeStar;
			else
				m_image.sprite = oneStar;
		}
	}
}

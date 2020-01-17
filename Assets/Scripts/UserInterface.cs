using UnityEngine;
using UnityEngine.UI;

using Scripts.Framework.Utils;
using Scripts.UI;

namespace Scripts
{
	public class UserInterface : MonoBehaviour
	{
		public static UserInterface instance;

		private RectTransform m_ingame;
		private LayeredLabel m_ingameCoins;
		private LayeredLabel m_ingameStrokes;

		private RectTransform m_victory;
		private LayeredLabel m_victoryCoins;
		private LayeredLabel m_victoryMaxCoins;
		private LayeredLabel m_victoryStrokes;
		private StarWidget m_victoryStarWidget;

		private void Awake()
		{
			instance = this;

			m_ingame = Hierarchy.FindComponentInChildDeep<RectTransform>(gameObject, "ingame");
			m_ingameCoins = Hierarchy.FindComponentInChildDeep<LayeredLabel>(m_ingame.gameObject, "coins_value");
			m_ingameStrokes = Hierarchy.FindComponentInChildDeep<LayeredLabel>(m_ingame.gameObject, "strokes_value");

			m_victory = Hierarchy.FindComponentInChildDeep<RectTransform>(gameObject, "victory");
			m_victoryStarWidget = Hierarchy.FindComponentInChildDeep<StarWidget>(m_victory.gameObject, "stars");
			m_victoryCoins = Hierarchy.FindComponentInChildDeep<LayeredLabel>(m_victory.gameObject, "coins_value");
			m_victoryMaxCoins = Hierarchy.FindComponentInChildDeep<LayeredLabel>(m_victory.gameObject, "coins_max_value");
			m_victoryStrokes = Hierarchy.FindComponentInChildDeep<LayeredLabel>(m_victory.gameObject, "strokes_value");
		}

		private void Start()
		{
			ShowVictory(false);
		}

		public void SetScore(int score)
		{
			m_ingameCoins.SetText(score.ToString());
		}

		public void SetCompletionPercent(float percent)
		{
			m_victoryStarWidget.SetCompletionPercent(percent);
		}

		public void SetMaxScore(int score)
		{
			m_victoryMaxCoins.SetText(score.ToString());
		}

		public void SetStrokes(int strokes)
		{
			m_ingameStrokes.SetText(strokes.ToString());
		}

		public void ShowVictory(bool show)
		{
			m_ingame.gameObject.SetActive(!show);
			m_victory.gameObject.SetActive(show);

			m_victoryCoins.SetText(m_ingameCoins.text);
			m_victoryStrokes.SetText(m_ingameStrokes.text);
		}
	}
}

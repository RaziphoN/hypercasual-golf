using UnityEngine;
using UnityEngine.Events;
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
		private LayeredLabel m_victoryStrokes;
		private StarWidget m_victoryStarWidget;

		private Button m_victoryContinueButton;
		private Button m_victoryReplayButton;

		private int m_maxScore = 0; // hacky a bit :)

		private void Awake()
		{
			instance = this;

			m_ingame = Hierarchy.FindComponentInChildDeep<RectTransform>(gameObject, "ingame");
			m_ingameCoins = Hierarchy.FindComponentInChildDeep<LayeredLabel>(m_ingame.gameObject, "coins_value");
			m_ingameStrokes = Hierarchy.FindComponentInChildDeep<LayeredLabel>(m_ingame.gameObject, "strokes_value");

			m_victory = Hierarchy.FindComponentInChildDeep<RectTransform>(gameObject, "victory");
			m_victoryStarWidget = Hierarchy.FindComponentInChildDeep<StarWidget>(m_victory.gameObject, "stars");
			m_victoryCoins = Hierarchy.FindComponentInChildDeep<LayeredLabel>(m_victory.gameObject, "coins");
			m_victoryStrokes = Hierarchy.FindComponentInChildDeep<LayeredLabel>(m_victory.gameObject, "strokes");
			m_victoryContinueButton = Hierarchy.FindComponentInChildDeep<Button>(m_victory.gameObject, "continue_btn");
			m_victoryReplayButton = Hierarchy.FindComponentInChildDeep<Button>(m_victory.gameObject, "replay_btn");
		}

		private void Start()
		{
			ShowVictory(false);
		}

		public void SubscribeOnContinue(UnityAction callback)
		{
			m_victoryContinueButton.onClick.AddListener(callback);
		}

		public void UnsubscribeOnContinue(UnityAction callback)
		{
			m_victoryContinueButton.onClick.RemoveListener(callback);
		}

		public void SubscribeOnReplay(UnityAction callback)
		{
			m_victoryReplayButton.onClick.AddListener(callback);
		}

		public void UnsubscribeOnReplay(UnityAction callback)
		{
			m_victoryReplayButton.onClick.RemoveListener(callback);
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
			m_maxScore = score;
		}

		public void SetStrokes(int strokes)
		{
			m_ingameStrokes.SetText(strokes.ToString());
		}

		public void ShowVictory(bool show)
		{
			m_ingame.gameObject.SetActive(!show);
			m_victory.gameObject.SetActive(show);

			if (show)
			{
				m_victoryCoins.SetText("Coins: " + m_ingameCoins.GetText() + " / " + m_maxScore.ToString());
				m_victoryStrokes.SetText("Strokes: " + m_ingameStrokes.GetText());
			}
		}
	}
}

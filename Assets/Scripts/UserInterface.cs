using UnityEngine;
using UnityEngine.UI;

using Scripts.Framework.Utils;

namespace Scripts
{
	public class UserInterface : MonoBehaviour
	{
		public static UserInterface instance;

		private Text m_score;
		private Text m_strokes;
		private GameObject m_victory;

		private void Awake()
		{
			instance = this;

			m_score = Hierarchy.FindComponentInChildDeep<Text>(gameObject, "score_value");
			m_strokes = Hierarchy.FindComponentInChildDeep<Text>(gameObject, "strokes_value");
			m_victory = Hierarchy.FindChildDeep(gameObject, "victory");
		}

		private void Start()
		{
			ShowVictory(false);
		}

		public void SetScore(int score)
		{
			m_score.text = score.ToString();
		}

		public void SetStrokes(int strokes)
		{
			m_strokes.text = strokes.ToString();
		}

		public void ShowVictory(bool show)
		{
			m_victory.SetActive(show);

			m_strokes.gameObject.SetActive(!show);
			m_score.gameObject.SetActive(!show);
		}
	}
}

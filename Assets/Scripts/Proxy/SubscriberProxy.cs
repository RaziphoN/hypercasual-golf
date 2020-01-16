using UnityEngine;

namespace Scripts.Details
{
	public class SubscriberProxy : MonoBehaviour
	{
		UserInterface m_ui;
		InputScheme m_input;
		Profile m_profile;

		private void Start()
		{
			m_input = FindObjectOfType<InputScheme>();
			m_ui = UserInterface.instance;
			m_profile = Profile.instance;

			Subscribe();
		}

		private void OnDestroy()
		{
			Unsubscribe();
		}

		private void Subscribe()
		{
			m_input.onObjectStroke += OnObjectStroke;
			m_profile.onScoreChanged += OnScoreChanged;
			m_profile.onStrokesChanged += OnStrokesChanged;
		}

		private void Unsubscribe()
		{
			m_input.onObjectStroke -= OnObjectStroke;
			m_profile.onScoreChanged -= OnScoreChanged;
			m_profile.onStrokesChanged -= OnStrokesChanged;
		}

		private void OnScoreChanged(int score)
		{
			m_ui.SetScore(score);
		}

		private void OnStrokesChanged(int strokes)
		{
			m_ui.SetStrokes(strokes);
		}

		private void OnObjectStroke(GameObject obj)
		{
			m_profile.strokes++;
		}
	}
}

using UnityEngine;
using UnityEngine.SceneManagement;

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

			m_ui.SetMaxScore(Coin.count);

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
			m_ui.SubscribeOnContinue(OnContinueClicked);
			m_ui.SubscribeOnReplay(OnReplayClicked);
		}

		private void Unsubscribe()
		{
			m_input.onObjectStroke -= OnObjectStroke;
			m_profile.onScoreChanged -= OnScoreChanged;
			m_profile.onStrokesChanged -= OnStrokesChanged;
			m_ui.UnsubscribeOnContinue(OnContinueClicked);
			m_ui.UnsubscribeOnReplay(OnReplayClicked);
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

		private void OnContinueClicked()
		{
			var nextSceneIdx = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
			StartLevel(nextSceneIdx);
		}

		private void OnReplayClicked()
		{
			var index = SceneManager.GetActiveScene().buildIndex;
			StartLevel(index);
		}

		public void StartLevel(int index)
		{
			m_profile.OnLevelComplete();
			m_ui.ShowVictory(false);

			SceneManager.LoadScene(index);
		}
	}
}

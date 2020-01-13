using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts
{
	[RequireComponent(typeof(AudioSource))]
	public class GolfHole : MonoBehaviour
	{
		public float maxSpeedOfBall = 2f;
		public float delayBeforeNextLevel = 1f;

		private AudioSource m_audio;

		private void Awake()
		{
			m_audio = GetComponent<AudioSource>();
		}

		private void OnTriggerStay2D(Collider2D collision)
		{
			if (collision.gameObject.tag == "Player")
			{
				var rigid = collision.GetComponent<Rigidbody2D>();

				if (rigid.velocity.magnitude <= maxSpeedOfBall)
				{
					Destroy(collision.gameObject);
					Invoke("OnVictoryEnd", delayBeforeNextLevel);
					OnVictory();
				}
			}
		}

		private void OnVictory()
		{
			m_audio.Play();
			UI.instance.ShowVictory(true);
		}

		private void OnVictoryEnd()
		{
			Profile.instance.OnLevelComplete();
			UI.instance.ShowVictory(false);

			var nextSceneIdx = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
			SceneManager.LoadScene(nextSceneIdx);
		}
	}
}
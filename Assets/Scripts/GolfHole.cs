﻿using UnityEngine;
using UnityEngine.SceneManagement;

using Scripts.Framework.Audio;

namespace Scripts
{
	[RequireComponent(typeof(AudioSource))]
	public class GolfHole : MonoBehaviour
	{
		public readonly string victorySfxName = "applause";
		public float maxSpeedOfBall = 2f;
		public float delayBeforeNextLevel = 1f;

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
			AudioManager.instance.Play(victorySfxName);

			UserInterface.instance.SetCompletionPercent((float)(Profile.instance.score) / Coin.count); // a little bit hacky too
			UserInterface.instance.ShowVictory(true); // as well as this code in this class, LMAO
		}

		private void OnVictoryEnd()
		{
			Profile.instance.OnLevelComplete();
			UserInterface.instance.ShowVictory(false);

			var nextSceneIdx = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
			SceneManager.LoadScene(nextSceneIdx);
		}
	}
}
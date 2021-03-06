﻿using UnityEngine;

using Scripts.Framework.Audio;

namespace Scripts
{
	[RequireComponent(typeof(Collider2D))]
	public class Coin : MonoBehaviour
	{
		public static int count = 0; // HACK
		public readonly string coinCollectSfxName = "coin";

		private void Awake()
		{
			count = FindObjectsOfType<Coin>().Length; // HACK
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.gameObject.tag == "Player")
			{
				AudioManager.instance.Play(coinCollectSfxName);

				Profile.instance.score++;
				Destroy(gameObject, 0.1f);
			}
		}
	}
}

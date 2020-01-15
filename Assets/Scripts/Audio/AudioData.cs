using UnityEngine;

namespace Scripts.Framework.Audio
{
	public class AudioData : MonoBehaviour
	{
		public string group = "";

		public bool isLooping = false;
		public bool isPlayOnAwake = false;

		public bool isFadeIn = false;
		public float fadeInDuration = 0.5f;

		public bool isFadeOut = false;
		public float fadeOutDuration = 0.5f;

		[Range(0.0f, 1.0f)]
		public float volumeOnStart = 1.0f;

		public AudioClip clip = null;

		[HideInInspector]
		public AudioSource source = null;
	}
}

using UnityEngine;

using System.Collections;
using System.Collections.Generic;

namespace Scripts.Framework.Audio
{
	public class AudioManager : MonoBehaviour
	{
		public static AudioManager instance;

		[SerializeField]
		public bool collectAudioDataAutomatically = true;

		[SerializeField]
		private List<AudioData> m_audios = new List<AudioData>();

		public bool isAllMuted { get; private set; } = false;
		public bool isAllPaused { get; private set; } = false;

		private void Awake()
		{
			instance = this;

			if (collectAudioDataAutomatically)
			{
				var audios = FindObjectsOfType<AudioData>();
				m_audios.Clear();
				m_audios.AddRange(audios);
			}

			foreach (var audio in m_audios)
			{
				audio.source = gameObject.AddComponent<AudioSource>();

				var source = audio.source;
				source.clip = audio.clip;
				source.loop = audio.isLooping;
				source.volume = audio.volumeOnStart;
				source.name = audio.clip.name;
				source.playOnAwake = audio.isPlayOnAwake;

				if (audio.isPlayOnAwake)
					Play(audio);
			}
		}

		public void OnDestroy()
		{
			StopAll();
		}

		private void OnApplicationPause(bool paused)
		{
			if (paused)
			{
				PauseAll();
			}
			else
			{
				UnpauseAll();
			}
		}

		private void OnApplicationFocus(bool focused)
		{
			if (!focused)
			{
				PauseAll();
			}
			else
			{
				UnpauseAll();
			}
		}

		public void Play(string name)
		{
			var found = m_audios.Find(audio => audio.clip.name == name);
			Debug.Assert(found != null);

			Play(found);
		}

		public void PlayGroup(string group)
		{
			List<AudioData> groupList = GetAudioGroup(group);

			if (groupList.Count == 0)
			{
				Debug.Log("[AudioManager] Trying to play empty group: " + group);
				return;
			}

			foreach (var audio in groupList)
			{
				Play(audio);
			}
		}

		public void PlayRandomFromGroup(string group)
		{
			List<AudioData> groupList = GetAudioGroup(group);

			if (groupList.Count == 0)
			{
				Debug.Log("[AudioManager] Trying to play random from empty group: " + group);
				return;
			}

			int idx = Random.Range(0, groupList.Count);
			Play(groupList[idx]);
		}

		public void Stop(string name)
		{
			var found = m_audios.Find(audio => audio.clip.name == name);
			Debug.Assert(found != null);

			Stop(found);
		}

		public void StopGroup(string group)
		{
			List<AudioData> groupList = GetAudioGroup(group);

			if (groupList.Count == 0)
			{
				Debug.Log("[AudioManager] Trying to stop empty group: " + group);
				return;
			}

			foreach (var audio in groupList)
			{
				if (audio.source.isPlaying)
				{
					Stop(audio);
				}
			}
		}

		public void StopAll()
		{
			foreach (var audio in m_audios)
			{
				Stop(audio);
			}
		}

		public void PauseAll()
		{
			foreach (var audio in m_audios)
			{
				if (audio.source.isPlaying)
				{
					audio.source.Pause();
				}
			}

			isAllPaused = true;
		}

		public void UnpauseAll()
		{
			foreach (var audio in m_audios)
			{
				audio.source.UnPause();
			}

			isAllPaused = false;
		}

		public void SetVolume(string name, float value)
		{
			var found = m_audios.Find(audio => audio.clip.name == name);
			Debug.Assert(found != null);

			found.source.volume = value;
		}

		public void SetVolumeGroup(string group, float value)
		{
			List<AudioData> groupList = GetAudioGroup(group);

			if (groupList.Count == 0)
			{
				Debug.Log("[AudioManager] Trying to set volume for empty group: " + group);
				return;
			}

			foreach (var audio in groupList)
			{
				if (audio.source.isPlaying)
				{
					audio.source.volume = value;
				}
			}
		}

		public void SetVolumeAll(float value)
		{
			foreach (var audio in m_audios)
			{
				if (audio.source.isPlaying)
				{
					audio.source.volume = value;
				}
			}
		}

		public void SetMuted(string name, bool muted)
		{
			var found = m_audios.Find(audio => audio.clip.name == name);
			Debug.Assert(found != null);

			found.source.mute = muted;
		}

		public void SetMutedGroup(string group, bool muted)
		{
			List<AudioData> groupList = GetAudioGroup(group);

			if (groupList.Count == 0)
			{
				Debug.Log("[AudioManager] Trying to mute empty group: " + group);
				return;
			}

			foreach (var audio in groupList)
			{
				audio.source.mute = muted;
			}
		}

		public void SetMutedAll(bool muted)
		{
			foreach (var audio in m_audios)
			{
				audio.source.mute = muted;
			}

			isAllMuted = muted;
		}

		private void Play(AudioData audio)
		{
			audio.source.Play();

			if (audio.isFadeIn)
			{
				StartCoroutine(PlayFadeIn(audio));
			}
		}

		private void Stop(AudioData audio)
		{
			if (audio.isFadeOut)
			{
				StartCoroutine(PlayFadeOut(audio));
			}
			else
			{
				audio.source.Stop();
			}
		}

		private IEnumerator PlayFadeIn(AudioData audio)
		{
			var duration = audio.fadeInDuration;
			audio.source.volume = 0.0f;

			while (duration > 0.0f)
			{
				duration -= Time.deltaTime;
				audio.source.volume = Mathf.Lerp(0.0f, audio.volumeOnStart, duration / audio.fadeInDuration);

				yield return null;
			}
		}

		private IEnumerator PlayFadeOut(AudioData audio)
		{
			var duration = audio.fadeOutDuration;
			
			while (duration > 0.0f)
			{
				duration -= Time.deltaTime;
				audio.source.volume = Mathf.Lerp(audio.source.volume, 0.0f, duration / audio.fadeOutDuration);

				yield return null;
			}

			audio.source.Stop();
		}

		private List<AudioData> GetAudioGroup(string group)
		{
			List<AudioData> groupList = new List<AudioData>();

			foreach (var audio in m_audios)
			{
				if (audio.group == group)
				{
					groupList.Add(audio);
				}
			}

			return groupList;
		}
	}
}

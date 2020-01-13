using UnityEngine;

namespace Scripts
{
	[RequireComponent(typeof(Collider2D))]
	public class Coin : MonoBehaviour
	{
		private AudioSource m_source;

		private void Awake()
		{
			m_source = GetComponent<AudioSource>();
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.gameObject.tag == "Player")
			{
				m_source.Play();

				Profile.instance.score++;
				Destroy(gameObject, 0.1f);
			}
		}
	}
}

using UnityEngine;

namespace Scripts
{
	[RequireComponent(typeof(Collider2D))]
	public class Coin : MonoBehaviour
	{
		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.gameObject.tag == "Player")
			{
				Profile.instance.score++;
				Destroy(gameObject);
			}
		}
	}
}

using UnityEngine;

namespace Scripts
{
	[RequireComponent(typeof(Collider2D))]
	[RequireComponent(typeof(Rigidbody2D))]
	public class CollisionStopper : MonoBehaviour
	{
		[Header("Options")]
		public float magnitudePerSecond = 1f;
		public LayerMask collisionMask;

		// debug
		[Header("Debug")]
		public bool isWorking = false;

		private Collider2D m_collider;
		private Rigidbody2D m_rigid;

		private void Awake()
		{
			m_collider = GetComponent<Collider2D>();
			m_rigid = GetComponent<Rigidbody2D>();
		}

		private void FixedUpdate()
		{
			if (Physics2D.IsTouchingLayers(m_collider, collisionMask))
			{
				isWorking = true;
				m_rigid.AddForce((-m_rigid.velocity.normalized) * magnitudePerSecond * Time.fixedDeltaTime);
			}
			else
			{
				isWorking = false;
			}
		}
	}
}

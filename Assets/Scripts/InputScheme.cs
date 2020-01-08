using UnityEngine;

namespace Scripts
{
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(Collider2D))]
	public class InputScheme : MonoBehaviour
	{
		[Header("Options")]
		public float forceMultiplier = 1.3f;
		public bool reversedInput = false;

		[Header("Input Contraints")]

		[Tooltip("if enabled, you can't push it's object while it's moving faster than 'maxVelocityToAllowInput'")]
		public bool velocityConstraint;
		public float maxVelocityToAllowInput = 0.2f;

		[Tooltip("if enabled, you can't push it's object while it isn't touch any object collider with 'groundMask'")]
		public bool groundConstraint;
		public LayerMask groundMask;

		private Rigidbody2D m_rigid;
		private Collider2D m_collider;
		private ContactFilter2D m_filter = new ContactFilter2D();
		private Vector2[] m_touchPoints = new Vector2[2];

		private void Awake()
		{
			m_rigid = GetComponent<Rigidbody2D>();

			m_collider = GetComponent<Collider2D>();
			m_filter.layerMask = groundMask;
		}

		private void Update()
		{
			if (InputUtility.IsTouchedThisFrame())
			{
				m_touchPoints[0] = InputUtility.GetTouchPosition();
			}
			else if (InputUtility.IsTouchCanceledThisFrame())
			{
				//m_touchPoints[1] = InputUtility.GetTouchPosition();
				m_touchPoints[1] = Input.mousePosition;

				if (IsAllowedToForce())
				{
					Vector2 diff = m_touchPoints[1] - m_touchPoints[0];

					if (reversedInput)
					{
						diff = -diff;
					}

					diff *= forceMultiplier;

					m_rigid.AddForce(diff);
				}
			}
		}

		public bool IsAllowedToForce()
		{
			bool velocityCond = velocityConstraint ? m_rigid.velocity.magnitude <= maxVelocityToAllowInput : true;
			bool groundCond = groundConstraint ? Physics2D.IsTouchingLayers(m_collider, groundMask) : true;

			return velocityCond && groundCond;
		}
	}
}

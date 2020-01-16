using UnityEngine;

namespace Scripts
{
	[ExecuteInEditMode]
	public class CoinPlacer : MonoBehaviour
	{
		[Header("Options")]
		public bool isEnabled = false;

		public Transform coinsParent;
		public Coin prefab;
		public float distanceBetweenCoins = 0.5f;

		private Transform m_prev;

		private Rigidbody2D m_rigid;
		private Vector3[] m_touchPoints = new Vector3[2];

		private void Awake()
		{
			m_rigid = GetComponent<Rigidbody2D>();
		}

		// Update is called once per frame
		void Update()
		{
			if (isEnabled && prefab != null)
			{
				if (m_prev == null)
					Create();
				else
				{
					if (Vector3.Distance(m_prev.position, transform.position) >= distanceBetweenCoins)
						Create();
				}

				if (InputUtility.IsTouchedThisFrame())
				{
					m_touchPoints[0] = InputUtility.GetTouchPosition();
				}
				else if (InputUtility.IsTouchCanceledThisFrame())
				{
					//m_touchPoints[1] = InputUtility.GetTouchPosition();
					m_touchPoints[1] = Input.mousePosition;

					// if (IsAllowedToForce())
					{
						Vector2 diff = m_touchPoints[1] - m_touchPoints[0];
						diff = -diff;

						diff *= 1.3f;
						m_rigid.AddForce(diff);
					}
				}
			}

			
		}

		void Create()
		{
			m_prev = Instantiate(prefab, coinsParent).transform;
			m_prev.position = transform.position;
		}
	}
}
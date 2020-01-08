using UnityEngine;

namespace Scripts
{
	[RequireComponent(typeof(LineRenderer))]
	public class InputVizualizer : MonoBehaviour
	{
		public float lineZ = -2f;
		public float startWidth = 1f;
		public float endWidth = 2f;

		public float colorForceCoefficient = 0.1f;
		public Color startColor = Color.white;
		public Color endColor = Color.red;

		private LineRenderer m_renderer;
		private InputScheme m_input;

		private void Awake()
		{
			m_input = FindObjectOfType<InputScheme>();
			m_renderer = GetComponent<LineRenderer>();

			m_renderer.positionCount = 2;

			m_renderer.startWidth = startWidth;
			m_renderer.endWidth = endWidth;

			m_renderer.startColor = startColor;
			m_renderer.endColor = endColor;


		}

		private void LateUpdate()
		{
			if (InputUtility.IsTouchedThisFrame())
			{
				var world = InputUtility.GetTouchPositionWorld();
				world.z = lineZ;
				m_renderer.SetPosition(0, world);
				 
				SetShow(true);
			}

			if (InputUtility.IsTouch())
			{
				var world = InputUtility.GetTouchPositionWorld();
				world.z = lineZ;

				m_renderer.SetPosition(1, world);
				m_renderer.endColor = GetEndColor();
			}

			if (InputUtility.IsTouchCanceledThisFrame())
				SetShow(false);
		}

		private void SetShow(bool show)
		{
			if (m_renderer.enabled != show)
				m_renderer.enabled = show;
		}

		private Color GetEndColor()
		{
			var forceMultiplier = m_input.forceMultiplier;
			var color = Color.Lerp(startColor, endColor, 1f - colorForceCoefficient / ((m_renderer.GetPosition(1) - m_renderer.GetPosition(0)).magnitude * forceMultiplier));
			color.a = endColor.a;

			return color;
		}
	}
}

using UnityEngine;

namespace Scripts.Test
{
	public class InputUtilityTest : MonoBehaviour
	{
		public int isTouchedThisFrameCount = 0;
		public int isCanceledTouchThisFrameCount = 0;

		public bool isTouch = false;

		public Vector2 cursorPos;
		public Vector2 screenTouch;
		public Vector3 worldTouch;

		private void Update()
		{
			if (InputUtility.IsTouchedThisFrame())
				++isTouchedThisFrameCount;

			if (InputUtility.IsTouchCanceledThisFrame())
				++isCanceledTouchThisFrameCount;

			isTouch = InputUtility.IsTouch();

			cursorPos = InputUtility.GetCursorPosition();
			screenTouch = InputUtility.GetTouchPosition();
			worldTouch = InputUtility.GetTouchPositionWorld();
		}
	}
}

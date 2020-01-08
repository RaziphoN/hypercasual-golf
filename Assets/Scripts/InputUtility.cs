using UnityEngine;

namespace Scripts
{
	public static class InputUtility
	{
		public static bool IsTouchedThisFrame()
		{
#if UNITY_STANDALONE
			return Input.GetMouseButtonDown(0);
#elif UNITY_ANDROID || UNITY_IOS
			if (Input.touchCount > 0)
			{
				return Input.GetTouch(0).phase == TouchPhase.Began;
			}

			return false;
#endif	
		}

		public static bool IsTouchCanceledThisFrame()
		{
#if UNITY_STANDALONE
			return Input.GetMouseButtonUp(0);
#elif UNITY_ANDROID || UNITY_IOS
			if (Input.touchCount > 0)
			{
				var phase = Input.GetTouch(0).phase;
				return phase == TouchPhase.Ended || phase == TouchPhase.Canceled;
			}

			return false;
#endif
		}

		public static bool IsTouch()
		{
#if UNITY_STANDALONE
			return Input.GetMouseButton(0);
#elif UNITY_ANDROID || UNITY_IOS
			return Input.touchCount > 0;
#endif
		}

		// returns cursor position in screen space
		public static Vector2 GetCursorPosition()
		{
#if UNITY_STANDALONE
			return Input.mousePosition;
#elif UNITY_ANDROID || UNITY_IOS
			if (IsTouch())
			{
				return Input.GetTouch(0).position;
			}

			return Vector2.zero;
#endif
		}

		/*	
			works like GetCursorPosition, but returns zero vector if we aren't touching screen
			done for same behaviour on PC & mobile
			(screen space) 
		*/
		public static Vector2 GetTouchPosition()
		{
			if (IsTouch())
			{
				return GetCursorPosition();
			}

			return Vector2.zero;
		}

		// takes main camera
		public static Vector3 GetTouchPositionWorld()
		{
			return GetTouchPositionWorld(Camera.main);
		}

		// returns touch position in world coords or zero if there is no touch
		public static Vector3 GetTouchPositionWorld(Camera cam)
		{
			if (IsTouch())
			{
				Vector3 world = cam.ScreenToWorldPoint(GetTouchPosition());
				return world;
			}

			return Vector3.zero;
		}
	}
}

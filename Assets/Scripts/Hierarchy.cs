using UnityEngine;

using System.Collections.Generic;

namespace Scripts.Framework.Utils
{
	public static class Hierarchy
	{
		public static T FindComponentInChild<T>(GameObject root, string name) where T : Component
		{
			var go = FindChild(root, name);

			if (go != null)
				return go.GetComponent<T>();

			return null;
		}

		public static T FindComponentInChildDeep<T>(GameObject root, string name) where T : Component
		{
			var go = FindChildDeep(root, name);

			if (go != null)
				return go.GetComponent<T>();

			return null;
		}

		public static GameObject FindChild(GameObject root, string name)
		{
			var transform = root.transform.Find(name);
			if (transform != null)
				return transform.gameObject;
			else
				return null;
		}

		public static GameObject FindChildDeep(GameObject root, string name)
		{
			Queue<Transform> queue = new Queue<Transform>();
			queue.Enqueue(root.transform);

			while (queue.Count > 0)
			{
				var transform = queue.Dequeue();

				var found = transform.Find(name);
				if (found != null)
					return found.gameObject;
				else
				{
					for (int idx = 0; idx < transform.childCount; ++idx)
					{
						var child = transform.GetChild(idx);
						if (child.gameObject.activeSelf)
							queue.Enqueue(child);
					}
				}
			}

			return null;
		}
	}
}

using UnityEngine;

using System.Collections.Generic;

namespace Scripts.Utility
{
	public static class Triangulator
	{
		public class Triangle
		{
			public Triangle(int _a, int _b, int _c)
			{
				a = _a;
				b = _b;
				c = _c;
			}

			public int a;
			public int b;
			public int c;
		}

		public static Triangle[] Triangulate(Vector3[] points, Vector3 normal)
		{
			const float epsilon = 0.001f;

			List<Triangle> triangles = new List<Triangle>();

			bool[] active = new bool[points.Length];
			for (int i = 0; i < active.Length; ++i)
				active[i] = true;

			int start = 0;
			int p1 = 0, p2 = 1;
			int m1 = points.Length - 1, m2 = points.Length - 2;

			bool lastPositive = false;
			while (true)
			{
				if (p2 == m2)
				{
					// Only three vertices remain
					triangles.Add(new Triangle(m1, p1, p2));

					break;
				}

				bool positive = false, negative = false;

				// Determine whether vp1, vp2 & vm1 form a valid triangle
				positive = IsValidTriangle(points, normal, active, epsilon, p1, p2, m1);

				// Determine whether vm1, vm2 && vp1 form a valid triangle
				negative = IsValidTriangle(points, normal, active, epsilon, m1, p1, m2);

				// If both triangles are valid, choose the one having the largest smallest angle
				if (positive && negative)
				{
					float pd = Vector3.Dot((points[p2] - points[m1]).normalized, (points[m2] - points[m1]).normalized);
					float md = Vector3.Dot((points[m2] - points[p1]).normalized, (points[p2] - points[p1]).normalized);

					if (Mathf.Abs(pd - md) < epsilon)
					{
						if (lastPositive)
							positive = false;
						else
							negative = false;
					}
					else
					{
						if (pd < md)
							negative = false;
						else
							positive = false;
					}
				}

				if (positive)
				{
					active[p1] = false;
					triangles.Add(new Triangle(m1, p1, p2));
					p1 = GetNextActive(p1, points.Length, active);
					p2 = GetNextActive(p2, points.Length, active);
					lastPositive = true;
					start = -1;
				}
				else if (negative)
				{
					active[m1] = false;
					triangles.Add(new Triangle(m2, m1, p1));
					m1 = GetPrevActive(m1, points.Length, active);
					m2 = GetPrevActive(m2, points.Length, active);
					lastPositive = false;
					start = -1;
				}
				else
				{
					// Exit if we've gone all the way around the polygon without finding a valid triangle
					if (start == -1)
						start = p2;
					else if (p2 == start)
						break;

					// advance working set of vertices
					m2 = m1;
					m1 = p1;
					p1 = p2;
					p2 = GetNextActive(p2, points.Length, active);
				}
			}

			return triangles.ToArray();
		}

		private static bool IsValidTriangle(Vector3[] points, Vector3 normal, bool[] active, float epsilon, int p1, int p2, int p3)
		{
			bool result = false;

			Vector3 n1 = Vector3.Cross(normal, (points[p3] - points[p2]).normalized);
			if (Vector3.Dot(n1, (points[p1] - points[p2])) > epsilon)
			{
				result = true;
				Vector3 n2 = Vector3.Cross(normal, (points[p1] - points[p3]).normalized);
				Vector3 n3 = Vector3.Cross(normal, (points[p2] - points[p1]).normalized);

				// Look for other vertices inside the triangle (check actually)
				for (int a = 0; a < points.Length; ++a)
				{
					if (active[a] && a != p1 && a != p2 && a != p3)
					{
						Vector3 v = points[a];

						if (Vector3.Dot(n1, (v - points[p2]).normalized) > -epsilon
							&& Vector3.Dot(n2, (v - points[p3]).normalized) > -epsilon
							&& Vector3.Dot(n3, (v - points[p1]).normalized) > -epsilon)
						{
							result = false;
							break;
						}
					}
				}
			}

			return result;
		}

		private static int GetNextActive(int x, int vertexCount, bool[] active)
		{
			while (true)
			{
				if (++x == vertexCount)
					x = 0;

				if (active[x])
					return x;
			}
		}

		private static int GetPrevActive(int x, int vertexCount, bool[] active)
		{
			while (true)
			{
				if (--x == -1)
					x = vertexCount - 1;

				if (active[x])
					return x;
			}
		}
	}
}

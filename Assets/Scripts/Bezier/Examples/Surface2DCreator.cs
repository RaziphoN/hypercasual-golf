using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PathCreator))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(EdgeCollider2D))]
public class Surface2DCreator : MonoBehaviour
{
	[Header("Options")]
    [Range(0.05f, 1.5f)]
    public float spacing = 1f;

	[Tooltip("Bottom point of surface. It's required to make surface flat on bottom")]
    public float surfaceY = -5f;

	[Tooltip("Updates surface automatically (real-time) when any option is changed")]
	public bool autoUpdate;

	public void UpdateSurface()
    {
        Path path = GetComponent<PathCreator>().path;
        Vector2[] points = path.CalculateEvenlySpacedPoints(spacing);

		GetComponent<MeshFilter>().mesh = CreateSurface(points);
		GetComponent<EdgeCollider2D>().points = points;
    }

    Mesh CreateSurface(Vector2[] points)
    {
        //Vector3[] verts = new Vector3[points.Length * 2];
        //Vector2[] uvs = new Vector2[verts.Length];
        //int numTris = 2 * (points.Length - 1);
        //int[] tris = new int[numTris * 3];
        //int vertIndex = 0;
        //int triIndex = 0;

		Vector3[] vertices = new Vector3[points.Length];
		int[] tris = new int[(vertices.Length - 2) * 3];

		for (int i = 0; i < points.Length; ++i)
			vertices[i] = new Vector3(points[i].x, points[i].y, 0f);

		var triangles = Scripts.Utility.Triangulator.Triangulate(vertices, Vector3.back);

		int triangleIndex = 0;
		for (int i = 0; i < triangles.Length; ++i)
		{
			tris[triangleIndex] = triangles[i].a;
			tris[triangleIndex + 1] = triangles[i].b;
			tris[triangleIndex + 2] = triangles[i].c;

			triangleIndex += 3;
		}

   //     for (int i = 0; i < points.Length; i++)
   //     {
   //         //Vector2 forward = Vector2.zero;
   //         //if (i < points.Length - 1)
   //         //{
   //         //    forward += points[(i + 1)%points.Length] - points[i];
   //         //}
   //         //if (i > 0)
   //         //{
   //         //    forward += points[i] - points[(i - 1 + points.Length)%points.Length];
   //         //}

   //         //forward.Normalize();

			//// vertices
			//{
			//	// Vector2 left = new Vector2(-forward.y, forward.x);

			//	verts[vertIndex] = points[i];
			//	verts[vertIndex + 1] = new Vector2(points[i].x, surfaceY);
			//}

			//// uv
			//{
			//	float completionPercent = i / (float)(points.Length - 1);
			//	float v = 1 - Mathf.Abs(2 * completionPercent - 1);

			//	uvs[vertIndex] = new Vector2(0, v);
			//	uvs[vertIndex + 1] = new Vector2(1, v);
			//}

   //         if (i < points.Length - 1)
   //         {
			//	tris[triIndex] = vertIndex;
   //             tris[triIndex + 1] = (vertIndex + 2) % verts.Length;
			//	tris[triIndex + 2] = vertIndex + 1;

			//	tris[triIndex + 3] = vertIndex + 1;
   //             tris[triIndex + 4] = (vertIndex + 2) % verts.Length;
   //             tris[triIndex + 5] = (vertIndex + 3)  % verts.Length;
   //         }

   //         vertIndex += 2;
   //         triIndex += 6;
   //     }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = tris;
        // mesh.uv = uvs;

        return mesh;
    }
}

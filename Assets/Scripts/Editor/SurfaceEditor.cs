using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Surface2DCreator))]
public class SurfaceEditorEditor : Editor {

    Surface2DCreator m_creator;

    void OnSceneGUI()
    {
        //if (m_creator.autoUpdate && Event.current.type == EventType.Repaint)
        //{
        //    m_creator.UpdateSurface();
        //}
    }

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		if (GUILayout.Button("Generate"))
		{
			m_creator.UpdateSurface();
		}
	}

	void OnEnable()
    {
        m_creator = (Surface2DCreator)target;
    }
}

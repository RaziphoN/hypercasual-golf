using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RoadCreator))]
public class RoadEditor : Editor {

    RoadCreator m_creator;

    void OnSceneGUI()
    {
        if (m_creator.autoUpdate && Event.current.type == EventType.Repaint)
        {
            m_creator.UpdateRoad();
        }
    }

    void OnEnable()
    {
        m_creator = (RoadCreator)target;
    }
}

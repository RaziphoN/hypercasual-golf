using UnityEditor;

using Scripts.UI;

namespace Scripts.Editors.UI
{
	[CustomEditor(typeof(LayeredLabel))]
	public class LayeredLabelEditor : Editor
	{
		private LayeredLabel m_target;

		private void OnEnable()
		{
			m_target = (LayeredLabel)target;
			m_target.Init();
		}

		public override void OnInspectorGUI()
		{
			EditorGUI.BeginChangeCheck();

			EditorGUILayout.HelpBox("Preffered width: " + (m_target.layer1.preferredWidth + (-m_target.offset.x) * 2).ToString()
								+ "\nPreffered height: " + (m_target.layer1.preferredHeight + m_target.offset.y * 2).ToString(), MessageType.Info);

			base.OnInspectorGUI();

			if (EditorGUI.EndChangeCheck())
			{
				m_target.Refresh();
			}
		}
	}
}

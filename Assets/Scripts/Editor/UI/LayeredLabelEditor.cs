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

			var size = m_target.GetPrefferedSize();
			EditorGUILayout.HelpBox("Preffered width: " + (size.x).ToString()
								+ "\nPreffered height: " + (size.y).ToString(), MessageType.Info);

			base.OnInspectorGUI();

			if (EditorGUI.EndChangeCheck())
			{
				m_target.Refresh();
			}
		}
	}
}

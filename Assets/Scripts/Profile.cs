using UnityEngine;

namespace Scripts
{
	public class Profile : MonoBehaviour
	{
		public delegate void IntValueChanged(int value);

		public event IntValueChanged onScoreChanged;
		public event IntValueChanged onStrokesChanged;

		public static Profile instance;


		public int score
		{
			get { return m_score; }
			set
			{
				if (m_score != value)
				{
					m_score = Mathf.Max(0, value);
					onScoreChanged?.Invoke(m_score);
				}
			}
		}

		public int strokes
		{
			get { return m_strokes; }
			set
			{
				if (m_strokes != value)
				{
					m_strokes = Mathf.Max(0, value);
					onStrokesChanged?.Invoke(m_strokes);
				}
			}
		}

		private int m_score = 0;
		private int m_strokes = 0;

		private void Awake()
		{
			instance = this;
		}

		private void OnDestroy()
		{

		}

		public void OnLevelComplete()
		{
			score = 0;
			strokes = 0;
		}
	}
}

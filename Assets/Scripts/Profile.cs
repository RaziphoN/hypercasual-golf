using UnityEngine;

public class Profile : MonoBehaviour
{
	public delegate void ScoreChanged(int score);
	public event ScoreChanged onScoreChanged;

	public static Profile instance;

	public int score = 0;

	private void Awake()
	{
		instance = this;
	}

	public void IncrementScore()
	{
		score++;

		if (onScoreChanged != null)
			onScoreChanged.Invoke(score);
	}
}

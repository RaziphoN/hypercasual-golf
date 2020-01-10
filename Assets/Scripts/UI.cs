using UnityEngine;
using UnityEngine.UI;

using Scripts.Framework.Utils;

public class UI : MonoBehaviour
{
	public static UI instance;

	private Text m_score;
	private Text m_strokes;
	private GameObject m_victory;

	private void Awake()
	{
		instance = this;

		m_score = Hierarchy.FindComponentInChildDeep<Text>(gameObject, "score_value");
		m_strokes = Hierarchy.FindComponentInChildDeep<Text>(gameObject, "strokes_value");
		m_victory = Hierarchy.FindChildDeep(gameObject, "victory");
	}

	private void Start()
	{
		Profile.instance.onScoreChanged += OnScoreChanged;
		Profile.instance.onStrokesChanged += OnStrokesChanged;

		ShowVictory(false);
	}

	private void OnDestroy()
	{
		Profile.instance.onScoreChanged -= OnScoreChanged;
		Profile.instance.onStrokesChanged -= OnStrokesChanged;
	}

	private void OnScoreChanged(int score)
	{
		m_score.text = score.ToString();
	}

	private void OnStrokesChanged(int strokes)
	{
		m_strokes.text = strokes.ToString();
	}

	public void ShowVictory(bool show)
	{
		m_victory.SetActive(show);

		m_strokes.gameObject.SetActive(!show);
		m_score.gameObject.SetActive(!show);
	}
}

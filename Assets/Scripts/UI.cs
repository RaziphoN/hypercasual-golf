using UnityEngine;
using UnityEngine.UI;

using Scripts.Framework.Utils;

public class UI : MonoBehaviour
{
	public static UI instance;

	private Text m_score;
	private GameObject m_victory;

	private void Awake()
	{
		instance = this;

		m_score = Hierarchy.FindComponentInChildDeep<Text>(gameObject, "value");
		m_victory = Hierarchy.FindChildDeep(gameObject, "victory");
	}

	private void Start()
	{
		Profile.instance.onScoreChanged += OnScoreChanged;

		ShowVictory(false);
	}

	private void OnDestroy()
	{
		Profile.instance.onScoreChanged -= OnScoreChanged;
	}

	private void OnScoreChanged(int score)
	{
		m_score.text = score.ToString();
	}

	public void ShowVictory(bool show)
	{
		m_victory.SetActive(show);
		m_score.gameObject.SetActive(!show);
	}
}

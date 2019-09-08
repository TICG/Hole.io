using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
	public GameObject mainMenu;
	public TextMeshProUGUI bestText;

	public GameObject gameplay;
	public TextMeshProUGUI scoreText;
	public TextMeshProUGUI timerText;

	public GameObject gameOver;
	public TextMeshProUGUI finalScoreText;
	public TextMeshProUGUI finalBestText;

	private void Awake()
	{
		GameManager.gm.ui = this;
	}

	private void Start()
	{
		mainMenu.SetActive(true);

		var best = PlayerPrefs.GetInt("best");
		if (best > 0)
		{
			bestText.text = "BEST " + best.ToString();
		}
		else
		{
			bestText.gameObject.SetActive(false);
		}
	}

	public void StartGame()
	{
		mainMenu.SetActive(false);
		gameplay.SetActive(true);
		GameManager.gm.Play();
	}

	public void Continue()
	{
		GameManager.gm.started = false;
		GameManager.gm.gameOver = false;
		GameManager.gm.RestartScene();
	}
}

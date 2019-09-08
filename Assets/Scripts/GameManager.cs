using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager gm;
	public PlayerControler pc;
	public CameraController cam;
	public MenuManager ui;

	public bool started;
	public bool gameOver;

	public float gravity = 10f;
	public float time = 120; // Total game time in seconds

	private void Awake()
	{
		if (!gm)
		{
			gm = this;
			DontDestroyOnLoad(this);
		}
	}

	public void Play()
	{
		started = true;
	}

	public void GameOver()
	{
		gameOver = true;
		ui.gameplay.SetActive(false);
		ui.gameOver.SetActive(true);
		ui.finalScoreText.text = pc.score.ToString();
		ui.finalBestText.gameObject.SetActive(false);

		var curBest = PlayerPrefs.GetInt("best");
		if (pc.score > curBest)
		{
			PlayerPrefs.SetInt("best", pc.score);
			ui.finalBestText.gameObject.SetActive(true);
		}		
	}

	private void Update()
	{
		if (Input.GetKey(KeyCode.R))
		{
			RestartScene();
		}

		if (!started || gameOver)
		{
			return;
		}

		time -= Time.deltaTime;

		if (time <= 1)
		{
			GameOver();
			ui.timerText.text = "00:00";
			return;
		}

		var minutes = Mathf.FloorToInt(time / 60f);
		var seconds = Mathf.FloorToInt(time - minutes * 60f);
		ui.timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
	}

	public void RestartScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
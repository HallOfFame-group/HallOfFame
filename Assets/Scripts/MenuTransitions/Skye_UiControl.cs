using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Skye_UiControl : MonoBehaviour 
{
	
	//public GameObject timeIsup, restartButton;
	GameObject gameOverPanel;
	//Animator anim;
	// Use this for initialization
	void Start () {
		gameOverPanel = transform.Find ("GameOverPanel").gameObject;
		gameOverPanel.SetActive (false);
		//anim = GetComponent<Animator> ();
		//timeIsup.gameObject.SetActive (false);
		//restartButton.gameObject.SetActive (false);
		//ScreenFader.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Skye_TimeLeft.timeLeft <= 0) 
		{
			//Time.timeScale = 0;
		//	timeIsup.gameObject.SetActive (true);
		//	restartButton.gameObject.SetActive (true);
			gameOverPanel.SetActive(true);
		}
	}

	public void restartScene()
	{
		//timeIsup.gameObject.SetActive (false);
		//restartButton.gameObject.SetActive (false);
		Time.timeScale = 1;
		Skye_TimeLeft.timeLeft = 10f;
		SceneManager.LoadScene("Timer");
	}
}

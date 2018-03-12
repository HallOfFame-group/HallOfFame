using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UiControl : MonoBehaviour
{
    GameObject gameOverPanel;
    GameObject startMenu;

    void Start() {
        gameOverPanel = transform.Find("GameOverPanel").gameObject;
        startMenu = transform.Find("StartMenu").gameObject;
        gameOverPanel.SetActive(false);
      
    }

    void Update()
    {
        if (TimeLeft.timeLeft <= 0)
        {
            gameOverPanel.SetActive(true);
        }
       
    }

	


}

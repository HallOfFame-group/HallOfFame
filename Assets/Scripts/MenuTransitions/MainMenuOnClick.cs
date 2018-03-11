using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuOnClick : MonoBehaviour {

    GameObject gameOverPanel;
    GameObject startMenu;

    public void LoadMainMenu()
    {
        gameOverPanel = transform.Find("GameOverPanel").gameObject;
        startMenu = transform.Find("StartMenu").gameObject;
        startMenu.gameObject.SetActive(true);
        gameOverPanel.gameObject.SetActive(false);
    }

}

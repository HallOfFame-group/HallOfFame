using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RestartOnClick : MonoBehaviour
{
    int loadedLevel;
    private void Awake()
    {
        loadedLevel = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(loadedLevel);
    }
    public void RestartScene()
    {
        SceneManager.LoadScene(2);
    }
}

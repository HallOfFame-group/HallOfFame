using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RestartOnClick : MonoBehaviour {
    Scene loadedLevel = SceneManager.GetActiveScene();
    public void RerestartScene(Scene loadedLevel)
    {
        
        SceneManager.LoadScene(loadedLevel.buildIndex);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {

    //public void LoadByIndex(int sceneIndex)
     public void LoadByClick(string level)
    {
        //SceneManager.LoadScene (sceneIndex);
        SceneManager.LoadScene(level);
    }
}

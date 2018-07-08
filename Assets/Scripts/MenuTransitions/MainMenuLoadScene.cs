using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

<<<<<<< HEAD
public class MainMenuLoadScene : MonoBehaviour {
    public int sceneIndex;
=======
public class MainMenuLoadScene : MonoBehaviour
{
    public SceneField nextScene;
>>>>>>> 132ba892fc5e2590101ff943f4aa5fee5cb380c3

	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown)
        {
<<<<<<< HEAD
            SceneManager.LoadScene(sceneIndex);
=======
            SceneManager.LoadScene(nextScene);
>>>>>>> 132ba892fc5e2590101ff943f4aa5fee5cb380c3
        }
	}
}

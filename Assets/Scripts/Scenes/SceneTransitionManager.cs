using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    [SerializeField]
    private SceneField nextScene;
    private bool proceedNextScene;

    AsyncOperation loadSceneAsyncOp;

    private static SceneTransitionManager sceneTransitionManager;
    public static SceneTransitionManager instance
    {
        get
        {
            if (!sceneTransitionManager)
            {
                sceneTransitionManager = FindObjectOfType<SceneTransitionManager>();
                if (!sceneTransitionManager)
                {
                    Debug.LogError("Scene Transition Manager must be attached to a script in scene");
                }
            }
            return sceneTransitionManager;
        }
    }

    private void Start()
    {
        if (!sceneTransitionManager)
        {
            instance.Init();
            DontDestroyOnLoad(this);

            loadSceneAsyncOp = SceneManager.LoadSceneAsync(nextScene);
            loadSceneAsyncOp.allowSceneActivation = false;
            StartCoroutine(LoadNextScene());
        }
        else
        {
            if (sceneTransitionManager != this)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Init()
    {
    }

    IEnumerator LoadNextScene()
    {
        while(!loadSceneAsyncOp.isDone)
        {
            yield return null;
        }
    }

    public void Proceed()
    {
        loadSceneAsyncOp.allowSceneActivation = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public enum ESelectedCharacter
    {
        Tchaikovsky,
        Beethovan,
        Berlioz,
        Dvorak,
        Chopin,
        Mozart,
        Vivaldi
    }

    [SerializeField]
    private SceneField nextScene;
    private bool proceedNextScene;

    public ESelectedCharacter[] selectedCharacter;
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
                else
                {
                    sceneTransitionManager.Init();
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
        selectedCharacter = new ESelectedCharacter[MultiplayerSelection.instance.GetTotalNumberOfPlayers()];
        for (int i = 0; i < selectedCharacter.Length; ++i)
        {
            // Need to explicitly + 1 because MultiplayerSelection auto decrements the input
            // Need to keep this way because the way specified in inspector
            selectedCharacter[i] = (ESelectedCharacter)(MultiplayerSelection.instance.GetCurrentSelectedIndex(i + 1));
        }
    }
}

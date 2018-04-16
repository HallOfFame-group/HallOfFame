using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct RhythmResult
{
    public int perfectCount;
    public int goodCount;
    public int badCount;
    public int missCount;
};

public class RhythmCombo : MonoBehaviour
{
    #region Private Members
    // Singleton reference to rhythm combo script
    private static RhythmCombo rhythmCombo;

    // RhythmCombo component reference
    private GameObject panel;
    private NodeSpawner nodeSpawner;
    private BeatLine beatline;

    // GUI reference to node press result
    private MusicTitleUIControl musicUI;

    // Contains combo result obtained from beat line object
    public RhythmResult comboResult;

    private bool spawnFinishedFlag = false;

    private ComboPiece currentPlayingPiece;
    #endregion

    #region Public Members
    public static RhythmCombo instance
    {
        get
        {
            if (!rhythmCombo)
            {
                rhythmCombo = FindObjectOfType(typeof(RhythmCombo)) as RhythmCombo;

                // If rhythmCombo still cannot be found, raise error
                if (!rhythmCombo)
                {
                    Debug.LogError("There needs to be one active RhythmCombo script on a GameObject in the scene.");
                }
                else
                {
                    // Initialize rhythmCombo
                    rhythmCombo.Init();
                }
            }

            return rhythmCombo;
        }
    }

    // Using delegate as function callback method
    // This will be called when rhythm combo has finished, and returns the result to caller
    public delegate void RhythmComboCallback();
    public delegate void RHythmComboOnNodeHit(NodePressResult result);
    public RhythmComboCallback finishedEventCallback;
    public RHythmComboOnNodeHit nodeEventCallback;

    #endregion

    #region Non-Public Methods
    private void Awake()
    {
        panel = transform.Find("Panel").gameObject;
        nodeSpawner = FindObjectOfType<NodeSpawner>();
        beatline = FindObjectOfType<BeatLine>();
        nodeSpawner.EndlinePosition(beatline.gameObject.transform.position);

        beatline.callbackFunc = NodeProcessed;
        nodeSpawner.callbackFunc = SpawnFinished;

        // Obtain visual feed back elements for node pressed event
        musicUI = FindObjectOfType<MusicTitleUIControl>();

        // By default, hides the UI elements
        Activate(false);
    }

    protected void Init()
    {
    }

    // Set Rhythm combo active or not
    private void Activate(bool active)
    {
        panel.SetActive(active);
        musicUI.gameObject.SetActive(active);
    }

    // Handling NodeSpawner callback, marks the node spawner has finished spawning process, awaiting for beatline
    private void SpawnFinished()
    {
        Debug.Log("Spawn Finished");
        spawnFinishedFlag = true;
    }

    // Handling BeatLine callback, gets called everytime a node is processed
    private void NodeProcessed(NodePressResult result)
    {
        // Disable all visual UI, then display the correct one
        nodeEventCallback(result);

        // Only care when the spawning process is finished as well
        // Invoke callback function when beatline has processed all spawned nodes
        if (spawnFinishedFlag && beatline.nodeCount == nodeSpawner.spawnCount)
        {
            instance.comboResult = beatline.rhythmResult;
            finishedEventCallback();
        }
    }

    #endregion

    #region Public Methods
    public void Test()
    {
    }

    /// <summary>
    /// Register rhythm combo display information using ComboPiece script
    /// </summary>
    /// <param name="combo"></param>
    public void Register(ComboPiece combo)
    {
        currentPlayingPiece = combo;
        //title.text = combo.musicName + " - " + combo.artistName;
        nodeSpawner.PrepareNodes(combo.timeNodeArray);
        spawnFinishedFlag = false;
    }

    /// <summary>
    /// Display the rhythm combo on screen
    /// </summary>
    public void Display(int playerNum)
    {
        Activate(true);
        beatline.RegisterPlayerNum(playerNum);
        nodeSpawner.StartSpawning(currentPlayingPiece.musicPathReference);
    }

    /// <summary>
    /// End the rhythmcombo
    /// </summary>
    public void End()
    {
        Activate(false);
    }
    #endregion
}

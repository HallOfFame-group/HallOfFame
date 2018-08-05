using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
public struct RhythmResult
{
    public int perfectCount;
    public int goodCount;
    public int badCount;
    public int missCount;
};
*/
[System.Serializable]
public struct RhythmComboBannerEntry
{
    public SceneTransitionManager.ESelectedCharacter CharacterName;
    public Image CharacterBanner;
}

[System.Serializable]
public struct RhythmComboBannerMapping
{
    public RhythmComboBannerEntry[] entries;
}

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
    // private MusicTitleUIControl musicUI;

    // Contains combo result obtained from beat line object
    public RhythmResult comboResult;

    private bool spawnFinishedFlag = false;

    private ComboPiece currentPlayingPiece;

    private int playerNum = 0;

    [SerializeField]
    private RhythmComboBannerMapping[] bannerMapping;
    private ShowBanner playedBannerImage;

    private static bool hasActivated = false;
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

        playedBannerImage = FindObjectOfType<ShowBanner>();

        // Obtain visual feed back elements for node pressed event
        // musicUI = FindObjectOfType<MusicTitleUIControl>();

        // By default, hides the UI elements
        Activate(false);
    }

    protected void Init()
    {
        foreach(RhythmComboBannerMapping rcbm in bannerMapping)
        {
            foreach(RhythmComboBannerEntry entry in rcbm.entries)
            {
                entry.CharacterBanner.gameObject.GetComponent<BannerImageAnimationHandler>().EvtOnBannerImageAnmiationEnd += BannerAnimFinishedHandler;
            }
        }
    }

    // Set Rhythm combo active or not
    private void Activate(bool active)
    {
        panel.SetActive(active);
        GetComponent<Animator>().enabled = active;
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

            hasActivated = false;
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
        Debug.Log(combo);
        if (!hasActivated)
        {
            currentPlayingPiece = combo;
            //title.text = combo.musicName + " - " + combo.artistName;
            nodeSpawner.PrepareNodes(combo.timeNodeArray);
            spawnFinishedFlag = false;

            hasActivated = true;
        }
    }

    /// <summary>
    /// Display the rhythm combo on screen
    /// </summary>
    public void Display(int playerNum)
    {
        --playerNum;
        foreach (RhythmComboBannerEntry entry in bannerMapping[playerNum].entries)
        {
            SceneTransitionManager.ESelectedCharacter c =  SceneTransitionManager.instance.selectedCharacter[playerNum];
            if (entry.CharacterName == c)
            {
                // Play corresponding animation mapped to this entry
                break;
            }
        }
        this.playerNum = playerNum;
    }

    public void Display(int playerNum, SceneTransitionManager.ESelectedCharacter c)
    {
        if (playerNum == 1)
        {
            if (c == SceneTransitionManager.ESelectedCharacter.Beethovan)
            {
                playedBannerImage.Show(ShowBanner.EBannerToShown.ShowLeftBeethoven);
            }
            else
            {
                playedBannerImage.Show(ShowBanner.EBannerToShown.ShowLeftMozart);
            }
        }
        else
        {
            if (c == SceneTransitionManager.ESelectedCharacter.Beethovan)
            {
                playedBannerImage.Show(ShowBanner.EBannerToShown.ShowRightBeethoven);
            }
            else
            {
                playedBannerImage.Show(ShowBanner.EBannerToShown.ShowRightMozart);
            }
        }
        //Activate(true);
        this.playerNum = --playerNum;
    }

    public void BannerAnimFinishedHandler()
    {
        Activate(true);
    }

    public void RolloutAnimFinshedHandler()
    {
        Debug.Log(nodeSpawner);
        Debug.Log(currentPlayingPiece);
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

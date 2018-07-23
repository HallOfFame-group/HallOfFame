using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct RhythmResult
{
    public int perfectCount;
    public int goodCount;
    public int badCount;
    public int missCount;
};

public class RhythmComboUI : MonoBehaviour
{
    // Rhythm combo callback event
    public delegate void OnRhythmComboCompleted();
    public event OnRhythmComboCompleted EvtOnRhythmComboCompleted;

    // Rhythm combo onhit callback event
    public delegate void OnRhythmComboHit();
    public event OnRhythmComboHit EvtOnRhythmComboHit;

    // Rhythm combo on miss callback event
    public delegate void OnRhythmComboMiss();
    public event OnRhythmComboMiss EvtOnRhythmComboMiss;

    private static RhythmComboUI rhythmComboUI;
    public static RhythmComboUI instance
    {
        get
        {
            if (!rhythmComboUI)
            {
                rhythmComboUI = FindObjectOfType<RhythmComboUI>();
                if (!rhythmComboUI)
                {
                    Debug.LogError("RhythmCombo must be attached to an UI Canvas in scene");
                }
                else
                {
                    // Initialize RhythmCombo UI version
                    rhythmComboUI.Init();
                }
            }

            return rhythmComboUI;
        }
    }

    private ComboPiece preparedComboPiece;

    private NodeSpawnerUI nodeSpawner;
    private BeatlineUI beatline;

    private bool ifNodeSpawnedFinished = false;

    private void Start()
    {
        if (rhythmComboUI && rhythmComboUI != this)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnEnable()
    {
        if (nodeSpawner && beatline)
        {
            nodeSpawner.EvtOnNodeSpawnedFinished += OnNodeSpawnedFinishedHandler;
            beatline.EvtOnBeatlineProcessedANode += OnBeatlineProcessedANodeHandler;
        }
    }

    private void OnDisable()
    {
        if (nodeSpawner && beatline)
        {
            nodeSpawner.EvtOnNodeSpawnedFinished -= OnNodeSpawnedFinishedHandler;
            beatline.EvtOnBeatlineProcessedANode -= OnBeatlineProcessedANodeHandler;
        }
    }

    private void Init()
    {
        // Initalize
        nodeSpawner = transform.GetComponentInChildren<NodeSpawnerUI>();
        beatline = transform.GetComponentInChildren<BeatlineUI>();
    }

    public void Prepare(ComboPiece comboPiece)
    {
        preparedComboPiece = comboPiece;
        nodeSpawner.Prepare(preparedComboPiece.timeNodeArray);
    }

    public void Activate()
    {
        nodeSpawner.Spawn();
        ifNodeSpawnedFinished = false;
    }

    public void Stop()
    {

    }

    private void OnNodeSpawnedFinishedHandler()
    {
        ifNodeSpawnedFinished = true;
    }

    private void OnBeatlineProcessedANodeHandler(NodePressResult nodeBtn)
    {

    }

    // Animation event handler
    public void OnAnimationComplete()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    //private BeatLineUI beatline;

    private void Start()
    {
        if (rhythmComboUI && rhythmComboUI != this)
        {
            Destroy(this.gameObject);
        }
    }

    private void Init()
    {
        // Initalize
        nodeSpawner = transform.GetComponentInChildren<NodeSpawnerUI>();
    }

    public void Prepare(ComboPiece comboPiece)
    {
        preparedComboPiece = comboPiece;
        nodeSpawner.Prepare(preparedComboPiece.timeNodeArray);
    }

    public void Activate()
    {
        nodeSpawner.Spawn();
    }

    public void Stop()
    {

    }

    // Animation event handler
    public void OnAnimationComplete()
    {

    }
}

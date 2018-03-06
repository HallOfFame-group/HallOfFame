using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeRhythmCombo : MonoBehaviour
{
    private void Start()
    {
        RhythmCombo.instance.Test();
        RhythmCombo.instance.Register(this.GetComponent<ComboPiece>());
        RhythmCombo.instance.Display();
        RhythmCombo.instance.nodeEventCallback = OnNodeHit;
        RhythmCombo.instance.finishedEventCallback = finished;
    }

    void OnNodeHit(NodePressResult result)
    {
        switch (result)
        {
            case NodePressResult.PERFECT:
            case NodePressResult.GOOD:
                CrowdBar.instance.IncreaseToPlayer1(30);
                break;
            case NodePressResult.BAD:
            case NodePressResult.MISS:
            default:
                CrowdBar.instance.IncreaseToPlayer2(30);
                break;
        }

    }


    void finished()
    {
        Debug.Log("Finished");
        Debug.Log(RhythmCombo.instance.comboResult.perfectCount);
        Debug.Log(RhythmCombo.instance.comboResult.goodCount);
        Debug.Log(RhythmCombo.instance.comboResult.badCount);
        Debug.Log(RhythmCombo.instance.comboResult.missCount);
        CrowdBar.instance.IncreaseToPlayer1(30);
    }
}

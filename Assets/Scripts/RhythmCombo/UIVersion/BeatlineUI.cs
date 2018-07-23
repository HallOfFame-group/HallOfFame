using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatlineUI : MonoBehaviour
{
    public delegate void OnBeatlineProcessedANode(NodePressResult result);
    public event OnBeatlineProcessedANode EvtOnBeatlineProcessedANode;

    private NodeSpawnerUI nodeSpawner;
    private int playerNumber;

    [SerializeField]
    private float PerfectProgressThreshold = 0.9f;

    [SerializeField]
    private float GoodProgressThreshold = 0.7f;

    [SerializeField]
    private float BadProgressThreshold = 0.4f;

    public RhythmResult rhythmResult;
    public int processedCount = 0;

    private void Start()
    {
        nodeSpawner = FindObjectOfType<NodeSpawnerUI>();
    }

    private void Update()
    {
        bool key1 = Input.GetButtonDown("360Controller0_RhythmKeyA") || Input.GetButtonDown("360Controller" + playerNumber + "_RhythmKeyA");
        bool key2 = Input.GetButtonDown("360Controller0_RhythmKeyB") || Input.GetButtonDown("360Controller" + playerNumber + "_RhythmKeyB");
        bool key3 = Input.GetButtonDown("360Controller0_RhythmKeyY") || Input.GetButtonDown("360Controller" + playerNumber + "_RhythmKeyY");

        if ((key1 || key2 || key3))
        {
            GameObject node = nodeSpawner.GetNode();

            // Check if node is valid
            if (node)
            {
                NodeUI nodeUI = node.GetComponent<NodeUI>();

                NodePressResult result = NodePressResult.MISS;
                if ((nodeUI.keyCode == NodeButton.A && key1 && !key2 && !key3) ||
                (nodeUI.keyCode == NodeButton.B && !key1 && key2 && !key3) ||
                (nodeUI.keyCode == NodeButton.Y && !key1 && !key2 && key3))
                {
                    if (nodeUI.GetProgress() > PerfectProgressThreshold)
                    {
                        ++rhythmResult.perfectCount;
                        result = NodePressResult.PERFECT;
                    }
                    else if (nodeUI.GetProgress() > GoodProgressThreshold)
                    {
                        ++rhythmResult.goodCount;
                        result = NodePressResult.GOOD;
                    }
                    else if (nodeUI.GetProgress() > BadProgressThreshold)
                    {
                        ++rhythmResult.badCount;
                        result = NodePressResult.BAD;
                    }
                    else
                    {
                        ++rhythmResult.missCount;
                    }
                }

                ++processedCount;
                EvtOnBeatlineProcessedANode(result);
            }
        }
    }
}

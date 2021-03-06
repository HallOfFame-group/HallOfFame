﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodePressResult
{
    PERFECT = 0,
    GOOD,
    BAD,
    MISS
};

public class BeatLine : MonoBehaviour
{
    #region Private Members
    // Using a list of game objects to track the nodes enter/exist the detection region
    private List<GameObject> nodeList;

    [SerializeField] private float perfectAllowanceRange = 1;
    [SerializeField] private float goodAllowanceRange = 2;
    [SerializeField] private float badAllowanceRange = 3;

    private int playerNum = 0;

    private NodeResultTextUIControl nodeResultUI;
    #endregion

    #region Public Members
    // Callback function for when every time a node has been processed
    public delegate void BeatLineCallback(NodePressResult result);

    public BeatLineCallback callbackFunc;

    // Counter for total amount of nodes registered
    public int nodeCount;

    // Rhythm result from pressing the button
    public RhythmResult rhythmResult;
    #endregion

    #region Private Methods

    // Initializes the members on awake
    private void Awake()
    {
        nodeList = new List<GameObject>();
        rhythmResult = new RhythmResult();
        nodeCount = 0;

        nodeResultUI = FindObjectOfType<NodeResultTextUIControl>();
    }

    private void OnEnable()
    {
        nodeResultUI.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        nodeResultUI.gameObject.SetActive(false);
    }

    // When a node enters the detection region, register it to the list
    private void OnTriggerEnter2D(Collider2D collision)
    {
        nodeList.Add(collision.transform.parent.gameObject);
    }

    // When a node leaves the detection region, destroy it, mark as miss, and increase total count
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Remoe from list
        nodeList.Remove(collision.transform.parent.gameObject);
        Destroy(collision.transform.parent.gameObject);

        // Count as miss
        ++rhythmResult.missCount;

        // Increment total count and calls to caller
        ++nodeCount;

        nodeResultUI.Display(NodePressResult.MISS);
        callbackFunc(NodePressResult.MISS);
    }

    private void Update()
    {
        // Right now just using space bar for detection        
        bool key1 = Input.GetButtonDown("360Controller0_RhythmKeyA") || Input.GetButtonDown("360Controller" + playerNum + "_RhythmKeyA");
        bool key2 = Input.GetButtonDown("360Controller0_RhythmKeyB") || Input.GetButtonDown("360Controller" + playerNum + "_RhythmKeyB");
        bool key3 = Input.GetButtonDown("360Controller0_RhythmKeyY") || Input.GetButtonDown("360Controller" + playerNum + "_RhythmKeyY");

        if ((key1 || key2 || key3) && nodeList.Count > 0)
        {
            NodePressResult result = NodePressResult.MISS;

            // Check if the key pressed equals the first node in list
            // Using debounce to prevent player smashing all keys
            BeatNode beatNode = nodeList[0].GetComponent<BeatNode>();
            if ((beatNode.keyCode == NodeButton.A && key1 && !key2 && !key3) ||
                (beatNode.keyCode == NodeButton.B && !key1 && key2 && !key3) ||
                (beatNode.keyCode == NodeButton.Y && !key1 && !key2 && key3))
            {
                // Using the distance from the center to determine the score
                if (Mathf.Abs(nodeList[0].transform.position.x - this.transform.position.x) < perfectAllowanceRange)
                {
                    result = NodePressResult.PERFECT;
                    ++rhythmResult.perfectCount;
                }
                else if (Mathf.Abs(nodeList[0].transform.position.x - transform.position.x) < goodAllowanceRange)
                {
                    result = NodePressResult.GOOD;
                    ++rhythmResult.goodCount;
                }
                else if (Mathf.Abs(nodeList[0].transform.position.x - transform.position.x) < badAllowanceRange)
                {
                    result = NodePressResult.BAD;
                    ++rhythmResult.badCount;
                }
                else
                {
                    result = NodePressResult.MISS;
                    ++rhythmResult.missCount;
                }
            }
            else
            {
                result = NodePressResult.MISS;
                ++rhythmResult.missCount;
            }

            // Remove the processed node from the list
            GameObject obj = nodeList[0];
            nodeList.RemoveAt(0);
            DestroyImmediate(obj);

            // Increase the total count
            ++nodeCount;

            nodeResultUI.Display(result);

            // Notify the caller a node has been processed
            callbackFunc(result);
        }
    }
    #endregion

    #region Public Methods
    public void RegisterPlayerNum(int playerNum)
    {
        this.playerNum = playerNum;
    }
    #endregion
}

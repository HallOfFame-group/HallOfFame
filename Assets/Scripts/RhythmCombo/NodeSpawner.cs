﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSpawner : MonoBehaviour
{
    #region Public Members
    public delegate void NodeSpawnerCallback();

    // Total spawned count
    public int spawnCount;

    // Callback function registed
    public NodeSpawnerCallback callbackFunc;
    #endregion

    #region Private Members
    // BeatNode prefab object
    [SerializeField] private GameObject beatNode;

    // Boolean indicate start spawning according to the TimeNode registered
    private bool spawning;

    // Timer variable for spawning node as TimeNode array indicated
    private float elapsedTime;

    // Distance from spawner to endline
    private float endlineDistance = 0;

    // TimeNode array from ComboPiece registered
    private TimedNode[] timeNodes;

    private float baseTime;

    // Node traveling speed in seconds
    [Range(0.1f, 2.0f)]
    [SerializeField] private float travelSpeed = 1.5f;

    private GameObject[] spawnLine;

    AudioSource audioSource;
    #endregion

    #region Private Methods
    private void ResetNodeSpawner()
    {
        spawning = false;
        elapsedTime = -travelSpeed;
    }

    private void Awake()
    {
        ResetNodeSpawner();

        // Obtain different spawn line
        spawnLine = new GameObject[3];
        spawnLine[0] = transform.Find("TopLine").gameObject;
        spawnLine[1] = transform.Find("MidLine").gameObject;
        spawnLine[2] = transform.Find("BotLine").gameObject;

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Start spawning beat nodes if enabled
        if (spawning)
        {
            // Increments the elasped timer to keep track of when to spawn node
            elapsedTime += Time.deltaTime;

            if (timeNodes[spawnCount].timeStamp - travelSpeed <= elapsedTime)
            {
                // Instantiate beat node
                BeatNode node = Instantiate(beatNode, spawnLine[(int)timeNodes[spawnCount].nodeButton].transform).GetComponent<BeatNode>();
                node.keyCode = timeNodes[spawnCount].nodeButton;
                node.StartNode(endlineDistance, travelSpeed);

                // Increment spawn count
                ++spawnCount;
            }

            // When finished spawning, stop the process
            if (spawnCount >= timeNodes.Length)
            {
                spawning = false;
                callbackFunc();
            }
        }
    }

    private IEnumerator DelayAudioSource()
    {
        yield return new WaitForSeconds(travelSpeed);
    }
    #endregion

    #region Public Methods

    public void PrepareNodes(TimedNode[] timeNodes)
    {
        this.timeNodes = timeNodes;
        ResetNodeSpawner();
    }

    public void StartSpawning()
    {
        spawning = true;
        spawnCount = 0;
        StartCoroutine(DelayAudioSource());
    }

    public void EndlinePosition(Vector3 endline)
    {
        endlineDistance = Vector3.Distance(this.gameObject.transform.position, endline);
    }

    #endregion
}

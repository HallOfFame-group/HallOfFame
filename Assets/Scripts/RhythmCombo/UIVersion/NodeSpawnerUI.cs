using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Node
{
    public NodeButton buttonId;
    public Sprite buttonSprite;
}

public class NodeSpawnerUI : MonoBehaviour
{
    public delegate void OnNodeSpawnedFinished();
    public event OnNodeSpawnedFinished EvtOnNodeSpawnedFinished;

    private bool isSpawning = false;
    private float elapsedTime = 0.0f;

    [SerializeField]
    private GameObject spawnNode;

    [SerializeField]
    private Node[] spawnNodeSetting;

    private Queue<GameObject> spawnedNode;

    private TimedNode[] preparedNodeTimeline;
    private int spawnCount = 0;
    private BeatlineUI beatlineUI;
    private float distance;

    private void Start()
    {
        beatlineUI = FindObjectOfType<BeatlineUI>();
        distance = Vector3.Distance(transform.position, beatlineUI.transform.position);
        spawnedNode = new Queue<GameObject>();
    }

    private void Update()
    {
        if (isSpawning)
        {
            elapsedTime += Time.deltaTime;

            if (preparedNodeTimeline[spawnCount].timeStamp - 1 <= elapsedTime)
            {
                // Spawn a UI image at current position
                // Change the UI image sprite depends on the buttonId
                // Increment spawn count
            }

            // Check if spawn has finished, and calls the event
        }
    }

    public void Prepare(TimedNode[] nodeTimeline)
    {
        preparedNodeTimeline = nodeTimeline;
    }

    public void Spawn()
    {
        isSpawning = true;
        SpawnNode(spawnNodeSetting[0].buttonSprite);
    }

    private void SpawnNode(Sprite sprite)
    {
        GameObject go = Instantiate(spawnNode, this.transform);
        go.GetComponent<Image>().sprite = sprite;
        go.GetComponent<NodeUI>().Prepare(distance);
        go.GetComponent<NodeUI>().Go();
        spawnedNode.Enqueue(go);
    }

    public GameObject GetNode()
    {
        if (spawnedNode.Count != 0)
        {
            return spawnedNode.Dequeue();
        }
        else
        {
            return null;
        }
    }
}

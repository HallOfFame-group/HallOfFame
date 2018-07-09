using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatlineUI : MonoBehaviour
{
    private NodeSpawnerUI nodeSpawner;
    private int playerNumber;

    private void Start()
    {
        nodeSpawner = FindObjectOfType<NodeSpawnerUI>();
    }

    private void Update()
    {
        bool isAPressed = Input.GetButtonDown("360Controller0_Button_A") || Input.GetButtonDown("360Controller" + playerNumber + "_Button_A");
        if (isAPressed)
        {
            NodeUI node = nodeSpawner.GetNode().GetComponent<NodeUI>();
            Debug.Log(node.GetProgress());
        }
    }
}

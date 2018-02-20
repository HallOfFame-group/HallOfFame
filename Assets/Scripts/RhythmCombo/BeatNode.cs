﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeButton
{
    Key1,
    Key2
}

public class BeatNode : MonoBehaviour
{
    public NodeButton keyCode;

    // Lerping time interval, determines the speed of lerping
    private float timeInterval;

    // Final destination relative to spawn point
    Vector3 destination;

    // Flag controlling lerping motion state
    private bool start = false;

    [SerializeField] private Sprite key1;
    [SerializeField] private Sprite key2;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void StartNode(float distance, float travelSpeed)
    {
        destination = this.transform.position + Vector3.right * distance;
        timeInterval = distance / travelSpeed * Time.fixedDeltaTime;
        if (keyCode == NodeButton.Key1)
        {
            spriteRenderer.sprite = key1;
        }
        else
        {
            spriteRenderer.sprite = key2;
        }
        start = true;
    }

    // Update is called once per frame
    private void FixedUpdate ()
    {
        if (start)
        {
            this.transform.position = Vector2.Lerp(transform.position, transform.position + Vector3.right, timeInterval);
        }
    }
}

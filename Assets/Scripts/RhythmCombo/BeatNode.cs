﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeButton
{
    A,
    B,
    Y
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

    [SerializeField] private Sprite btnA;
    [SerializeField] private Sprite btnB;
    [SerializeField] private Sprite btnY;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void StartNode(float distance, float travelSpeed)
    {
        destination = this.transform.position + Vector3.right * distance;
        timeInterval = distance / travelSpeed * Time.fixedDeltaTime;
        if (keyCode == NodeButton.A)
        {
            spriteRenderer.sprite = btnA;
        }
        else if (keyCode == NodeButton.B)
        {
            spriteRenderer.sprite = btnB;
        }
        else
        {
            spriteRenderer.sprite = btnY;
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

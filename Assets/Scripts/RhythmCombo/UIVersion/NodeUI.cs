using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeButton
{
    A,
    B,
    Y
}

public class NodeUI : MonoBehaviour
{
    [SerializeField]
    private float completeInSeconds = 1.5f;

    private float travelSpeed;

    private bool isTravelling;

    private float progress = 0;

    private float distance = 0;

    public NodeButton keyCode;

    private void Update()
    {
        if (isTravelling)
        {
            transform.position += new Vector3(travelSpeed * Time.deltaTime, 0, 0);
            progress += travelSpeed * Time.deltaTime;
        }
    }

    public void Prepare(float distance, NodeButton key)
    {
        travelSpeed = distance / completeInSeconds;
        this.distance = distance;
        keyCode = key;
    }

    public void Go()
    {
        isTravelling = true;
    }

    public float GetProgress()
    {
        return progress / distance;
    }
}

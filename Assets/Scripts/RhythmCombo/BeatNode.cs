using System.Collections;
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

    [SerializeField] private GameObject btnA;
    [SerializeField] private GameObject btnB;
    [SerializeField] private GameObject btnY;

    public void StartNode(float distance, float travelSpeed)
    {
        destination = this.transform.position + Vector3.right * distance;
        timeInterval = distance / travelSpeed * Time.fixedDeltaTime;
        if (keyCode == NodeButton.A)
        {
            Instantiate(btnA, this.transform);
        }
        else if (keyCode == NodeButton.B)
        {
            Instantiate(btnB, this.transform);
        }
        else
        {
            Instantiate(btnY, this.transform);
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

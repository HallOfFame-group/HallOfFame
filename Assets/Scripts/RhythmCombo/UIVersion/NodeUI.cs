using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeUI : MonoBehaviour
{
    [SerializeField]
    private float completeInSeconds = 1.5f;

    private float travelSpeed;

    private bool isTravelling;

    private void Update()
    {
        if (isTravelling)
        {
            GetComponent<RectTransform>().anchoredPosition += new Vector2(travelSpeed, 0);
        }
    }

    public void Prepare(float distance)
    {
        travelSpeed = distance / completeInSeconds;
    }

    public void Go()
    {
        isTravelling = true;
    }
}

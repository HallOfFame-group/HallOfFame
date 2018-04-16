using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeResultTextUIControl : MonoBehaviour
{
    private Dictionary<NodePressResult, GameObject> nodeText;

    private void Awake()
    {
        nodeText = new Dictionary<NodePressResult, GameObject>();
        nodeText.Add(NodePressResult.PERFECT, transform.Find("PerfectText").gameObject);
        nodeText.Add(NodePressResult.GOOD, transform.Find("GoodText").gameObject);
        nodeText.Add(NodePressResult.BAD, transform.Find("BadText").gameObject);
        nodeText.Add(NodePressResult.MISS, transform.Find("MissText").gameObject);

        foreach(KeyValuePair<NodePressResult, GameObject> entry in nodeText)
        {
            entry.Value.SetActive(false);
        }
    }

    public void Display(NodePressResult result)
    {
        foreach(KeyValuePair<NodePressResult, GameObject> entry in nodeText)
        {
            entry.Value.SetActive(false);
        }

        nodeText[result].SetActive(true);
    }
}

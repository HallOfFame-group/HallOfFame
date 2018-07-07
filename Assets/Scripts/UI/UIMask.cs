using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMask : MonoBehaviour
{
    public GameObject uiMask;
    public GameObject winner;

    private void Start()
    {
        uiMask.SetActive(false);
    }

    public void ShowMask()
    {
        if (!uiMask.activeSelf)
        {
            uiMask.SetActive(true);
            uiMask.transform.position = new Vector3(winner.transform.position.x, 
                uiMask.transform.position.y, 
                uiMask.transform.position.z);
        }
    }

    public void HideMask()
    {
        if (uiMask.activeSelf)
        {
            uiMask.SetActive(false);
        }
    }
}

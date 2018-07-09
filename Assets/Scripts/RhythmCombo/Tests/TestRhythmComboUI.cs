using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRhythmComboUI : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            RhythmComboUI.instance.Activate();
        }
    }
}

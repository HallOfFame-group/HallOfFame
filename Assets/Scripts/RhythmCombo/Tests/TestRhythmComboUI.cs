using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRhythmComboUI : MonoBehaviour
{
    ComboPiece cp;

    private void Start()
    {
        cp = GetComponent<ComboPiece>();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            RhythmComboUI.instance.Prepare(cp);
            RhythmComboUI.instance.Activate();
        }
    }
}

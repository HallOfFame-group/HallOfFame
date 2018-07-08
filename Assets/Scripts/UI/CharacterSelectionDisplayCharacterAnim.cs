using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionDisplayCharacterAnim : MonoBehaviour
{
    [SerializeField]
    private float delayTime = 1.5f;

    [SerializeField]
    private int playerIndex = 1;

    private Animator anim;
    private int previousSelected = 0;

    private void Start()
    {
        anim = GetComponent<Animator>();
        MultiplayerSelection.instance.EvtOnPlayerHighlightCharacter += OnPlayerHighlightCharacter;

        OnPlayerHighlightCharacter();
    }

    private void OnPlayerHighlightCharacter()
    {
        StartCoroutine(DisplayDelay());
    }

    IEnumerator DisplayDelay()
    {
        yield return new WaitForSeconds(delayTime);

        int index = MultiplayerSelection.instance.GetCurrentSelectedIndex(playerIndex);

        if (index != previousSelected)
        {
            anim.runtimeAnimatorController = MultiplayerSelection.instance.GetCurrentHighlightedAnimation(playerIndex);
            previousSelected = index;
        }
    }
}

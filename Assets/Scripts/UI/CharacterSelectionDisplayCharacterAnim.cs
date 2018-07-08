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
        anim.runtimeAnimatorController = MultiplayerSelection.instance.GetCurrentHighlightedAnimation(playerIndex);
    }
}

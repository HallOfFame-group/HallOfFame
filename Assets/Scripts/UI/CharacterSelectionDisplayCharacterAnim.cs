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

    float originalYRotation;

    private void Start()
    {
        originalYRotation = transform.rotation.y;
        anim = GetComponent<Animator>();
        MultiplayerSelection.instance.EvtOnPlayerHighlightCharacter += OnPlayerHighlightCharacter;

        OnPlayerHighlightCharacter();
    }

    private void OnPlayerHighlightCharacter()
    {
        StopCoroutine(DisplayDelay());
        StartCoroutine(DisplayDelay());
    }

    IEnumerator DisplayDelay()
    {
        yield return new WaitForSeconds(delayTime);
        anim.runtimeAnimatorController = MultiplayerSelection.instance.GetCurrentHighlightedAnimation(playerIndex);
        if (MultiplayerSelection.instance.GetCurrentSelectedFlippingHack(playerIndex))
        {
            if (transform.rotation.y == originalYRotation)
            {
                transform.Rotate(Vector3.up, 180);
            }
        }
        else
        {
            if (transform.rotation.y != originalYRotation)
            {
                transform.Rotate(Vector3.up, 180);
            }
        }
    }
}

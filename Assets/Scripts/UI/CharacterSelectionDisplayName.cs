using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionDisplayName : MonoBehaviour
{
    [SerializeField]
    private float delayBeforeShowingName = 0.3f;
    [SerializeField]
    private int playerIndex = 1;
    private Image displayNameImg;

    private void Start()
    {
        displayNameImg = GetComponent<Image>();
        MultiplayerSelection.instance.EvtOnPlayerHighlightCharacter += OnPlayerHighlightCharacter;
    }

    private void OnPlayerHighlightCharacter()
    {
        Sprite img = MultiplayerSelection.instance.GetCurrentHighlightedDisplayName(playerIndex);
        if (img != displayNameImg.sprite)
        {
            displayNameImg.sprite = img;
            displayNameImg.color = Color.white;
        }
    }
}

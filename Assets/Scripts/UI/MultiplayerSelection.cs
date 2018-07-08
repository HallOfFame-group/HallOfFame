using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerSelection : MonoBehaviour
{
    public delegate void OnPlayerHighlightCharacter();
    public event OnPlayerHighlightCharacter EvtOnPlayerHighlightCharacter;

    private static MultiplayerSelection multiplayerSelection;
    public static MultiplayerSelection instance
    {
        get
        {
            if (!multiplayerSelection)
            {
                multiplayerSelection = FindObjectOfType<MultiplayerSelection>();
                if (!multiplayerSelection)
                {
                    Debug.LogError("MultiplayerSelection script must be attached to an active object in scene");
                }
            }
            return multiplayerSelection;
        }
    }

    private enum EPlayerControllerState
    {
        RESET = 0,
        MOVING,
        SET,
        WAIT
    };

    private Button[] characters;

    [SerializeField]
    private Color unselectedColor;

    [SerializeField]
    private Color [] playerColor;

    private Color [] playerHighlightColor;
    private Color [] playerSelectColor;

    private EPlayerControllerState[] playerControllerState;

    private int[] playerHighlighted;
    private bool[] isPlayerSelected;
    
    private bool isPlayer1Selected = false;
    private bool isPlayer2Selected = false;

    private void Start()
    {
        // Ensure the singleton pattern
        if (multiplayerSelection && multiplayerSelection != this)
        {
            Destroy(this);
        }

        characters = GetComponentsInChildren<Button>();

        // Set player highligh color and select color array based on number of player colors given
        playerHighlightColor = new Color[playerColor.Length];
        playerSelectColor = new Color[playerColor.Length];

        // Derive highlight and select color
        for(int i = 0; i < playerColor.Length; ++i)
        {
            playerHighlightColor[i] = playerColor[i];
            playerHighlightColor[i].a = 0.5f;

            playerSelectColor[i] = playerColor[i];
            playerSelectColor[i].a = 0.9f;
        }

        // Set player controller state based on number of player colors given
        playerControllerState = new EPlayerControllerState[playerColor.Length];

        playerHighlighted = new int[playerColor.Length];

        isPlayerSelected = new bool[playerColor.Length];

        // Highlight defaults
        for(int i = 0; i < playerColor.Length; ++i)
        {
            playerHighlighted[i] = i;
            characters[playerHighlighted[i]].image.color = playerHighlightColor[i];
        }
    }

    private void Update()
    {
        #region Force player to debounce the input axis

        for(int i = 0; i < playerColor.Length; ++i)
        {
            int playerInput = (int)Input.GetAxis("360Controller" + (i + 1) + "_LS_XAxis");
            bool playerReset = Input.GetAxis("360Controller" + (i + 1) + "_LS_XAxis") == 0;
            /*
             * RESET -> MOVING: When player LS X axis is touched
             * MOVING -> SET: When player LS X axis is at 1 or -1
             * SET -> WAIT: n/a
             * WAIT -> RESET: When player LS X axis is not at 1 or -1
             */
            switch (playerControllerState[i])
            {
                case EPlayerControllerState.RESET:
                    {
                        if (!playerReset)
                        {
                            playerControllerState[i] = EPlayerControllerState.MOVING;
                        }
                        break;
                    }
                case EPlayerControllerState.MOVING:
                    {
                        if (playerInput != 0)
                        {
                            playerControllerState[i] = EPlayerControllerState.SET;
                        }
                        break;
                    }
                case EPlayerControllerState.SET:
                    {
                        SetPlayerHighlight(playerInput, i);
                        playerControllerState[i] = EPlayerControllerState.WAIT;
                        break;
                    }
                case EPlayerControllerState.WAIT:
                    {
                        if (playerInput == 0)
                        {
                            playerControllerState[i] = EPlayerControllerState.RESET;
                        }
                        break;
                    }
            }


            bool isPlayerSelecting = Input.GetButtonDown("360Controller" + (i + 1) + "_Button_A");
            bool isPlayerCancelling = Input.GetButtonDown("360Controller" + (i + 1) + "_Block");

            CancelSelection(isPlayerCancelling, i);
            SelectCharacter(isPlayerSelecting, i);
        }
        #endregion
    }

    private void SetPlayerHighlight(int axisDirection, int playerIndex)
    {
        if (axisDirection != 0 && !isPlayerSelected[playerIndex])
        {
            int nextSelected = GetNextSelected(playerHighlighted[playerIndex], axisDirection);

            // Loop to find any player highlighting the target already
            for(int i = 0; i < playerColor.Length; ++i)
            {
                if (playerHighlighted[i] == nextSelected)
                {
                    nextSelected += axisDirection;
                }
            }

            // Unhighlight current selected
            characters[playerHighlighted[playerIndex]].image.color = unselectedColor;

            // Highlight next
            playerHighlighted[playerIndex] = nextSelected;
            characters[playerHighlighted[playerIndex]].image.color = playerHighlightColor[playerIndex];

            EvtOnPlayerHighlightCharacter();
        }
    }

    private void SelectCharacter(bool playerPressed, int playerIndex)
    {
        if (!playerPressed)
        {
            return;
        }

        if (characters[playerHighlighted[playerIndex]].GetComponent<CharacterSelectable>().IsSelectable)
        {
            isPlayerSelected[playerIndex] = !isPlayerSelected[playerIndex];
            characters[playerHighlighted[playerIndex]].image.color = (isPlayerSelected[playerIndex]) ? playerSelectColor[playerIndex] : playerHighlightColor[playerIndex];
        }

        // Verify all players are ready to proceed to fight
        bool checkAllPlayerReady = true;
        foreach(bool b in isPlayerSelected)
        {
            checkAllPlayerReady = checkAllPlayerReady && b;
        }

        if (checkAllPlayerReady)
        {
            SceneTransitionManager.instance.Proceed();
        }
    }

    private int GetNextSelected(int playerCurrentSelected, int playerInput)
    {
        int nextSelection = playerCurrentSelected + playerInput;
        return (nextSelection % characters.Length + characters.Length) % characters.Length;
    }

    private void CancelSelection(bool isPlayerCancelling, int playerIndex)
    {
        isPlayerCancelling = isPlayerSelected[playerIndex] && isPlayerCancelling;

        SelectCharacter(isPlayerCancelling, playerIndex);
    }

    public Sprite GetCurrentHighlightedDisplayName(int playerIndex)
    {
        Sprite nameImg = characters[playerHighlighted[--playerIndex]].GetComponent<CharacterSelectable>().characterName;

        if (nameImg)
        {
            return nameImg;
        }

        return null;
    }

    public RuntimeAnimatorController GetCurrentHighlightedAnimation(int playerIndex)
    {
        RuntimeAnimatorController anim = characters[playerHighlighted[--playerIndex]].GetComponent<CharacterSelectable>().characterDisplayAnim;
        return anim;
    }

    public int GetCurrentSelectedIndex(int playerIndex)
    {
        return playerHighlighted[--playerIndex];
    }

    public bool GetCurrentSelectedFlippingHack(int playerIndex)
    {
        return characters[playerHighlighted[--playerIndex]].GetComponent<CharacterSelectable>().RequireFlipping;
    }
}

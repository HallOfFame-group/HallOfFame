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
        RESET,
        MOVING,
        SET,
        WAIT
    };

    private Button[] characters;

    [SerializeField]
    private Color unselectedColor;

    [SerializeField]
    private Color player1Color;
    [SerializeField]
    private Color player2Color;

    private Color player1HighlightColor;
    private Color player2HighlightColor;
    private Color player1SelectColor;
    private Color player2SelectColor;

    private EPlayerControllerState player1State = EPlayerControllerState.RESET;
    private EPlayerControllerState player2State = EPlayerControllerState.RESET;

    private int player1Highlighted = 0;
    private int player2Highlighted = 0;
    
    private bool isPlayer1Selected = false;
    private bool isPlayer2Selected = false;

    private void Start()
    {
        // Ensure the singleton pattern
        if (multiplayerSelection && multiplayerSelection != this)
        {
            Destroy(this);
        }

        // Derive highlight and select color
        player1HighlightColor = player1Color;
        player1HighlightColor.a = 0.5f;
        player1SelectColor = player1Color;
        player1SelectColor.a = 0.9f;

        player2HighlightColor = player2Color;
        player2HighlightColor.a = 0.5f;
        player2SelectColor = player2Color;
        player2SelectColor.a = 0.9f;

        // Find selectable buttons
        characters = transform.GetComponentsInChildren<Button>();
        player2Highlighted = characters.Length - 1;

        // Highlight defaults
        characters[player1Highlighted].image.color = player1HighlightColor;
        characters[player2Highlighted].image.color = player2HighlightColor;
    }

    private void Update()
    {
        #region Force player to debounce the input axis
        // LS X-axis input value from players
        int player1Input = (int)Input.GetAxis("360Controller1_LS_XAxis");
        int player2Input = (int)Input.GetAxis("360Controller2_LS_XAxis");

        // Boolean to check if the LS X-axis is moved
        bool player1InputReset = Input.GetAxis("360Controller1_LS_XAxis") == 0;
        bool player2InputReset = Input.GetAxis("360Controller2_LS_XAxis") == 0;

        /*
         * RESET -> MOVING: When player LS X axis is touched
         * MOVING -> SET: When player LS X axis is at 1 or -1
         * SET -> WAIT: n/a
         * WAIT -> RESET: When player LS X axis is not at 1 or -1
         */
        switch (player1State)
        {
            case EPlayerControllerState.RESET:
                {
                    if (!player1InputReset)
                    {
                        player1State = EPlayerControllerState.MOVING;
                    }
                    break;
                }
            case EPlayerControllerState.MOVING:
                {
                    if (player1Input != 0)
                    {
                        player1State = EPlayerControllerState.SET;
                    }
                    break;
                }
            case EPlayerControllerState.SET:
                {
                    Player1AxisSet(player1Input);
                    player1State = EPlayerControllerState.WAIT;
                    break;
                }
            case EPlayerControllerState.WAIT:
                {
                    if (player1Input == 0)
                    {
                        player1State = EPlayerControllerState.RESET;
                    }
                    break;
                }
        }

        switch (player2State)
        {
            case EPlayerControllerState.RESET:
                {
                    if (!player2InputReset)
                    {
                        player2State = EPlayerControllerState.MOVING;
                    }
                    break;
                }
            case EPlayerControllerState.MOVING:
                {
                    if (player2Input != 0)
                    {
                        player2State = EPlayerControllerState.SET;
                    }
                    break;
                }
            case EPlayerControllerState.SET:
                {
                    Player2AxisSet(player2Input);
                    player2State = EPlayerControllerState.WAIT;
                    break;
                }
            case EPlayerControllerState.WAIT:
                {
                    if (player2Input == 0)
                    {
                        player2State = EPlayerControllerState.RESET;
                    }
                    break;
                }
        }
        #endregion

        #region Handle player selecting the player
        bool isPlayer1Selecting = Input.GetButtonDown("360Controller1_Button_A");
        bool isPlayer2Selecting = Input.GetButtonDown("360Controller2_Button_A");
        bool isPlayer1Cancelling = Input.GetButtonDown("360Controller1_Block");
        bool isPlayer2Cancelling = Input.GetButtonDown("360Controller2_Block");

        CancelSelection(isPlayer1Cancelling, isPlayer2Cancelling);
        SelectCharacter(isPlayer1Selecting, isPlayer2Selecting);
        #endregion
    }

    private void Player1AxisSet(int axisDirection)
    {
        // Check player 1 inputs
        if (axisDirection != 0 && !isPlayer1Selected)
        {
            // Skip ahead if selecting same character
            int player1NextSelected = GetNextSelected(player1Highlighted, axisDirection);
            if (player1NextSelected == player2Highlighted)
            {
                axisDirection *= 2;
            }

            // Unhighlight current selected
            characters[player1Highlighted].image.color = unselectedColor;

            // Highlight next object
            player1Highlighted = GetNextSelected(player1Highlighted, axisDirection);
            characters[player1Highlighted].image.color = player1HighlightColor;

            EvtOnPlayerHighlightCharacter();
        }
    }

    private void Player2AxisSet(int axisDirection)
    {
        // Check player 2 inputs
        if (axisDirection != 0 && !isPlayer2Selected)
        {
            // Skip ahead if selecting same character
            int player2NextSelected = GetNextSelected(player2Highlighted, axisDirection);
            if (player2NextSelected == player1Highlighted)
            {
                axisDirection *= 2;
            }

            // Unhighlight current selected
            characters[player2Highlighted].image.color = unselectedColor;

            // Highlight next object
            player2Highlighted = GetNextSelected(player2Highlighted, axisDirection);
            characters[player2Highlighted].image.color = player2HighlightColor;

            EvtOnPlayerHighlightCharacter();
        }
    }

    private int GetNextSelected(int playerCurentSelected, int playerInput)
    {
        int nextSelection = playerCurentSelected + playerInput;
        return (nextSelection % characters.Length + characters.Length) % characters.Length;
    }

    private void SelectCharacter(bool player1ButtonPressed, bool player2ButtonPressed)
    {
        if (player1ButtonPressed)
        {
            if (characters[player1Highlighted].GetComponent<CharacterSelectable>().IsSelectable)
            {
                isPlayer1Selected = !isPlayer1Selected;
                characters[player1Highlighted].image.color = (isPlayer1Selected) ? player1SelectColor : player1HighlightColor;
            }
        }

        if (player2ButtonPressed)
        {
            if (characters[player2Highlighted].GetComponent<CharacterSelectable>().IsSelectable)
            {
                isPlayer2Selected = !isPlayer2Selected;
                characters[player2Highlighted].image.color = (isPlayer2Selected) ? player2SelectColor : player2HighlightColor;
            }
        }

        // Return a callback function when both player have selected
        if (isPlayer1Selected && isPlayer2Selected)
        {
            SceneTransitionManager.instance.Proceed();
        }
    }

    private void CancelSelection(bool isPlayer1Cancelling, bool isPlayer2Cancelling)
    {
        isPlayer1Cancelling = isPlayer1Selected && isPlayer1Cancelling;
        isPlayer2Cancelling = isPlayer2Selected && isPlayer2Cancelling;

        SelectCharacter(isPlayer1Cancelling, isPlayer2Cancelling);
    }

    public Sprite GetCurrentHighlightedDisplayName(int playerIndex)
    {
        Sprite nameImg;
        switch (playerIndex)
        {
            case 1:
                nameImg = characters[player1Highlighted].GetComponent<CharacterSelectable>().characterName;
                break;
            case 2:
                nameImg = characters[player2Highlighted].GetComponent<CharacterSelectable>().characterName;
                break;
            default:
                nameImg = null;
                break;
        }

        if (nameImg)
        {
            return nameImg;
        }

        return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct TimedNode
{
    public float timeStamp;
    public NodeButton nodeButton;
}

public class ComboPiece : MonoBehaviour
{
    #region Public Editable Members
    public string musicPathReference;
    public string musicName;
    public string artistName;
    public Image icon;
    public TimedNode[] timeNodeArray;
    #endregion

    #region Private Members

    [FMODUnity.EventRef]
    string musicPath;

    FMOD.Studio.EventInstance musicEvent;

    #endregion

    #region Internal Methods

    private void Start()
    {
        musicPath = musicPathReference;
        musicEvent = FMODUnity.RuntimeManager.CreateInstance(musicPath);

        musicEvent.start();
    }

    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSoundManager : MonoBehaviour
{

    string footstepPath;
    string lightpunchPath;
    string heavyPunchPath;
    string punchedPath;
    string kickPath;

    FMOD.Studio.EventInstance footstep;
    FMOD.Studio.EventInstance lightPunch;
    FMOD.Studio.EventInstance heavyPunch;
    FMOD.Studio.EventInstance punched;
    FMOD.Studio.EventInstance kick;

    // Use this for initialization
    void Start()
    {

        footstepPath = "event:/SFX/Character/Footstep";
        lightpunchPath = "event:/SFX/Character/Light Punch Whoosh";
        heavyPunchPath = "event:/SFX/Character/Heavy Punch Whoosh";
        punchedPath = "event:/SFX/Character/Punched";
        kickPath = "event:/SFX/Character/Kick Whoosh";

        footstep = FMODUnity.RuntimeManager.CreateInstance(footstepPath);
        lightPunch = FMODUnity.RuntimeManager.CreateInstance(lightpunchPath);
        heavyPunch = FMODUnity.RuntimeManager.CreateInstance(heavyPunchPath);
        punched = FMODUnity.RuntimeManager.CreateInstance(punchedPath);
        kick = FMODUnity.RuntimeManager.CreateInstance(kickPath);


    }

    // Update is called once per frame
    void Update()
    {

    }


    public void Footfall()
    {
        footstep.start();
    }

    public void LightPunch()
    {
        lightPunch.start();
    }

    public void HeavyPunch()
    {
        heavyPunch.start();
    }

    public void Punched()
    {
        punched.start();
    }

    public void Kick()
    {
        kick.start();
    }

    public void LightEffort(string name)
    {

    }

    public void Effort(string name)
    {

    }

    public void HeavyEffort(string name)
    {

    }

    public void Death(string name)
    {

    }
}
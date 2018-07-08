using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerImageAnimationHandler : MonoBehaviour
{
    public delegate void OnBannerImageAnimationEnd();
    public event OnBannerImageAnimationEnd EvtOnBannerImageAnmiationEnd;

    public void BannerImageAnimationEndHandler()
    {
        EvtOnBannerImageAnmiationEnd();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBanner : MonoBehaviour
{
    public enum EBannerToShown
    {
        ShowLeftBeethoven,
        ShowRightBeethoven,
        ShowLeftMozart,
        ShowRightMozart
    };

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Show(EBannerToShown bannerToShown)
    {
        switch (bannerToShown)
        {
            case EBannerToShown.ShowLeftBeethoven:
                anim.SetTrigger("LeftBeethoven");
                break;
            case EBannerToShown.ShowRightBeethoven:
                anim.SetTrigger("RightBeethoven");
                break;
            case EBannerToShown.ShowLeftMozart:
                anim.SetTrigger("LeftMozart");
                break;
            case EBannerToShown.ShowRightMozart:
                anim.SetTrigger("RightMozart");
                break;
        }
    }

    public void BannerAnimationFinishedHandler()
    {
        RhythmCombo.instance.BannerAnimFinishedHandler();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class CrowdAnimControl : MonoBehaviour
{
    private SkeletonAnimation anim;
    private float value = 0;

    private void Awake()
    {
        anim = GetComponent<SkeletonAnimation>();
    }

    private void Update()
    {
        anim.state.Update(Time.deltaTime);
        anim.state.Apply(anim.skeleton);

        value -= Time.deltaTime;
        value = Mathf.Clamp(value, 0, 100);

        float animValue = Mathf.Clamp(value, 10, 90);

        string targetName = ((int)animValue / 10).ToString() + "0";
        if (targetName != anim.AnimationName)
        {
            anim.state.SetAnimation(0, targetName, true);

        }
    }

    public void IncreaseByValue(float v)
    {
        value += v;
    }
}

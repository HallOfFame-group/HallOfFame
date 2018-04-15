using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine;
using Spine.Unity;

/*
 * Author: Jason Lin
 * 
 * Description:
 * Singleton CrowdBar for controlling the crowdbar movement as well as visualize the movement
 * Range[-100, 100]
 * when the crowdExcitement value > 0, that means it's moving towards right
 * when the crowdExcitement value < 0, that menas it's moving towards left
 */
public class CrowdBar : MonoBehaviour
{
    [SerializeField]
    private SkeletonAnimation player1;

    [SerializeField]
    private SkeletonAnimation player2;

    private static CrowdBar crowdBar;

    public static CrowdBar instance
    {
        get
        {
            if (!crowdBar)
            {
                crowdBar = FindObjectOfType(typeof(CrowdBar)) as CrowdBar;

                if (!crowdBar)
                {
                    Debug.LogError("CrowdBar Script must be attached to a gameobject in scene");
                }
                else
                {
                    crowdBar.Init();
                }
            }
            return crowdBar;
        }
    }

    private void Init()
    {
    }

    private void Awake()
    {
    }

    public void IncreaseToPlayer1(int value)
    {
        player1.GetComponent<CrowdAnimControl>().IncreaseByValue(3);
    }

    public void IncreaseToPlayer2(int value)
    {
        player2.GetComponent<CrowdAnimControl>().IncreaseByValue(3);
    }
}

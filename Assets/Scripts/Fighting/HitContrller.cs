using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerController))]
public class HitContrller : MonoBehaviour {

    enum AttackType { Light, Heavy, Float }

    [System.Serializable]
    private struct AttackPref
    {
        [SerializeField]
        public string AttackName;

        [SerializeField]
        public AttackType attackType;
        
    };

    [SerializeField]
    private AttackPref[] attackList;

    //private int hitFramer;
    private PlayerController playerController;
    private Animator animator;
    private BoxCollider2D hitbox;
    private BoxCollider2D attackbox;


    public bool attackTriggered = false;
    public int hits = 0;
    public Text text;


    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        animator = GetComponentInChildren<Animator>();
        hitbox = transform.Find("Animator").Find("HitBox").GetComponent<BoxCollider2D>();
        attackbox = transform.Find("Animator").Find("AttackBox").GetComponent<BoxCollider2D>();
    }
    
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if(otherCollider.transform.root.CompareTag("Player"))
        {
            if (!attackTriggered)
            {
                if ((otherCollider.name == "HitBox"|| otherCollider.name == "AttackBox")&& otherCollider.IsTouching(attackbox))
                {
                    attackTriggered = true;

                    AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

                    HitContrller opponentHitController = otherCollider.transform.root.GetComponent<HitContrller>();
                    Animator opponentAnimator = otherCollider.transform.root.GetComponentInChildren<Animator>();
                    AnimatorStateInfo opponentStateInfo = opponentAnimator.GetCurrentAnimatorStateInfo(0);
                    

                    if (!opponentStateInfo.IsName("Block"))
                    {
                        for (int i = 0; i < attackList.Length; i++)
                        {
                            if (stateInfo.IsName(attackList[i].AttackName))
                            {
                                opponentHitController.hits++;
                                switch(attackList[i].attackType)
                                {
                                    case AttackType.Light:
                                        opponentAnimator.SetTrigger("Hit Up");
                                        break;

                                    case AttackType.Heavy:
                                        opponentAnimator.SetTrigger("Hit Down");
                                        break;

                                    case AttackType.Float:
                                        opponentAnimator.SetTrigger("Death");
                                        break;
                                }

                                if (opponentHitController.hits > 7)
                                {
                                    //RhythmCombo.instance.Register(this.GetComponent<ComboPiece>());
                                    //RhythmCombo.instance.Display(1);
                                    //RhythmCombo.instance.nodeEventCallback = OnNodeHit;
                                    //RhythmCombo.instance.finishedEventCallback = finished;
                                }

                                break;
                            }
                        }
                    }
                    else
                    {

                    }

                    if(gameObject.name=="Mozart")
                        otherCollider.transform.root.GetComponent<Rigidbody2D>().AddForce(-transform.right * 10, ForceMode2D.Impulse);
                    else
                        otherCollider.transform.root.GetComponent<Rigidbody2D>().AddForce(transform.right * 10, ForceMode2D.Impulse);

                }
            }
        }
    }

    void OnNodeHit(NodePressResult result)
    {
        switch (result)
        {
            case NodePressResult.PERFECT:
            case NodePressResult.GOOD:
                CrowdBar.instance.IncreaseToPlayer1(30);
                break;
            case NodePressResult.BAD:
            case NodePressResult.MISS:
            default:
                CrowdBar.instance.IncreaseToPlayer2(30);
                break;
        }

    }


    void finished()
    {
        Debug.Log("Finished");
        Debug.Log(RhythmCombo.instance.comboResult.perfectCount);
        Debug.Log(RhythmCombo.instance.comboResult.goodCount);
        Debug.Log(RhythmCombo.instance.comboResult.badCount);
        Debug.Log(RhythmCombo.instance.comboResult.missCount);
        CrowdBar.instance.IncreaseToPlayer1(30);
    }
    private void Update()
    {

        //if (hits > 1)
        //{
        //    test.text = "" + hits;
        //}

        //if (hitTimer>0)
        //{
        //    hitTimer -= Time.deltaTime;
        //}
        //else
        //{
        //    hitTimer = 0.0f;
        //    hits = 0;
        //    test.text = "";
        //}

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerController))]
public class HitContrller : MonoBehaviour {

    private float hitTimer;
    //private int hitFramer;
    private PlayerController playerController;
    private Animator animator;

    public int hits = 0;

    public Text test;

    private string[] Attack_Name = { "Punch1", "Punch2", "Punch3", "Kick1", "Kick2", "Head Smash", "Duck Punch", "Duck Kick", "Jump Punch", "Jump Punch", "Jump Kick" };
    private float[] Attack_HitTime = { 1f, 1f, 1f, 1f, 1f,1f,1f,1f,1f,1f};


    private void Awake()
    {
        hitTimer = 0.0f;
        //hitFramer = 0;
        playerController = GetComponent<PlayerController>();
        animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.transform.root.CompareTag("Player"))
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            HitContrller opponentHitController = collision.transform.root.GetComponent<HitContrller>();
            Animator opponentAnimator = collision.transform.root.GetComponentInChildren<Animator>();
            AnimatorStateInfo opponentStateInfo = opponentAnimator.GetCurrentAnimatorStateInfo(0);
            //TODO: still has some issue with opponentStateInfo block
            if (collision.name.Contains("Position") && !opponentStateInfo.IsName("Block"))
            {
                for (int i = 0; i < Attack_Name.Length; i++)
                {
                    if (stateInfo.IsName(Attack_Name[i]))
                    {
                        opponentHitController.hits++;
                        opponentHitController.hitTimer = Attack_HitTime[i];
                        opponentAnimator.SetTrigger("Hit Up");

                        if(opponentHitController.hits > 7)
                        {
                            //    RhythmCombo.instance.Register(this.GetComponent<ComboPiece>());
                            //    RhythmCombo.instance.Display();
                            //    RhythmCombo.instance.nodeEventCallback = OnNodeHit;
                            //    RhythmCombo.instance.finishedEventCallback = finished;
                        }
                    }
                }
            }

        }
    }

    private void Update()
    {

        if (hits > 1)
        {
            test.text = "" + hits;
        }

        if (hitTimer>0)
        {
            hitTimer -= Time.deltaTime;
        }
        else
        {
            hitTimer = 0.0f;
            hits = 0;
            test.text = "";
        }
        animator.SetFloat("HitTime",hitTimer);

    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log(collision.collider.name);
    //}
}

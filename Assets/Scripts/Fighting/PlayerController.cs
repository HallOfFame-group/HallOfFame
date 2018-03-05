using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    [SerializeField] Transform target;
    [SerializeField] float moveSpeed = 20;
    [SerializeField] float jumpHight = 75;

    private Animator animator;
    private Rigidbody2D playerRigidbody;
    private BoxCollider2D positionBox;

    public bool isGrounded;
    public bool isCrouching;
    public bool isBlocking;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        positionBox = transform.Find("PositionBox").GetComponent<BoxCollider2D>();
    }


    private void FixedUpdate()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);



        float horizontal = Input.GetAxis("360Controller_LS_XAxis");
        horizontal = (horizontal == 0) ? Input.GetAxis("Keyboard_Horizontal") : horizontal;
        animator.SetFloat("WalkSpeed", horizontal);
        if (stateInfo.IsName("Walk") || stateInfo.IsName("Back Walk"))
        {
            transform.position = playerRigidbody.position + Vector2.right * horizontal * moveSpeed * Time.fixedDeltaTime;
        }


        float vertical = Input.GetAxis("360Controller_LS_YAxis");
        vertical = (vertical == 0) ? Input.GetAxis("Keyboard_Vertical") : vertical;
        if (vertical > 0.1f && isGrounded&&(stateInfo.IsName("Idle") || stateInfo.IsName("Walk") || stateInfo.IsName("Back Walk")))
        {
            isGrounded = false;
            animator.SetBool("isGrounded", false);
            playerRigidbody.AddForce(new Vector2(horizontal * moveSpeed, jumpHight), ForceMode2D.Impulse);
        }

        if (vertical < -0.1f && isGrounded)
        {
            isCrouching = true;
            animator.SetBool("isCrouching", true);
        }
        else
        {
            isCrouching = false;
            animator.SetBool("isCrouching", false);
        }

        if (Input.GetButton("360Controller_Block") || Input.GetButton("Keyboard_Block"))
        {
            isBlocking = true;
            animator.SetBool("isBlocking", true);
        }
        else
        {
            isBlocking = false;
            animator.SetBool("isBlocking", false);
        }
        

        if (Input.GetButtonDown("360Controller_Punch") || Input.GetButtonDown("Keyboard_Punch"))
            animator.SetTrigger("Punch");

        if (Input.GetButtonDown("360Controller_Kick") || Input.GetButtonDown("Keyboard_Kick"))
            animator.SetTrigger("Kick");



    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;

            positionBox.gameObject.layer = StaticValue.PositionBox_Ground;

            animator.SetBool("isGrounded", true);

            if (target != null)
            {
                if (transform.position.x < target.position.x)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
            }

        }
        else if(collision.gameObject.CompareTag("Player"))
        {
            if(collision.contacts[0].normal.y>0)
            {

                positionBox.gameObject.layer = StaticValue.PositionBox_Jump;

            }
        }
    }



    //private IEnumerator Jump(float horizontal, int frames)
    //{
    //    for (int i = 0; i < frames; i++)
    //        yield return null;

    //    playerRigidbody.AddForce(new Vector2(horizontal * moveSpeed, jumpHight), ForceMode2D.Impulse);
    //}
}

using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    [SerializeField] Transform target;
    [SerializeField] float moveSpeed = 20;
    [SerializeField] float jumpHight = 75;

    private Animator animator;
    private Rigidbody2D rigidbody2d;
    private BoxCollider2D playerCollider;


    
    public bool isGrounded;
    

	void Start ()
    {
        animator = transform.GetComponentInChildren<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        playerCollider = transform.GetComponentInChildren<BoxCollider2D>();
    }
	
    

    private void FixedUpdate()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);



        float horizontal = Input.GetAxis("Horizontal");
        animator.SetFloat("WalkSpeed", horizontal);
        if (stateInfo.IsName("Walk") || stateInfo.IsName("Walk_Back"))
        {

            rigidbody2d.position = rigidbody2d.position + Vector2.right* horizontal * moveSpeed * Time.fixedDeltaTime;
        }


        float vertical = Input.GetAxis("Vertical");
        if(vertical>0.1f&&isGrounded)
        {
            isGrounded = false;
            animator.SetBool("Jump",true);
            transform.Find("PositionCollider").gameObject.layer = StaticValue.JUMP_COLLIDER_LAYER;
            rigidbody2d.AddForce(new Vector2(horizontal* moveSpeed, jumpHight), ForceMode2D.Impulse);
        }

        if(Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Punch");
        }

        if (Input.GetButtonDown("Fire2"))
        {
            animator.SetTrigger("Kick");
        }




    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("Jump", false);
            transform.Find("PositionCollider").gameObject.layer = StaticValue.GROUND_COLLIDER_LAYER;

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
    }
    



}

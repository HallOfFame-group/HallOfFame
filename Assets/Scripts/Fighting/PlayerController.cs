using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    [SerializeField] Transform target;
    [SerializeField] float moveSpeed = 20;
    [SerializeField] float jumpHight = 75;
    public int PlayerNumber = 0;

    private Animator animator;
    private Rigidbody2D playerRigidbody;
    private BoxCollider2D positionBox;


    public bool IsGrounded { get; set; }
    public bool IsCrouching { get; set; }
    public bool IsBlocking { get; set; }

<<<<<<< HEAD
=======
    public enum WeaponType { piano, violin};
    public WeaponType selectedWeapon = WeaponType.piano;
    public GameObject weapon;
>>>>>>> 132ba892fc5e2590101ff943f4aa5fee5cb380c3
    //public CameraShake cameraShake;
    //public SlowMotionEffect slowMotionEffect;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        positionBox = transform.Find("Animator").Find("PositionBox").GetComponent<BoxCollider2D>();
    }

    private void Update()
    {

        if (Input.GetButton("360Controller" + PlayerNumber + "_Block") || Input.GetButton("Keyboard_Block"))
        {
            IsBlocking = true;
            animator.SetBool("isBlocking", true);
        }
        else
        {
            IsBlocking = false;
            animator.SetBool("isBlocking", false);
        }


        if (Input.GetButtonDown("360Controller" + PlayerNumber + "_Punch") || Input.GetButtonDown("Keyboard_Punch"))
        {
            animator.SetTrigger("Punch");
        }

        if (Input.GetButtonDown("360Controller" + PlayerNumber + "_Kick") || Input.GetButtonDown("Keyboard_Kick"))
            animator.SetTrigger("Kick");

        if (Input.GetButtonDown("360Controller" + PlayerNumber + "_Throw") || Input.GetButtonDown("Keyboard_Throw"))
        {
            animator.SetTrigger("Throw");
            GameObject temp;
            switch (selectedWeapon)
            {
                case WeaponType.piano:         
                    temp = Instantiate(weapon, new Vector2(target.transform.position.x, target.transform.position.y + 25), Quaternion.identity) as GameObject;
                    temp.GetComponent<Projectiles>().target = target.gameObject;
                    break;
                case WeaponType.violin:
                    temp = Instantiate(weapon, new Vector2(transform.position.x + 2.5f * (target.transform.position - transform.position).normalized.x, transform.position.y + 7.5f), Quaternion.identity) as GameObject;
                    temp.GetComponent<Rigidbody2D>().AddForce((target.transform.position - transform.position).normalized * 2000, 0);
                    temp.GetComponent<Projectiles>().target = target.gameObject;
                    break;
            }
        }


    }

    private void FixedUpdate()
    {

        if (target != null&&IsGrounded)
        {
            if (transform.position.x > target.position.x)
            {
                transform.rotation = Quaternion.Euler(0,180,0);
            }
            else
            {
                transform.rotation = Quaternion.identity;
            }
        }


        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);



        float horizontal = Input.GetAxis("360Controller"+ PlayerNumber + "_LS_XAxis");
        horizontal = (horizontal == 0) ? Input.GetAxis("Keyboard_Horizontal") : horizontal;
        animator.SetFloat("WalkSpeed", horizontal);
        if (stateInfo.IsName("Walk") || stateInfo.IsName("Back Walk"))
        {
            transform.position = playerRigidbody.position + Vector2.right * horizontal * moveSpeed * Time.fixedDeltaTime;
        }


        float vertical = Input.GetAxis("360Controller"+ PlayerNumber + "_LS_YAxis");
        vertical = (vertical == 0) ? Input.GetAxis("Keyboard_Vertical") : vertical;
        if (vertical > 0.1f && IsGrounded&&(stateInfo.IsName("Idle") || stateInfo.IsName("Walk") || stateInfo.IsName("Back Walk")))
        {
            IsGrounded = false;
            animator.SetBool("isGrounded", false);
            playerRigidbody.AddForce(new Vector2(horizontal * moveSpeed, jumpHight), ForceMode2D.Impulse);
        }

        if (vertical < -0.1f && IsGrounded)
        {
            IsCrouching = true;
            animator.SetBool("isCrouching", true);
        }
        else
        {
            IsCrouching = false;
            animator.SetBool("isCrouching", false);
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsGrounded = true;

            positionBox.gameObject.layer = StaticValue.PositionBox_Ground;

            animator.SetBool("isGrounded", true);


        }
        else if(collision.gameObject.CompareTag("Player"))
        {
            if(collision.contacts[0].normal.y>0)
            {

                positionBox.gameObject.layer = StaticValue.PositionBox_Jump;

            }
        }
    }



}

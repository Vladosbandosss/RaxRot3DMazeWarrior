using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody rb;

    private Animator anim;
    private bool isPlayerMoving;

    private float playerSpeed = 0.5f;
    private float rotationSpeed = 4f;

    private float JumpForce = 3f;
    private bool canJump;

    private float moveHorizontal;
    private float moveVertical;

    private float rotationY = 0f;

    public Transform groundCheckPosition;
    public LayerMask groundLayer;

    public GameObject damagePoint;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        rotationY = transform.localRotation.eulerAngles.y;
    }

    private void Update()
    {
        PlayerMoveKeyBoard();
        AnimatePlayer();
        Attack();
        IsOnGround();
        Jump();
    }

    private void FixedUpdate()
    {
        MoveAndRotate();
    }

    void PlayerMoveKeyBoard()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            moveHorizontal = -1f;
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            moveHorizontal = 0f;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            moveHorizontal = 1f;
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            moveHorizontal = 0f;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            moveVertical = 1f;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            moveVertical = 0f;
        }

      
    }

    void MoveAndRotate()
    {
        if (moveVertical != 0)
        {
            rb.MovePosition(transform.position + transform.forward * (moveVertical * playerSpeed));//прямо двигаемся
        }

        rotationY += moveHorizontal * rotationSpeed;
        rb.rotation = Quaternion.Euler(0f, rotationY, 0f);//вращает лево право
    }

    void AnimatePlayer()
    {
        if (moveVertical != 0)
        {
            if (!isPlayerMoving)
            {
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName(MyTags.RUNANIMATION))
                {
                    isPlayerMoving = true;
                    anim.SetTrigger(MyTags.RUNTRIGGER);
                }
            }
        }

        else
        {
            if (isPlayerMoving)
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName(MyTags.RUNANIMATION))
                {
                    isPlayerMoving = false;
                    anim.SetTrigger(MyTags.STOPTRIGGER);
                }
            }
        }
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName(MyTags.ATTACKANIMATION)||
                !anim.GetCurrentAnimatorStateInfo(0).IsName(MyTags.RUNATTACKANIMATION))
            {
                anim.SetTrigger(MyTags.ATTACKTRIGGER);
            }
        }
    }

    void IsOnGround()
    {
        canJump = Physics.Raycast(groundCheckPosition.position, Vector3.down, 0.1f, groundLayer);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canJump)
            {
                canJump = false;

                rb.MovePosition(transform.position + transform.up * (JumpForce * playerSpeed));

                anim.SetTrigger(MyTags.JUMPTRIGGER);
            }
        }
    }
    void ActivateDamagePoint()
    {
        damagePoint.SetActive(true);
    }

    void DeactivateDamagePoint()
    {
        damagePoint.SetActive(false);
    }
}//class

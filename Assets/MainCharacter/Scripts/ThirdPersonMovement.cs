using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    private CharacterController characterController;
    public Transform cam;
    private Animator animator;
    //　キャラクターの速度
    private Vector3 velocity;

    //　キャラクターの歩くスピード
    [SerializeField]
    private float walkSpeed = 2f;

    //　キャラクターの走るスピード
    [SerializeField]
    private float runSpeed = 4f;

    [SerializeField]
    private Vector3 addForceDownPower = Vector3.down;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public float jumpForce = 10;
    public bool isGround = false;



    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        velocity = Vector3.zero;
    }


    void Update()
    {
        if (characterController.isGrounded)
        {
            velocity = Vector3.zero;
            animator.SetBool("jump", false);
        }
        var input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        if (input.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(input.x, input.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            
            animator.SetFloat("vertical", input.magnitude);
            velocity.x = input.normalized.x * 2;
            velocity.z = input.normalized.z * 2;
            if (input.magnitude > 0.5f)
            {
                characterController.Move(moveDir.normalized * runSpeed * Time.deltaTime);
            }
            else
            {
                characterController.Move(moveDir.normalized * walkSpeed * Time.deltaTime);
            }
        }
        else
        {
            animator.SetFloat("vertical", 0f);
        }

        //ジャンプ
        if (Input.GetButton("A")　&& !animator.GetCurrentAnimatorStateInfo(0).IsName("Jump")　&& !animator.IsInTransition(0))
        {
            animator.SetBool("jump", true);
            velocity.y += jumpForce;
        }
        else
        {
            velocity += addForceDownPower;
        }

        //ダッシュ回避
        if(Input.GetButton("B"))
        {

        }



        velocity.y += Physics.gravity.y * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }


}

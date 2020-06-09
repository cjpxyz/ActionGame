using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private CharacterController characterController;
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
            var input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            
            if (input.magnitude > 0.1f)
            {
                transform.LookAt(transform.position + input.normalized);
                animator.SetFloat("vertical", input.magnitude);
                velocity.x = input.normalized.x * 2;
                velocity.z = input.normalized.z * 2;

                if (input.magnitude > 0.5f)
                {
                    velocity += transform.forward * runSpeed;
                }
                else
                {
                    velocity += transform.forward * walkSpeed;
                }
            }
            else
            {
                animator.SetFloat("vertical", 0f);
            }

            //ジャンプキー
            if (Input.GetButton("A")
                && !animator.GetCurrentAnimatorStateInfo(0).IsName("Jump")
                && !animator.IsInTransition(0)
               )
            {
                    animator.SetBool("jump", true);
                    velocity.y += jumpForce;
                }
            else
            {
                velocity += addForceDownPower;
            }

        

        velocity.y += Physics.gravity.y * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }


}
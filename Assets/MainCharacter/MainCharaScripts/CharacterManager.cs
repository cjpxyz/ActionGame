using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private Rigidbody rb;
    private float h, v;
    public Transform cam;
    private Animator animator;
    //　キャラクターの速度
    private Vector3 velocity;

    public float ForceGravity;

    //　キャラクターの歩くスピード
    [SerializeField]
    private float walkSpeed = 2f;

    //　キャラクターの走るスピード
    [SerializeField]
    private float runSpeed = 4f;

    /*[SerializeField]
    private Vector3 addForceDownPower = Vector3.down;*/

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public float dodgeForce = 5;
    public float jumpForce = 10;
    public bool isGround = false;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        velocity = Vector3.zero;
        isGround = false;
    }


    void FixedUpdate()
    {
        /*if (characterController.isGrounded)
        {
            
            animator.SetBool("jump", false);
        }*/

        Vector3 extraGravityForce = (Physics.gravity * ForceGravity) - Physics.gravity;
        rb.AddForce(extraGravityForce);
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        var input = new Vector3(h, 0f, v);

        if (input.magnitude > 0.1f)
        {
            //カメラの向きを基準に移動
            float targetAngle = Mathf.Atan2(input.x, input.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            
            animator.SetFloat("vertical", input.magnitude);
            /*velocity.x = input.normalized.x * 2;
            velocity.z = input.normalized.z * 2;*/
            if (input.magnitude > 0.6f)
            {
                rb.velocity = (moveDir.normalized * runSpeed);
            }
            else
            {
                rb.velocity = (moveDir.normalized * walkSpeed);
            }


            

        }
        else
        {
            animator.SetFloat("vertical", 0f);
        }



        //ジャンプ
        /*if (Input.GetButton("A")　&& !animator.GetCurrentAnimatorStateInfo(0).IsName("Jump")　&& !animator.IsInTransition(0))
        {
            
            velocity.y += jumpForce;
        }
        else
        {
            velocity += addForceDownPower;
        }*/

        if (Input.GetButtonDown("A") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Jump") && !animator.IsInTransition(0))
        {
            //animator.SetBool("jump", true);
            rb.velocity = Vector3.up * jumpForce;
        }


        //ダッシュ回避

        if (Input.GetButton("B"))
        {
            rb.AddForce(transform.forward * dodgeForce, ForceMode.Impulse);
        }


        /*velocity.y += Physics.gravity.y * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);*/
    }

    /*private void FixedUpdate()
    {
        // rb.AddForce(Vector3.down * ForceGravity, ForceMode.Acceleration); 
        //重力を付与
        
    }*/


}

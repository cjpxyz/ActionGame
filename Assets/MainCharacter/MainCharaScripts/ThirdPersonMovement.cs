using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace ActionGame
{
    public class ThirdPersonMovement : MonoBehaviour
    {
        private CharacterController characterController;
        CharacterStatus status;
        public Transform cam;
        private Animator animator;
        LockOnTarget  LockOnTarget;
        //　キャラクターの向き
        private Vector3 moveDirection;

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
        public float dodgeForce = 5;
        public float jumpForce = 10;
        public float rotateSpeed = 0.1f;
        public float gravity = 20.0f;

        

        void Start()
        {
            characterController = GetComponent<CharacterController>();
            status = GetComponent<CharacterStatus>();
            animator = GetComponent<Animator>();
            LockOnTarget = GetComponent<LockOnTarget>();

            moveDirection = Vector3.zero;
        }


        void Update()
        {
            if (characterController.isGrounded)
            {
                moveDirection = Vector3.zero;
                animator.SetBool("jump", false);
            }
            var input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

            if (input.magnitude > 0.1f && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1")
                && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2")
                && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack3")
                                       && (animator.GetBool("HeavyAttack") == false)
                                       && (animator.GetBool("dash") == false))
            {
                float targetAngle = Mathf.Atan2(input.x, input.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;


                animator.SetFloat("vertical", input.magnitude);
                /*moveDirection.x = input.normalized.x * 2;
                moveDirection.z = input.normalized.z * 2;*/
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

            //攻撃
            if (Input.GetButton("Y"))
            {
                
                animator.SetBool("Attack", true);
                //transform.LookAt(LockOnTarget.lookTarget.transform, Vector3.up);

                Quaternion targetRotation = Quaternion.LookRotation(LockOnTarget.lookTarget.transform.position - transform.position, Vector3.up);
                targetRotation.x = 0;
                targetRotation.z = 0;
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotateSpeed);
            }

            if (Input.GetButton("X"))
            {
                animator.SetBool("HeavyAttack", true);
            }

            if (Input.GetButton("B"))
            {
                animator.SetBool("dash", true);
            }


            //ジャンプ
            if (Input.GetButton("A") && characterController.isGrounded)
            {
                animator.SetBool("jump", true);
                moveDirection.y = jumpForce;
            }
            else
            {
                moveDirection += addForceDownPower;
            }
            

            moveDirection.y -= gravity * Time.deltaTime;
            characterController.Move(moveDirection * Time.deltaTime);
        }

        /*void Damage(AttackArea.AttackInfo attackInfo)
        {
            status.HP -= attackInfo.attackPower;
            if(status.HP <= 0)
            {
                status.HP = 0;
            }
            //animator.SetBool("Dead", true);
        }*/

        void attack() { 
        }

    }
}

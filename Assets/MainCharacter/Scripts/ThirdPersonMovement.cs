using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace ActionGame
{
    public class ThirdPersonMovement : MonoBehaviour
    {
        private CharacterController characterController;
        public Transform cam;
        private Animator animator;
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
        public float gravity = 20.0f;

        

        public bool CameraButton()
        {
            return Input.GetButton("");
        }



        void Start()
        {
            characterController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            

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

            if (input.magnitude > 0.1f)
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



            //ジャンプ
            if (Input.GetButton("A") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Jump") && !animator.IsInTransition(0))
            {
                animator.SetBool("jump", true);
                moveDirection.y += jumpForce;
            }
            else
            {
                moveDirection += addForceDownPower;
            }


            /*//ダッシュ回避
            if (Input.GetButton("B"))
            {
                iTween.MoveTo(gameObject, tra, 1.0f);
            }*/



            moveDirection.y -= gravity * Time.deltaTime;
            characterController.Move(moveDirection * Time.deltaTime);
        }


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animator;
    private Vector3 moveDirection;
    //ジャンプ力
    public float jumpForce = 10;
    public bool isGrounded = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        moveDirection = Vector3.zero;
    }

    void Update()
    {
        if (characterController.isGrounded)
        { //地面についているか判定
            if (Input.GetButton("A"))
            {
                moveDirection.y = jumpForce; //ジャンプするベクトルの代入
                isGrounded = false;
            }
        }

        moveDirection.y -= 10 * Time.deltaTime; //重力計算
        characterController.Move(moveDirection * Time.deltaTime); //cubeを動かす処理
    }

    void OnColliderGround(Collider col)
    {
        if(col.gameObject.tag == "Ground")
        {
            if (!isGrounded)
                isGrounded = true;
        }
    }
}
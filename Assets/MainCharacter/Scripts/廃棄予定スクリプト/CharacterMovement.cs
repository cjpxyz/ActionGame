/*
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class CharacterMovement : MonoBehaviour
{
    public CharacterController Controller;
    private Camera cam;
    public float runSpeed = 3f;

    [SerializeField]
    private float changeHorizontal = 4.0f;
    public float gravity = -10;
    public float fallRate;
    public float inputMagnitude;
    public float moveDifferenceX;
    public float moveDifferenceZ;
    public float speedLimit;
    public float normalSpeedLimit = 1;
    public bool normalMove;
    public bool freeSpeed;
    public Vector3 camForward;
    public Vector3 camRight;
    public Vector3 horizontalDirection;
    public Vector3 horizontalVelocity;
    public Vector3 moveMode;


    public void Awake()
    {
        Controller = GetComponent<CharacterController>();
        cam = Camera.main;
        fallRate = gravity;
        normalMove = true;
        freeSpeed = true;
    }


    // Update is called once per frame
    void Update()
    {
        camForward = cam.transform.forward;
        camRight = cam.transform.right;
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        horizontalDirection = ((camForward * InputManager.MainVertical()) + (camRight * InputManager.MainHorizontal())).normalized;
        inputMagnitude = Mathf.Max(Mathf.Abs(InputManager.MainVertical()), Mathf.Abs(InputManager.MainHorizontal()));

        if (freeSpeed) speedLimit = normalSpeedLimit;

        var runInput = inputMagnitude * runSpeed * speedLimit;
        horizontalVelocity = horizontalDirection * runInput;
;
        moveDifferenceZ = Vector3.Dot(Controller.transform.forward, horizontalDirection);
        moveDifferenceX = Vector3.Dot(Controller.transform.right, horizontalDirection);

        var controllerHorizontal = new Vector3(Controller.velocity.x, 0, Controller.velocity.z);
        var horizontalChange = Vector3.Lerp(controllerHorizontal, horizontalVelocity, Time.deltaTime * changeHorizontal);

        var verticalVelocity = transform.up * fallRate;

        if (normalMove == true) moveMode = horizontalChange;
    }
}
*/
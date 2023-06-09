﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 10;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 20;

    private Rigidbody body;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    private InputControls inputControls;
    void Awake()
    {
        inputControls = new InputControls();
        inputControls.Player.Enable();
    }

    void Start()
    {
        // Get the rigidbody on this.
        body = GetComponent<Rigidbody>();
    }

    void Update()//FixedUpdate
    {
        if (PlayerDynamic.Instance.Halt)
            return;

        // Update IsRunning from input.
        //IsRunning = canRun && Input.GetKey(KeyCode.LeftShift);
        float shiftKey = inputControls.Player.Run.ReadValue<float>();
        IsRunning = canRun && (shiftKey > 0.2f);

        // Get targetMovingSpeed.
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        // Get targetVelocity from input.
        //float moveX = Input.GetAxis("Horizontal");
        //float moveY = Input.GetAxis("Vertical");
        Vector2 move = inputControls.Player.Move.ReadValue<Vector2>();
        Vector2 targetVelocity = new Vector2(move.x * targetMovingSpeed, move.y * targetMovingSpeed);

        // Apply movement.
        body.velocity = transform.rotation * new Vector3(targetVelocity.x, body.velocity.y, targetVelocity.y);
    }

}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z3Z
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        float moveForce;

        [SerializeField]
        float jumpForce;

        [SerializeField]
        float gravity;

        [SerializeField]
        float terminalVelocity;

        [SerializeField]
        Gun gun;

        enum MoveState
        {
            Walk,
            Jump
        }

        Vector2 inputVector;
        Vector2 jumpInputVector;

        Vector3 velocity;

        MoveState moveState;
        CharacterController characterController;


        void Awake()
        {
            Initialize();
        }

        void Update()
        {
            InputHandler();
            MoveHandler();
        }

        void Initialize()
        {
            characterController = GetComponent<CharacterController>();
        }

        void InputHandler()
        {
            inputVector.x = Input.GetAxisRaw("Horizontal");
            inputVector.y = Input.GetAxisRaw("Vertical");

            if (inputVector.magnitude > 1.0f)
                inputVector = inputVector.normalized;

            if (inputVector != Vector2.zero)
                jumpInputVector = inputVector;

            moveState = Input.GetButtonDown("Jump") ? MoveState.Jump : MoveState.Walk;

            if (Input.GetButtonDown("Fire1")) {
                gun?.PullTrigger(Camera.main.transform.forward);
            }
        }

        void MoveHandler()
        {
            if (characterController.isGrounded) {
                velocity = (inputVector.x * transform.right) + (inputVector.y * transform.forward);
                velocity *= moveForce;

                if (moveState == MoveState.Jump) {
                    jumpInputVector = inputVector;
                    velocity.y = jumpForce;
                }

                velocity.y = velocity.y - (gravity * Time.deltaTime);
            }
            else {
                Vector3 airboneVelocity = (jumpInputVector.x * transform.right) + (jumpInputVector.y * transform.forward);
                airboneVelocity *= (moveForce * 0.8f);

                airboneVelocity.y = velocity.y;
                airboneVelocity.y -= ((velocity.y * velocity.y) + gravity) * Time.deltaTime;

                velocity = airboneVelocity;
            }

            velocity.y = Mathf.Clamp(velocity.y, -terminalVelocity, jumpForce);
            characterController.Move(velocity * Time.deltaTime);
        }
    }
}

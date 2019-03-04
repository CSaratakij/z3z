using System.Collections;
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

        enum MoveState
        {
            Walk,
            Jump
        }

        Vector2 inputVector;
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

            if (inputVector.magnitude > 1.0f) {
                inputVector = inputVector.normalized;
            }

            moveState = Input.GetButtonDown("Jump") ? MoveState.Jump : MoveState.Walk;
        }

        void MoveHandler()
        {
            if (characterController.isGrounded) {
                velocity = (inputVector.x * transform.right) + (inputVector.y * transform.forward);
                velocity *= moveForce;

                if (moveState == MoveState.Jump)
                    velocity.y = jumpForce;

                velocity.y = velocity.y - (gravity * Time.deltaTime);
            }
            else {
                Vector3 airboneVelocity = (inputVector.x * transform.right) + (inputVector.y * transform.forward);
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

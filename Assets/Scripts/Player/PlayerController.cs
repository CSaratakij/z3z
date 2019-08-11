using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z3Z
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        float airboneSlowrate;

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

        [SerializeField]
        GunAimSight gunAimSight;

        enum MoveState
        {
            Walk,
            Jump,
            InstantJump,
            Idle
        }

        float instantJumpForce;

        bool isPressPullTrigger;
        bool isPreventControlAirboneMovement;
        bool isFallingDown;

        Vector2 inputVector;
        Vector2 jumpInputVector;
        Vector2 instantInputVector;

        Vector3 velocity;
        Vector3 airboneVelocity;
        Vector3 instantJumpVelocity;

        MoveState moveState;
        CharacterController characterController;


        void Awake()
        {
            Initialize();
            SubscribeEvent();
        }

        //Test
        void Start()
        {
            GameController.GameStart();
        }

        void Update()
        {
            InputHandler();
            MoveHandler();
        }

        void OnDestroy()
        {
            UnsubscribeEvent();
        }

        void Initialize()
        {
            characterController = GetComponent<CharacterController>();
        }

        void InputHandler()
        {
            if (GameController.IsGamePause)
                return;

            if (moveState == MoveState.Idle) {
                inputVector = Vector2.zero;
                return;
            }

            inputVector.x = Input.GetAxisRaw("Horizontal");
            inputVector.y = Input.GetAxisRaw("Vertical");

            if (inputVector.magnitude > 1.0f)
                inputVector = inputVector.normalized;

            if (inputVector != Vector2.zero)
                jumpInputVector = inputVector;

            isPressPullTrigger = Input.GetButtonDown("Fire1");

            if (isPressPullTrigger) {
                gun?.PullTrigger();
            }

            if (Input.GetButtonDown("Fire2")) {
                gunAimSight?.ToggleSight();
            }

            /* gunAimSight?.Aim(Input.GetButton("Fire2")); */

            if (moveState == MoveState.InstantJump)
                return;

            if (moveState != MoveState.Idle)
                moveState = Input.GetButtonDown("Jump") ? MoveState.Jump : MoveState.Walk;
        }

        void MoveHandler()
        {
            if (moveState == MoveState.Idle)
                return;

            if (characterController.isGrounded) {
                velocity = (inputVector.x * transform.right) + (inputVector.y * transform.forward);
                velocity *= moveForce;

                if (moveState == MoveState.Jump) {
                    jumpInputVector = inputVector;
                    velocity.y = jumpForce;
                }

                if (moveState == MoveState.InstantJump) {
                    jumpInputVector = Vector3.zero;
                    velocity = instantJumpVelocity;
                }

                velocity.y = velocity.y - (gravity * Time.deltaTime);
            }
            else {
                if (isPreventControlAirboneMovement) {
                    airboneVelocity = instantJumpVelocity;
                }
                else {
                    airboneVelocity = (jumpInputVector.x * transform.right) + (jumpInputVector.y * transform.forward);
                    airboneVelocity *= moveForce;

                    if (inputVector == Vector2.zero) {
                        jumpInputVector = Vector3.Lerp(jumpInputVector, Vector3.zero, airboneSlowrate);
                    }
                }

                airboneVelocity.y = velocity.y;
                airboneVelocity.y -= (gravity * Time.deltaTime);

                velocity = airboneVelocity;
            }

            characterController.Move(velocity * Time.deltaTime);

            if (moveState == MoveState.InstantJump)
                moveState = MoveState.Walk;

            if (velocity.y < 0.0f && isPreventControlAirboneMovement) {
                isFallingDown = true;
            }

            if (isFallingDown && velocity.y < (instantJumpVelocity.y * -0.8f)) {
                jumpInputVector = Vector2.up;
                isPreventControlAirboneMovement = false;
            }
        }

        void SubscribeEvent()
        {
            GameController.OnGameStart += OnGameStart;
            GameController.OnGameOver += OnGameOver;
        }

        void UnsubscribeEvent()
        {
            GameController.OnGameStart -= OnGameStart;
            GameController.OnGameOver -= OnGameOver;
        }

        void OnGameStart()
        {
            moveState = MoveState.Walk;
        }

        void OnGameOver()
        {
            moveState = MoveState.Idle;
        }

        internal void InstantJump(Vector3 velocity)
        {
            moveState = MoveState.InstantJump;
            instantJumpVelocity = velocity;
            isFallingDown = false;
            isPreventControlAirboneMovement = true;
        }
    }
}


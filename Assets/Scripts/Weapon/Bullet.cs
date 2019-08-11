using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z3Z
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField]
        float moveForce;

        [SerializeField]
        Color bulletColor;

        [SerializeField]
        float disableDelay;


        float disableTime;

        Vector3 moveDirection;
        Vector3 velocity;

        Rigidbody rigid;
        Material material;

        TrailRenderer trailRenderer;

        void Awake()
        {
            Initialize();
        }

        void OnEnable()
        {
            disableTime = Time.time + disableDelay;

            if (trailRenderer == null)
                return;

            trailRenderer.emitting = true;
        }

        void OnDisable()
        {
            if (trailRenderer == null)
                return;

            trailRenderer.emitting = false;
            trailRenderer.Clear();
        }

        void Initialize()
        {
            rigid = GetComponent<Rigidbody>();
            material = GetComponent<Renderer>().material;
            material.color = bulletColor;
            trailRenderer = GetComponent<TrailRenderer>();
        }

        void FixedUpdate()
        {
            MoveHandler();
        }

        void LateUpdate()
        {
            RotateHandler();
            LifeTimeHandler();
        }

        void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.CompareTag("Gun") || collider.gameObject.CompareTag("Bullet"))
                return;

            gameObject.SetActive(false);
        }

        void MoveHandler()
        {
            velocity = (moveDirection * moveForce) * Time.fixedDeltaTime;
            rigid.MovePosition(rigid.position + velocity);
        }

        void RotateHandler()
        {
            transform.Rotate(Vector3.forward * 360.0f * Time.deltaTime);
        }

        void LifeTimeHandler()
        {
            if (Time.time > disableTime && gameObject.activeSelf)
                gameObject.SetActive(false);
        }

        internal void SetMovDirection(Vector3 value)
        {
            moveDirection = value;
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }
    }
}


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
        string ignoreTag;

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
            transform.Rotate(Vector3.forward * 360.0f * Time.deltaTime);

            if (Time.time > disableTime && gameObject.activeSelf)
                gameObject.SetActive(false);
        }

        void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.CompareTag(ignoreTag))
                return;

            gameObject.SetActive(false);
        }

        void MoveHandler()
        {
            velocity = (moveDirection * moveForce) * Time.fixedDeltaTime;
            rigid.MovePosition(rigid.position + velocity);
        }

        public void SetMovDirection(Vector3 value)
        {
            moveDirection = value;
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }
    }
}

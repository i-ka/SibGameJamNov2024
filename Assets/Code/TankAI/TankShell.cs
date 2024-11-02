using SibGameJam.Health;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SibGameJam.TankAI
{
    public class TankShell : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float lifeTime;
        [SerializeField] private int damage;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Collider collider;

        private bool canDamage = false;


        private void Update()
        {
            if (rb.velocity.magnitude > speed)
            {
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, speed);
            }
        }

        private void FixedUpdate()
        {
            rb.velocity = transform.forward * speed;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (canDamage == false) return;
            canDamage = false;

            if (collision.gameObject.TryGetComponent(out HealthController healthObject))
            {
                healthObject.ReduceHealth(damage);
            }

            Deactivate();

        }

        private void OnEnable()
        {
            canDamage = true;
            gameObject.SetActive(true);
            if (collider) collider.enabled = true;
            Invoke(nameof(Deactivate), lifeTime);
        }

        private void OnDisable()
        {
            if (collider) collider.enabled = false;
        }

        private void Deactivate()
        {
            CancelInvoke();
            gameObject.SetActive(false);
        }
    }

}


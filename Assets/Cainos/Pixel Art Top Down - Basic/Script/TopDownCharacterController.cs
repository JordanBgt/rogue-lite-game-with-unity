using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    public class TopDownCharacterController : MonoBehaviour
    {
        private float speed = BaseStats.speed;
        private float shotSpeed = BaseStats.shotSpeed;
        private float fireRate = BaseStats.fireRate;
        private float range = BaseStats.range;
        private float damage = BaseStats.damage;
        private float maxHealth = BaseStats.health;
        private float resistance = BaseStats.resistance;
        private float timeInvincibility = BaseStats.timeInvincibility;
        private Animator animator;
        private float currentHealth;
        private bool isInvincible;
        private float invicibilityTimer;

        private void Start()
        {
            animator = GetComponent<Animator>();
            currentHealth = maxHealth;
        }


        private void Update()
        {
            Vector2 dir = Vector2.zero;
            if (Input.GetKey(KeyCode.Q))
            {
                dir.x = -1;
                animator.SetInteger("Direction", 3);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                dir.x = 1;
                animator.SetInteger("Direction", 2);
            }

            if (Input.GetKey(KeyCode.Z))
            {
                dir.y = 1;
                animator.SetInteger("Direction", 1);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                dir.y = -1;
                animator.SetInteger("Direction", 0);
            }

            dir.Normalize();
            animator.SetBool("IsMoving", dir.magnitude > 0);

            GetComponent<Rigidbody2D>().velocity = speed * dir;
        }

         public void ChangeHealth(int amount)
        {
            if (amount < 0)
            {
                if (isInvincible)
                {
                    return;
                }

                isInvincible = true;
                invicibilityTimer = timeInvincibility;
            }

            currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        }

        private void handleInvincibilityTimer()
        {
            if (isInvincible)
            {
                invicibilityTimer -= Time.deltaTime;
                if (invicibilityTimer < 0)
                {
                    isInvincible = false;
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    public class TopDownCharacterController : MonoBehaviour
    {

        public GameObject bulletPrefab;
        
        private float speed = BaseStats.speed;
        private float shotSpeed = BaseStats.shotSpeed;
        private float fireRate = BaseStats.fireRate;
        private float range = BaseStats.range;
        private float damage = BaseStats.damage;
        private float maxHealth = BaseStats.health;
        private float resistance = BaseStats.resistance;
        private float timeInvincibility = BaseStats.timeInvincibility;
        
        private Animator animator;
        private Rigidbody2D rigidbody2D;
        
        private float currentHealth;
        private bool isInvincible;
        private float invincibilityTimer;
        private bool isShooting;
        private float shootingTimer;
        private Vector2 lookingDir = new Vector2(0, -1);

        private void Start()
        {
            animator = GetComponent<Animator>();
            rigidbody2D = GetComponent<Rigidbody2D>();
            currentHealth = maxHealth;
        }
        
        private void Update()
        {
            Vector2 dir = Vector2.zero;
            if (Input.GetKey(KeyCode.Q))
            {
                dir.x = -1;
                lookingDir = dir;
                animator.SetInteger("Direction", 3);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                dir.x = 1;
                lookingDir = dir;
                animator.SetInteger("Direction", 2);
            }

            if (Input.GetKey(KeyCode.Z))
            {
                dir.y = 1;
                lookingDir = dir;
                animator.SetInteger("Direction", 1);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                dir.y = -1;
                lookingDir = dir;
                animator.SetInteger("Direction", 0);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
            }

            HandleInvincibilityTimer();
            HandleShootingTime();
            dir.Normalize();
            animator.SetBool("IsMoving", dir.magnitude > 0);

            rigidbody2D.velocity = speed * dir;
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
                invincibilityTimer = timeInvincibility;
            }

            currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        }

        private void HandleInvincibilityTimer()
        {
            if (!isInvincible) return;
            invincibilityTimer -= Time.deltaTime;
            if (invincibilityTimer < 0)
            {
                isInvincible = false;
            }
        }

        public void Shoot()
        {
            if (!isShooting)
            {
                isShooting = true;
                shootingTimer = 1.5f; // TODO : use fireRate stat
                GameObject bulletObject =
                    Instantiate(bulletPrefab, rigidbody2D.position + Vector2.up * 0.5f, Quaternion.identity);
                Bullet bullet = bulletObject.GetComponent<Bullet>();
                bullet.Shoot(lookingDir, shotSpeed * 300, range);
            }
        }

        private void HandleShootingTime()
        {
            if (!isShooting) return;
            shootingTimer -= Time.deltaTime;
            if (shootingTimer < 0)
            {
                isShooting = false;
            }
        }
    }
}

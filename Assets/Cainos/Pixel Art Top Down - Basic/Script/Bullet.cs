using System;
using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private float range = 500f;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (transform.position.magnitude > range)
        {
            Debug.Log("Bullet destroyed");
            Destroy(gameObject);
        }
    }

    public void Shoot(Vector2 direction, float force, float playerRange)
    {
        range *= playerRange;
        Debug.Log("Range " +range);
        rigidbody2D.AddForce(direction * force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // TODO : implement
        Destroy(gameObject);
    }
}

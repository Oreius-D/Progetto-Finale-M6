using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float lifetime = 1f;
    [SerializeField] private int damage = 1;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime);
    }

    public void Init(Vector2 direction, float bulletSpeed)
    {
        Vector2 dir = direction.normalized;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);

        rb.velocity = dir * bulletSpeed;
    }

    public void SetDamage(int newDamage) => damage = newDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var dmg = collision.GetComponent<IDamageable>();
        if (dmg != null)
        {
            dmg.TakeDamage(damage);
            Destroy(gameObject);
        }
        // Se vuoi che muoia anche su muri/ostacoli, aggiungi un layer check qui
    }
}

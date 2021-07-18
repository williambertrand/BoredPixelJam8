using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public int damage;
    public bool shouldPoint = true;
    public bool isPlayerShot;

    public bool hasDrop;
    public string dropName; // Should match the ObjectPool name for whichever object we want to drop on collision

    Rigidbody2D rigidbody2D;

    [Tooltip("How long can this projectile last")]
    public float MaxLifeTime;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        Destroy(gameObject, MaxLifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldPoint)
        {
            float angle = Mathf.Atan2(rigidbody2D.velocity.y, rigidbody2D.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isPlayerShot)
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Enemy") && isPlayerShot)
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == 8) //TODO: Layer constants...
        {
            Destroy(gameObject);
        }
        else
        {
            Damageable d = collision.gameObject.GetComponent<Damageable>();
            if (d)
            {
                d.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
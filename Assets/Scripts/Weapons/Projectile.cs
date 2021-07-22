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

    [FMODUnity.EventRef]
    public string HitEvent;
    [FMODUnity.EventRef]
    public string HitEnemyEvent;
    FMOD.Studio.EventInstance PE;

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

    private void OnEnable()
    {
        if(HitEvent != null)
        {
            PE = FMODUnity.RuntimeManager.CreateInstance(HitEvent);
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(PE, transform, GetComponent<Rigidbody2D>());
            PE.release();
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
            //FMODUnity.RuntimeManager.PlayOneShotAttached(HitGroundEvent, gameObject);
            Destroy(gameObject);
        }
        else
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached(HitEnemyEvent, gameObject);
            Damageable d = collision.gameObject.GetComponent<Damageable>();
            if (d)
            {
                d.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
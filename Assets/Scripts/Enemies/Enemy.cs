using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Common Enemy Fields")]
    [Tooltip("How much to add to players bounty on killing")]
    public int bountyAmount;

    [Tooltip("Enemy Health")]
    public int maxHealth;
    public int currentHealth;

    [Tooltip("Ammo to drop on death")]
    public int ammoAmount = 1;

    protected Animator anim;
    protected bool isDead = false;


    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            OnDeath();
        }
    }

    public void OnDeath()
    {
        DropAmmo();
        isDead = true;
        anim.SetFloat("move", 0.0f);
        anim.SetTrigger("death");
        EnemyManager.Instance.OnEnemyDeath(this);
        StartCoroutine(DestorySelf());
    }

    IEnumerator DestorySelf()
    {
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }

    public void DropAmmo()
    {
        for (int i = 0; i < ammoAmount; i++)
        {
            GameObject ammo = Instantiate(LevelPrefabs.Instance.AmmoPickup);
            ammo.transform.position = transform.position;

            Rigidbody2D ammoRB = ammo.GetComponent<Rigidbody2D>();

            Vector2 randPopVel = new Vector2(
                Random.Range(-1.5f, 1.5f),
                Random.Range(0, 1.5f)
            );
            ammoRB.velocity = randPopVel;
        }
    }
}

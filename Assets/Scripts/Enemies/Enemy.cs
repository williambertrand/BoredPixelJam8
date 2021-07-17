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
        EnemyManager.Instance.OnEnemyDeath(this);
    }
}

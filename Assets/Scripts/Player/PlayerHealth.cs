using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public bool isPlayer;
    public int maxHealth;
    public int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            //TODO: Handle OnGameOver
        }
        if(isPlayer)
            CameraEffects.Instance.CameraShake.Shake(0.425f, 0.2f);
    }
}

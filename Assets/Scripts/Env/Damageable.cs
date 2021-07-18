using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public int hitPoints;

    public void TakeDamage(int amount)
    {
        hitPoints -= amount;
        if (hitPoints <= 0)
        {
            // TODO: Can add destroy anmation here...
            Destroy(gameObject);
        }
    }
}

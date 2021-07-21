using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    public bool isPlayer;
    public int maxHealth;
    public int currentHealth;

    public List<Sprite> healthImages;
    public Image healthImage;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthImage.sprite = healthImages[healthImages.Count - 1];
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            healthImage.enabled = false;
            GameManager.Instance.OnGameOver();
        }
        else
        {
            UpdateHealthImage();
        }
        if(isPlayer)
            CameraEffects.Instance.CameraShake.Shake(0.425f, 0.2f);
    }

    private void UpdateHealthImage()
    {
        healthImage.sprite = healthImages[currentHealth - 1];
    }
}

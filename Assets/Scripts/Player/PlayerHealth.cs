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

    [FMODUnity.EventRef]
    public string OnHitEvent;
    FMOD.Studio.EventInstance PE;

    private void OnEnable()
    {
        PE = FMODUnity.RuntimeManager.CreateInstance(OnHitEvent);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(PE, transform, GetComponent<Rigidbody2D>());
        PE.release();
    }

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
            //FMODUnity.RuntimeManager.PlayOneShotAttached(OnGameOverEvent, gameObject);
        }
        else
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached(OnHitEvent, gameObject);
            UpdateHealthImage();
        }
        if(isPlayer)
            CameraEffects.Instance.CameraShake.Shake(0.35f, 0.15f);
    }

    private void UpdateHealthImage()
    {
        healthImage.sprite = healthImages[currentHealth - 1];
    }
}

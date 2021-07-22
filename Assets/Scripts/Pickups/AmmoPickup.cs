using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int amount;

    [FMODUnity.EventRef]
    public string PickupSoundEvent;
    FMOD.Studio.EventInstance PE;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached(PickupSoundEvent, gameObject);
            collision.gameObject.GetComponent<PlayerInventory>().AddAmmo(amount);
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        PE = FMODUnity.RuntimeManager.CreateInstance(PickupSoundEvent);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(PE, transform, GetComponent<Rigidbody2D>());
        PE.release();
    }

}

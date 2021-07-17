using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{

    public static PlayerInventory Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public int startingAmmo;
    public int ammoAmount;
    public int maxAmmo;
    public TMP_Text ammoText;

    // Start is called before the first frame update
    void Start()
    {
        ammoAmount = startingAmmo;
    }

    void UpdateAmmoText()
    {
        ammoText.text = "Ammo: " + ammoAmount + "/" + maxAmmo;
    }

    public void AddAmmo(int amount)
    {
        ammoAmount += amount;
        if(ammoAmount > maxAmmo)
        {
            ammoAmount = maxAmmo;
        }
        UpdateAmmoText();
    }

    // Return false if player does not have any ammo
    public bool ExpendAmmo()
    {
        ammoAmount -= 1;
        if(ammoAmount < 0)
        {
            ammoAmount = 0;
            UpdateAmmoText();
            return false;
        }
        UpdateAmmoText();
        return true;
    }
}

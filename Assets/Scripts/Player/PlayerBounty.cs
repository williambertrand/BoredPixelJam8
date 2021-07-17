using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerBounty : MonoBehaviour
{

    public int currentBounty;
    public int startBounty;
    public TMP_Text bountyText;

    #region Singleton
    public static PlayerBounty Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        currentBounty = startBounty;
    }

    public void AddBounty(int amount)
    {
        currentBounty += amount;
        UpdateBountyText();
    }

    private void UpdateBountyText()
    {
        this.bountyText.text = "Bounty: $" + currentBounty;
    }
}

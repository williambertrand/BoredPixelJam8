using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPrefabs : MonoBehaviour
{
    #region Singleton
    public static LevelPrefabs Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    #endregion

    public GameObject AmmoPickup;
}
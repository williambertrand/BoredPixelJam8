using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffects : MonoBehaviour
{

    #region Singleton
    public static CameraEffects Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        CameraShake = FindObjectOfType<CameraShake>();
    }
    #endregion


    // Add public references to any camera effects to be used here.
    public CameraShake CameraShake;
}
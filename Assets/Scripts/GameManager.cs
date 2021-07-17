using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = new GameObject("CameraEffects");
        obj.AddComponent<CameraEffects>();
        obj.transform.parent = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

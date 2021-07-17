using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Parallax : MonoBehaviour
{
    public float length;
    public float startPos;
    public GameObject cam;
    public float parallaxFactor;


    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        // How far has it moved relative to camera
        float moveAmount = cam.transform.position.x * (1 - parallaxFactor);
        float dist = (cam.transform.position.x * parallaxFactor);
        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

        if (moveAmount > startPos + length) startPos += length;
        else if (moveAmount < startPos - length) startPos -= length;
    }
}

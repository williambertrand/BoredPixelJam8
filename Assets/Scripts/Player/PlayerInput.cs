using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    #region Singleton
    public static PlayerInput Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    #endregion

    public PlayerMovement movement;

    public float moveSpeed;
    public float horizontalMove = 0;
    bool jump = false;
    public bool crouch = false;

    [Header("Player weapon input")]
    private Vector3 mousePos;
    public Transform playerGun; //Assign to the object you want to rotate
    private Vector3 objectPos;
    private float angle;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (GameManager.Instance.isGameOver) return;

        horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            PlayerWeaponController.Instance.AttemptFire();
        }

        UpdateGunRot();

    }

    private void FixedUpdate()
    {
        // Move character
        movement.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }


    void UpdateGunRot()
    {
        mousePos = Input.mousePosition;
        //mousePos.z = 5.23; //The distance between the camera and object
        objectPos = Camera.main.WorldToScreenPoint(playerGun.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;
        angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        if (movement.isFacingRight)
            angle -= 180;
        playerGun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        movement.LookAt(mousePos);
    }
}
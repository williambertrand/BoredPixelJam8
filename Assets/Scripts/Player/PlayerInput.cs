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

    }

    private void FixedUpdate()
    {
        // Move character
        movement.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
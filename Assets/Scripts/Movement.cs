using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [HideInInspector]
    public Controls playerControls;
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //playerControls = new Controls();
        //playerControls.PlayerControls.Movement.performed += ctx => UpdateSpeed(ctx.ReadValue<Vector2>());
        //playerControls.PlayerControls.Movement.canceled += ctx => UpdateSpeed(Vector2.zero);
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        rb.MovePosition(rb.position + rb.velocity);
    }

    public void UpdateSpeed(InputAction.CallbackContext ctx)
    {
        //movementDirection = newSpeed;
        rb.velocity = ctx.ReadValue<Vector2>();
    }



}

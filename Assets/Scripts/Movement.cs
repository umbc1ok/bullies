using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [HideInInspector] public InputActionMap playerControls;
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerControls = GetComponent<PlayerInput>().currentActionMap;
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
        rb.velocity = ctx.ReadValue<Vector2>();
    }



}

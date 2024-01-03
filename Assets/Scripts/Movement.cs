using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [NonSerialized] public InputActionMap playerControls;

    private new Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        playerControls = GetComponent<PlayerInput>().currentActionMap;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rigidbody.MovePosition(rigidbody.position + rigidbody.velocity);
    }

    public void UpdateSpeed(InputAction.CallbackContext callbackContext)
    {
        rigidbody.velocity = callbackContext.ReadValue<Vector2>();
    }
}

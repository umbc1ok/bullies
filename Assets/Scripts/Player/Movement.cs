using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed;
    InputSetup inputSetup;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inputSetup = GetComponent<InputSetup>();
        inputSetup.OnLeftAnalogInput += UpdateSpeed;
    }

    void UpdateSpeed(Vector2 leftStickValue)
    {
        rb.velocity = leftStickValue * speed;
        rb.MovePosition(rb.position + rb.velocity);
    }

    void OnDestroy()
    {
        inputSetup.OnLeftAnalogInput -= UpdateSpeed;
    }
}

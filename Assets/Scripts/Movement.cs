using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Controls playerControls;
    Transform transform;
    Vector2 movementDirection;

    void Awake()
    {
        transform = GetComponent<Transform>();
        playerControls = new Controls();
        playerControls.PlayerControls.Movement.performed += ctx => Move(ctx.ReadValue<Vector2>());
        //playerControls.PlayerControls.Movement.canceled += ctx => movementDirection = Vector2.zero;
    }


    void Move(Vector2 movement)
    {
        transform.position += new Vector3(movement.x, movement.y, 0);
    }


    void OnEnable()
    {
        playerControls.Enable();
    }
    void OnDisable()
    {
        playerControls.Disable();
    }

}

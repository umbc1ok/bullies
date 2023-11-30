using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Throwing : MonoBehaviour
{

    [SerializeField] private Ball ball;
    [SerializeField] private GameObject arrow;
    private Vector2 throwDirection;
    [SerializeField]
    private float throwForce = 10f;



    // let the script know that It can start pointing the arrow
    public void GrabBall(Ball _ball)
    {
        ball = _ball;
        arrow.SetActive(true);
    }

    void Start()
    {
        arrow.SetActive(true);
        ball = null;
    }

    // this function is added to the PlayerInput event called "Aim"
    public void UpdateDirection(InputAction.CallbackContext ctx)
    {
        throwDirection = ctx.ReadValue<Vector2>();
        Debug.Log(throwDirection);
    }

    // This will be called on the PlayerInput event called "Throw"
    public void Throw(InputAction.CallbackContext ctx)
    {
        ball.ThrowMe(throwDirection,throwForce);
    }


    void FixedUpdate()
    {
        if(arrow.activeSelf)
        {
            Quaternion arrowRotation = Quaternion.Euler(0, 0, Mathf.Atan2(throwDirection.y, throwDirection.x) * Mathf.Rad2Deg);
            arrow.transform.rotation = arrowRotation;
        }
    }
}

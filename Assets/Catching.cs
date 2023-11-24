using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Catching : MonoBehaviour
{
    Ball ball;

    public void Awake()
    {
        // idk if i should use it but idc
        ball = FindFirstObjectByType<Ball>();
    }

    public void Catch(InputAction.CallbackContext ctx)
    {
        ball.GetCaught(this.gameObject);
    }
}

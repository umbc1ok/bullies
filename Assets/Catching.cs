using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Catching : MonoBehaviour
{
    Catchable ball;

    public void Awake()
    {
        // idk if i should use it but idc
        ball = FindFirstObjectByType<Catchable>();
    }

    public void Catch(InputAction.CallbackContext ctx)
    {
        ball.GetCaught(this.gameObject);
    }
}

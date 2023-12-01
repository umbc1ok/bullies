using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : Catchable
{
    [SerializeField] private Rigidbody2D rb;
    protected override void Catch()
    {
        base.Catch();
        Debug.Log("caught");
    }

    protected override void Release()
    {
        base.Release();
        Debug.Log("released");
    }
    
    public void ThrowMe(Vector2 direction, float throwForce)
    {
        rb.AddForce(direction * throwForce, ForceMode2D.Impulse);
    }

    private void Update()
    {
        if (isCaught)
            transform.position = holder.transform.position;
    }
}

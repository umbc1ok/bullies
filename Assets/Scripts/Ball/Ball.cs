using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : Catchable
{
    
    protected override void Catch(GameObject catching)
    {
        base.Catch(catching);
        isCaught = true;
        GetComponent<CircleCollider2D>().enabled = false;
        Debug.Log("caught");
    }

    protected override void Release(GameObject releasing)
    {
        base.Release(releasing);
        isCaught = false;
        GetComponent<CircleCollider2D>().enabled = true;
        Debug.Log("released");
    }

    private void Update()
    {
        if (isCaught)
            transform.position = catchingObject.transform.position;
    }
}

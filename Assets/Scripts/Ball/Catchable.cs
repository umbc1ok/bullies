using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Catchable : MonoBehaviour
{
    [SerializeField] private float catchRange = 5;
    public bool isCaught { get; protected set; }
    protected GameObject catchingObject;
    private CircleCollider2D playerDetector;

    void Start()
    {
        playerDetector = transform.GetChild(0).GetComponent<CircleCollider2D>();
        playerDetector.radius = catchRange;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // Check if it's player. TODO: Check WHICH player is that, to prevent bugs like releasing not your catchable
        if (!col.gameObject.CompareTag("Player")) return;
        var controls = col.GetComponent<Movement>().playerControls;
        controls["PickUpBall"].performed += Callback;
        catchingObject = col.gameObject;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        // Check if it's player. TODO: Check WHICH player is that, to prevent bugs like releasing not your catchable
        if (!col.gameObject.CompareTag("Player")) return;
        var controls = col.GetComponent<Movement>().playerControls;
        controls["PickUpBall"].performed -= Callback;
        catchingObject = null;
    }

    private void Callback(InputAction.CallbackContext ctx)
    {
        if(!isCaught)
            Catch(catchingObject);
        else
            Release(catchingObject);
    }
    
    protected virtual void Catch(GameObject catching) {}
    protected virtual void Release(GameObject releasing) {}
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Catchable : MonoBehaviour
{
    [SerializeField] private float catchRange = 5;
    public bool isCaught { get; protected set; }
    protected GameObject playerInRange;
    protected int holdingPlayerDeviceID;
    protected GameObject holdingPlayer;
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
        playerInRange = col.gameObject;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        // Check if it's player. TODO: Check WHICH player is that, to prevent bugs like releasing not your catchable
        if (!col.gameObject.CompareTag("Player")) return;
        var controls = col.GetComponent<Movement>().playerControls;
        controls["PickUpBall"].performed -= Callback;
        playerInRange = null;
    }

    private void Callback(InputAction.CallbackContext ctx)
    {
        if (!isCaught)
        {
            holdingPlayerDeviceID = ctx.control.device.deviceId;
            holdingPlayer = playerInRange;
            Catch(holdingPlayer);
            Debug.Log(holdingPlayerDeviceID);
        }
        else if(holdingPlayerDeviceID == ctx.control.device.deviceId)
        {
            Debug.Log(holdingPlayerDeviceID);
            Release(playerInRange);
            holdingPlayer = null;
            holdingPlayerDeviceID = -1;
        }
    }

    protected virtual void Catch(GameObject catching)
    {
        isCaught = true;
        GetComponent<CircleCollider2D>().enabled = false;
    }

    protected virtual void Release(GameObject releasing)
    {
        isCaught = false;
        GetComponent<CircleCollider2D>().enabled = true;
    }
}

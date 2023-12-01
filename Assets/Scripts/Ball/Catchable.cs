using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Catchable : MonoBehaviour
{
    [SerializeField] private float catchRange = 5;
    public bool isCaught { get; protected set; }
    protected GameObject holder;
    private CircleCollider2D playerDetector;
    private List<InputSetup> playersInRange = new(); // Of type InputSetup for convenience 

    void Start()
    {
        playerDetector = transform.GetChild(0).GetComponent<CircleCollider2D>();
        playerDetector.radius = catchRange;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // Check if it's player. TODO: Check WHICH player is that, to prevent bugs like releasing not your catchable
        if (!col.gameObject.CompareTag("Player")) return;
        var input = col.GetComponent<InputSetup>();
        playersInRange.Add(col.GetComponent<InputSetup>());
        input.OnRightTriggerPressed += Callback;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        // Check if it's player. TODO: Check WHICH player is that, to prevent bugs like releasing not your catchable
        if (!col.gameObject.CompareTag("Player")) return;
        var input = col.GetComponent<InputSetup>();
        playersInRange.Remove(col.GetComponent<InputSetup>());
        input.OnRightTriggerPressed -= Callback;
    }

    private void Callback(int playerID)
    {
        var matchingInput = playersInRange.FirstOrDefault(input => input.GetPlayerID() == playerID);

        if (matchingInput != null)
        {
            if (isCaught)
            {
                Release();
                holder = null;
            }
            else
            {
                holder = matchingInput.gameObject;
                Catch();
            }
        }
    }

    protected virtual void Catch()
    {
        isCaught = true;
        GetComponent<CircleCollider2D>().enabled = false;
    }

    protected virtual void Release()
    {
        isCaught = false;
        GetComponent<CircleCollider2D>().enabled = true;
    }
}
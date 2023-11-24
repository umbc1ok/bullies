using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Catchable : MonoBehaviour
{
    [SerializeField] private float catchRange = 5;
    public List<GameObject> objectsInRange;
    public GameObject catcher;
    private CircleCollider2D playerDetector;
    bool isCaught = false;

    void Start()
    {
        playerDetector = transform.GetChild(0).GetComponent<CircleCollider2D>();
        playerDetector.radius = catchRange;
    }

    public void GetCaught(GameObject catcherCandidate)
    {
        if (objectsInRange.Contains(catcher) && catcher == null)
        {
            catcher = catcherCandidate;
            isCaught = true;
            GetComponent<CircleCollider2D>().enabled = false;
        }
        if(catcher == catcherCandidate)
        {
            isCaught=false;
            catcher = null;
            GetComponent<CircleCollider2D>().enabled = true;
        }
    }

    void FixedUpdate()
    {
        if (isCaught)
            transform.position = catcher.transform.position;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        // Check if it's player. TODO: Check WHICH player is that, to prevent bugs like releasing not your catchable
        if (col.gameObject.CompareTag("Player"))
        {
            if (!objectsInRange.Contains(col.gameObject))
            {
                objectsInRange.Add(gameObject); 
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (objectsInRange.Contains(col.gameObject))
            {
                objectsInRange.Remove(gameObject);
            }
        }
    }


}

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
        if (objectsInRange.Contains(catcherCandidate) && catcher == null)
        {
            catcher = catcherCandidate;
            isCaught = true;
            GetComponent<CircleCollider2D>().enabled = false;
        }
        else if(catcher == catcherCandidate)
        {
            GetComponent<CircleCollider2D>().enabled = true;
            isCaught = false;
            catcher = null;
        }
    }

    void FixedUpdate()
    {
        if (isCaught)
            transform.position = catcher.transform.position;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (!objectsInRange.Contains(col.gameObject))
            {
                objectsInRange.Add(col.gameObject); 
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (objectsInRange.Contains(col.gameObject))
            {
                objectsInRange.Remove(col.gameObject);
            }
        }
    }


}

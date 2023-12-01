using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Player : MonoBehaviour
{
    public int playerID;
    public Catchable caughtObject;

    [NonSerialized] public new Rigidbody2D rigidbody;
    [NonSerialized] public ColliderList colliderList;

    [SerializeField] private List<Catchable> catchablesInReach = new List<Catchable>();

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        if (colliderList == null)
        {
            colliderList = GetComponent<ColliderList>();
            Assert.IsNotNull(colliderList);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rigidbody.MovePosition(rigidbody.position + rigidbody.velocity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.attachedRigidbody == null)
            return;

        Catchable catchable = collision.attachedRigidbody.GetComponent<Catchable>();
        if (catchable != null)
            catchablesInReach.Add(catchable);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.attachedRigidbody == null)
            return;

        Catchable catchable = collision.attachedRigidbody.GetComponent<Catchable>();
        if (catchable != null)
            OnCatchableLeave(catchable);
    }

    public void OnCatchableLeave(Catchable catchable)
    {
        catchablesInReach.Remove(catchable);
    }

    public void Catch()
    {
        // If we have an object caught, release it and return
        if (caughtObject != null)
        {
            caughtObject.Release(this);
            return;
        }

        // Otherwise, try to catch the closest object in reach
        Catchable closestCatchable = null;
        float closestDistance = float.MaxValue;
        foreach (var catchable in catchablesInReach)
        {
            if (catchable.IsCaught)
                continue;

            float distance = Vector2.Distance(transform.position, catchable.transform.position);
            if (distance < closestDistance)
            {
                closestCatchable = catchable;
                closestDistance = distance;
            }
        }

        if (closestCatchable != null)
            closestCatchable.Catch(this);
    }

    public void Move(Vector2 velocity)
    {
        rigidbody.velocity = velocity;
    }
}

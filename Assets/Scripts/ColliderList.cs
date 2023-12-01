using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ColliderList : MonoBehaviour
{
    public List<Collider2D> colliders = new List<Collider2D>();

    private new Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        Assert.IsNotNull(rigidbody);

        Collider2D[] attachedColliders = new Collider2D[rigidbody.attachedColliderCount];
        rigidbody.GetAttachedColliders(attachedColliders);

        foreach (var collider in attachedColliders)
        {
            if (!collider.isTrigger)
                colliders.Add(collider);
        }
    } 
}

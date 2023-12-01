using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class Catchable : MonoBehaviour
{
    public Player caughtBy;

    [NonSerialized] public ColliderList colliderList;

    public bool IsCaught { get; private set; }

    [SerializeField] private float catchRange = 5;
    [SerializeField] private CircleCollider2D catchCollider;

    protected virtual void Awake()
    {
        Assert.IsNotNull(catchCollider);

        if (colliderList == null)
        {
            colliderList = GetComponent<ColliderList>();
            Assert.IsNotNull(colliderList);
        }

        catchCollider.radius = catchRange;
    }

    public virtual void Catch(Player player)
    {
        Assert.IsFalse(IsCaught);

        caughtBy = player;
        player.caughtObject = this;
        IsCaught = true;
    }

    public virtual void Release(Player player)
    {
        Assert.IsTrue(IsCaught);
        Assert.AreEqual(player, caughtBy);

        caughtBy = null;
        player.caughtObject = null;
        IsCaught = false;
    }

    protected virtual void Update()
    {

    }
}

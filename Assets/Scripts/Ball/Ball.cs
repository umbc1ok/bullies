using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Ball : Catchable
{
    private new Rigidbody2D rigidbody;

    private List<Player> playersToStopIgnoringCollisionsWith = new List<Player>();

    // Most of this should probably be moved to a base class

    protected override void Awake()
    {
        base.Awake();

        rigidbody = GetComponent<Rigidbody2D>();

        Assert.IsNotNull(rigidbody);
    }

    public override void Catch(Player player)
    {
        base.Catch(player);

        // Disable collision between ball and player
        foreach (var collider in colliderList.colliders)
        {
            foreach (var playerCollider in player.colliderList.colliders)
            {
                Physics2D.IgnoreCollision(collider, playerCollider, ignore: true);
            }
        }

        rigidbody.velocity = Vector2.zero;
    }

    public override void Release(Player player)
    {
        base.Release(player);

        playersToStopIgnoringCollisionsWith.Add(player);

        rigidbody.velocity = player.rigidbody.velocity;
        transform.position += new Vector3(player.rigidbody.velocity.normalized.x, player.rigidbody.velocity.normalized.y, 0.0f);
    }

    protected override void Update()
    {
        if (IsCaught)
        {
            transform.position = caughtBy.transform.position;
        }

        List<Player> playersToRemove = new List<Player>();
        foreach (var player in playersToStopIgnoringCollisionsWith)
        {
            if (player == null)
            {
                playersToRemove.Remove(player);
                continue;
            }

            // Enable collision between ball and player for the sake of checking if we stopped overlapping
            foreach (var collider in colliderList.colliders)
            {
                foreach (var playerCollider in player.colliderList.colliders)
                {
                    Physics2D.IgnoreCollision(collider, playerCollider, ignore: false);
                }
            }

            // Check if we stopped overlapping with the player
            bool isOverlapping = false;
            foreach (var collider in colliderList.colliders)
            {
                foreach (var playerCollider in player.colliderList.colliders)
                {
                    if (Physics2D.IsTouching(collider, playerCollider, contactFilter: new ContactFilter2D() { useTriggers = false }))
                    {
                        isOverlapping = true;
                        break;
                    }
                }

                if (isOverlapping)
                    break;
            }

            if (!isOverlapping)
            {
                playersToRemove.Add(player);
            }
            else
            {
                // Disable collision between ball and player
                foreach (var collider in colliderList.colliders)
                {
                    foreach (var playerCollider in player.colliderList.colliders)
                    {
                        Physics2D.IgnoreCollision(collider, playerCollider, ignore: true);
                    }
                }
            }
        }

        foreach (var player in playersToRemove)
        {
            playersToStopIgnoringCollisionsWith.Remove(player);
        }
    }
}

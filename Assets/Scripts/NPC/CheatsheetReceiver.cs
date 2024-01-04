using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatsheetReceiver : MonoBehaviour
{
    public GameplayManager gameplayManager;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D collider;


    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<CircleCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Throwable")
        {
            Debug.Log("Hit");
            gameplayManager.Score(collision.gameObject);
            SetAsDelievered();
            collision.gameObject.SetActive(false);
        }
    }

    private void SetAsDelievered()
    {
       spriteRenderer.color = Color.green;
       collider.enabled = false;
    }


}

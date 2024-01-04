using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public GameplayManager gameplayManager;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Throwable")
        {
            Debug.Log("Hit");
            gameplayManager.Lose();
        }
    }
}

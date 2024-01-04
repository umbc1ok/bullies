using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    Vector3 currentTarget;
    public Throwable ball;
    public GameObject cheatsheet;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0) && ball != null)
        {
            currentTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ball.Throw(currentTarget);
            ball = null;
        }

        if (Input.GetKeyDown(KeyCode.Space) && ball == null)
        {
            Debug.Log("Instantiating....");
            Instantiate(cheatsheet, this.transform.position, Quaternion.identity);
            ball = FindFirstObjectByType<Throwable>();
        }
    }
}

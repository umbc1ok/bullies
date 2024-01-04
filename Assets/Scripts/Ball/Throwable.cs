using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    // Start is called before the first frame update
    bool isHot = false;
    Vector3 currentTarget;
    public float speed = 10.0f;

    public void Throw(Vector3 target)
    {
        isHot = true;
        currentTarget = target;
    }


    private void FixedUpdate()
    {
        if (isHot)
        {
            this.transform.position += (currentTarget - this.transform.position).normalized * Time.deltaTime * speed;
            float distance = Vector3.Distance(currentTarget, this.transform.position);
            if(distance < 0.1f)
            {
                isHot = false;
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Pathfollower : MonoBehaviour
{
    public List<GameObject> waypoints = new List<GameObject>();
    int currentWaypointIndex = 0;
    public float speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        RotateTowardsWaypoint();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        this.transform.position += (waypoints[currentWaypointIndex].transform.position - this.transform.position).normalized * Time.deltaTime * speed;
        
        Vector3 distance = waypoints[currentWaypointIndex].transform.position - this.transform.position;
        if(distance.magnitude<0.1f)
        {
            currentWaypointIndex++;
            if(currentWaypointIndex >= waypoints.Count)
            {
                currentWaypointIndex = 0;
            }
            RotateTowardsWaypoint();

        }
    }


    void RotateTowardsWaypoint()
    {
          // find direction of next waypoint
        Vector3 direction = (waypoints[currentWaypointIndex].transform.position - this.transform.position).normalized;
        // rotate to face next waypoint
        Vector3 eulerRotation = new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        this.transform.rotation = Quaternion.Euler(eulerRotation);


    }
}

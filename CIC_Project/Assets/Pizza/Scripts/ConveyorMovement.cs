using UnityEngine;
using System.Collections.Generic;

public class ConveyorMovement : MonoBehaviour
{
    public List<Transform> waypoints = new List<Transform>();
    public float speed = 2.0f; 

    public int currentWaypoint = 0;
    public bool shouldMove = false;

    private void Start()
    {

    }

    void Update()
    {
        if (shouldMove)
        {
            MoveOnConveyor();
        }
    }

    void MoveOnConveyor()
    {
        if (waypoints.Count == 0)
            return;

        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) < 0.1f)
        {
            shouldMove = false;
            if (currentWaypoint == 2 || currentWaypoint == 4)
            {
                currentWaypoint++;
                shouldMove = true;
            }
        } 
    }

    public void StartConveyor()
    {
        shouldMove = true;
    }

    public void MoveToNextWaypoint()
    {
        currentWaypoint++;
        currentWaypoint = Mathf.Clamp(currentWaypoint, 0, 6);
        shouldMove = true;
    }


}

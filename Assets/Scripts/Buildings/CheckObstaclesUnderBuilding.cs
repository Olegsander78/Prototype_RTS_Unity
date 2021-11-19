using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckObstaclesUnderBuilding : MonoBehaviour
{
    public bool IsObstaclesUnderBuilding;

    private void OnTriggerEnter(Collider other)
    {
        string nameTagObstacles = other.tag;
        Debug.Log(nameTagObstacles);
        if (nameTagObstacles == "Obstacle" || nameTagObstacles=="EnemyUnit" || nameTagObstacles=="friendlyUnit")
        {
            IsObstaclesUnderBuilding = true;
        }
        else
        {
            IsObstaclesUnderBuilding = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        string nameTagObstacles = other.tag;
        Debug.Log(nameTagObstacles);
        if (nameTagObstacles == "Obstacle" || nameTagObstacles == "EnemyUnit" || nameTagObstacles == "friendlyUnit")
        {
            IsObstaclesUnderBuilding = false;
        }
        else
        {
            IsObstaclesUnderBuilding = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        string nameTagObstacles = other.tag;
        Debug.Log(nameTagObstacles);
        if (nameTagObstacles == "Obstacle" || nameTagObstacles == "EnemyUnit" || nameTagObstacles == "friendlyUnit")
        {
            IsObstaclesUnderBuilding = true;
        }
        else
        {
            IsObstaclesUnderBuilding = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int Xsize = 3;
    public int Zsize = 3;


    private void OnDrawGizmos()
    {
        float cellsize = FindObjectOfType<BuildingPlacer>().CellSize;

        for (int x = 0; x < Xsize; x++)
        {
            for (int z = 0; z < Zsize; z++)
            {
                Gizmos.DrawWireCube(transform.position + new Vector3(x, 0, z) * cellsize, new Vector3(1f, 0f, 1f) * cellsize);
                Gizmos.DrawRay(transform.position + new Vector3(x, 0, z), -Vector3.up);
            }
        }
    }
}

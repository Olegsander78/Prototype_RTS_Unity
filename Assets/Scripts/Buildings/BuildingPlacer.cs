using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    public float CellSize = 1f;
    public Camera RaycastCamera;
    private Plane _plane;
    public Building CurrentBuilding;

    public CheckObstaclesUnderBuilding CheckObstaclesUnderBuilding;    

    public Dictionary<Vector2Int, Building> BuildingsDictionary = new Dictionary<Vector2Int, Building>();

    void Start()
    {
        _plane = new Plane(Vector3.up, Vector3.zero);
        SetObstacles();
    }

        void SetObstacles()
        {
            Obstacle[] obstacles = FindObjectsOfType<Obstacle>();
            for (int o = 0; o < obstacles.Length; o++)
            {
                int xPosition = Mathf.RoundToInt(obstacles[o].transform.position.x);
                int zPosition = Mathf.RoundToInt(obstacles[o].transform.position.z);
                for (int x = 0; x < obstacles[o].Xsize; x++)
                {
                    for (int z = 0; z < obstacles[o].Zsize; z++)
                    {
                        Vector2Int coordinate = new Vector2Int(xPosition + x, zPosition + z);
                        BuildingsDictionary.Add(coordinate, CurrentBuilding);
                    }
                }
            }
    }

    void Update()
    {
        if (CurrentBuilding == null)
        {
            return;
        }
        Ray ray = RaycastCamera.ScreenPointToRay(Input.mousePosition);
        float distance;
        _plane.Raycast(ray, out distance);
        Vector3 point = ray.GetPoint(distance)*CellSize;

        int x = Mathf.RoundToInt(point.x);
        int z = Mathf.RoundToInt(point.z);

        CurrentBuilding.transform.position = new Vector3(x, 0f, z) * CellSize;
        //Проверка наличия казармы и объекта ландшафта
        CheckObstaclesUnderBuilding = FindObjectOfType<CheckObstaclesUnderBuilding>();
        if (CheckAllow(x, z, CurrentBuilding) &&  !CheckObstaclesUnderBuilding.IsObstaclesUnderBuilding)
        {
            CurrentBuilding.DisplayAcceptableposition();
            if (Input.GetMouseButtonDown(0))
            {
                InstallBuilding(x, z, CurrentBuilding);
                CurrentBuilding = null;
            }
        }
        else
        {
            CurrentBuilding.DisplayUnacceptableposition();
        }
        
    }

    //Проверка свободного места под здание
    bool CheckAllow(int xPosition, int zPosition, Building building)
    {
        for (int x = 0; x < building.Xsize; x++)
        {
            for (int z = 0; z < building.Zsize; z++)
            {
                Vector2Int coordinate = new Vector2Int(xPosition + x, zPosition + z);

                if (BuildingsDictionary.ContainsKey(coordinate))
                {
                    return false;
                }
            }
        }
        return true;
    }
    
    //Установка здания
    void InstallBuilding(int xPosition, int zPosition, Building building)
    {
        for (int x = 0; x < building.Xsize; x++)
        {
            for (int z = 0; z < building.Zsize; z++)
            {
                Vector2Int coordinate = new Vector2Int(xPosition + x, zPosition + z);
                BuildingsDictionary.Add(coordinate, CurrentBuilding);
                building.PlaceBuilding();
            }
        }

        foreach (var item in BuildingsDictionary)
        {
            Debug.Log(item);
        }
    }

    //Создание здания
    public void CreateBuilding(GameObject buildingPrefab)
    {
        GameObject newBuilding = Instantiate(buildingPrefab);
        CurrentBuilding = newBuilding.GetComponent<Building>();
    }
}

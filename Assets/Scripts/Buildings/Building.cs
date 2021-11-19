using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : SelectableObject
{
    public int Price;
    public int BuildingHitPoint;
    private int _maxBuildingHitPoint;
    public GameObject HealthBarPrefab;
    private HealthBar _healthBar;
    public float PositionHealthBar;

    public int Xsize = 3;
    public int Zsize = 3;

    private Color _startColor;
    public Renderer Renderer;

    public Canvas MenuBuildingCanvas;

    public bool IsPlaced;
    [SerializeField] private Collider _collider;

    public override void Start()
    {
        base.Start();
        MenuBuildingCanvas.enabled = false;
        if (_collider) { _collider.enabled = false; }
        _maxBuildingHitPoint = BuildingHitPoint;
        GameObject healthBar = Instantiate(HealthBarPrefab);
        _healthBar = healthBar.GetComponent<HealthBar>();
        _healthBar.SetHealthBarHeightAtGround(PositionHealthBar);
        _healthBar.Setup(transform);
        SetHealth(BuildingHitPoint, _maxBuildingHitPoint);
    }

    private void Awake()
    {
        _startColor = Renderer.material.color;
    }

    private void OnDrawGizmos()
    {
        float cellsize = FindObjectOfType<BuildingPlacer>().CellSize;
        
        for (int x = 0; x < Xsize; x++)
        {
            for (int z = 0; z < Zsize; z++)
            {
                Gizmos.DrawWireCube(transform.position+ new Vector3(x,0,z)*cellsize, new Vector3(1f, 0f, 1f)*cellsize);
                Gizmos.DrawRay(transform.position + new Vector3(x, 0, z), -Vector3.up);
            }
        }
    }

    public virtual void DisplayUnacceptableposition()
    {
        Renderer.material.color = Color.red;
    }
    public virtual void DisplayAcceptableposition()
    {
        Renderer.material.color = _startColor;
    }

    public override void SelectView()
    {
        base.SelectView();
        MenuBuildingCanvas.enabled = true;
    }

    public override void UnselectView()
    {
        base.UnselectView();
        MenuBuildingCanvas.enabled = false;
    }

    public void PlaceBuilding()
    {
        IsPlaced = true;
        _collider.enabled = true;
    }

    public void GetDamage(int damageValue)
    {
        BuildingHitPoint -= damageValue;
        SetHealth(BuildingHitPoint, _maxBuildingHitPoint);
        if (BuildingHitPoint <= 0)
        {
            Destruct();
        }
    }

    private void Destruct()
    {
        Destroy(gameObject, 1f);
        FindObjectOfType<Management>().Unselect(this);
        if (_healthBar)
        {
            _healthBar.DelHealthBar();
        }
    }

    public void SetHealth(int health, int maxHealth)
    {
        float xScale = (float)health / maxHealth;
        xScale = Mathf.Clamp01(xScale) * Xsize;
        _healthBar.SetHealthBar(xScale);
    }
}



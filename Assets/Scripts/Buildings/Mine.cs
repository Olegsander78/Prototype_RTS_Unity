using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : Building
{
    public GameObject GoldMineBarPrefab;
    private ProgressBarForBuilding _progressBarForBuilding;

    public float CreationGoldPeriod = 3f;
    public int CreationGoldValue = 1;

    private float _timer;
    private Resources _resources;

    public override void Start()
    {
        GameObject newGoldMineBarPrefab = Instantiate(GoldMineBarPrefab);
        _progressBarForBuilding = newGoldMineBarPrefab.GetComponent<ProgressBarForBuilding>();
        _progressBarForBuilding.Setup(transform);

        _resources = FindObjectOfType<Resources>();
    }

    void Update()
    {
        CreationGold();
    }

    void CreationGold()
    {
        _timer += Time.deltaTime;
        float xScale = _timer / CreationGoldPeriod;
        xScale = Mathf.Clamp01(xScale);
        _progressBarForBuilding.SetValue(xScale);
        if (_timer > CreationGoldPeriod)
        {
            _timer = 0;
            _resources.AddMoney(CreationGoldValue);
        }
    }
}

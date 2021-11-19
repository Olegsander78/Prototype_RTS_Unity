using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blacksmith : Building
{
 [Header("Blacksmith")]
    public GameObject BlacksmithBarPrefab;
    private ProgressBarForBuilding _progressBarForBuilding;

    private float _timer;
    private Resources _resources;

    public float CreationRecruitPeriod = 3f;
    public int CreationRecruitValue = 1;

    public override void Start()
    {
        GameObject newBlacksmithPrefab = Instantiate(BlacksmithBarPrefab);
        _progressBarForBuilding = newBlacksmithPrefab.GetComponent<ProgressBarForBuilding>();
        _progressBarForBuilding.Setup(transform);

        _resources = FindObjectOfType<Resources>();
    }

    void Update()
    {
        CreationRecruit();
    }

    void CreationRecruit()
    {
        _timer += Time.deltaTime;
        float xScale = _timer / CreationRecruitPeriod;
        xScale = Mathf.Clamp01(xScale);
        _progressBarForBuilding.SetValue(xScale);
        if (_timer > CreationRecruitPeriod)
        {
            _timer = 0;
            _resources.AddRecruits(CreationRecruitValue);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBarForBuilding : MonoBehaviour
{
    public Transform ScaleTransform;
    public Transform Target;

    private Transform _cameraTransform;

    private void Start()
    {
        _cameraTransform = Camera.main.transform;
    }

    private void LateUpdate()
    {
        transform.position = Target.position + (Vector3.up * 2f);
        transform.rotation = _cameraTransform.rotation;
    }

    public void Setup(Transform target)
    {
        Target = target;
    }

    public void SetValue(float value)
    {
        ScaleTransform.localScale = new Vector3(value, 1f, 1f);
    }
}

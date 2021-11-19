using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldMineCreator : MonoBehaviour
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
}
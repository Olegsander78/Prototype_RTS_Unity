using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Transform ScaleTransform;
    public Transform Target;
    public float HeghtHealthBarAtGround = 2f;

    private Transform _cameraTransform;

    private void Start()
    {
        _cameraTransform = Camera.main.transform;
    }

    private void LateUpdate()
    {
        if (Target != null)
        {
            transform.position = Target.position + (Vector3.up * HeghtHealthBarAtGround);
            transform.rotation = _cameraTransform.rotation;
        }
        else
        {
            DelHealthBar();
        }
    }

    public void Setup(Transform target)
    {
        Target = target;
    }

    public void SetHealthBar(float xScale)
    {
        ScaleTransform.localScale = new Vector3(xScale, 1f, 1f);
    }

    public void DelHealthBar()
    {
        Destroy(gameObject);
    }

    public void SetHealthBarHeightAtGround(float height)
    {
        HeghtHealthBarAtGround = height;
    }
}

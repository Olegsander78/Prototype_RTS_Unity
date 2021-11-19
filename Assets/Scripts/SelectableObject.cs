using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObject : MonoBehaviour
{
    public GameObject SelectionIndicator;

    public virtual void Start()
    {
        SelectionIndicator.SetActive(false);
    }
    public virtual void OnHover()
    {
        transform.localScale = transform.localScale * 1.1f;
    }

    public virtual void OnUnhover()
    {
        transform.localScale = transform.localScale / 1.1f;
    }

    public virtual void SelectView()
    {
        SelectionIndicator.SetActive(true);
    }

    public virtual void UnselectView()
    {
        SelectionIndicator.SetActive(false);
    }

    public virtual void WhenClickOnGround(Vector3 point)
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SelectionState
{
    UnitsSelected,
    Frame,
    Other
}

public class Management : MonoBehaviour
{
    public Camera Camera;
    public SelectableObject Hovered;
    public List<SelectableObject> ListOfSelected = new List<SelectableObject>();

    public Image FrameImage;
    private Vector2 _frameStart;
    private Vector2 _frameEnd;

    public SelectionState CurrentSelectionState;

    public LayerMask LayerMask;

    void Update()
    {
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction*10f, Color.green);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f, LayerMask,QueryTriggerInteraction.Ignore))
        {
            if (hit.collider.GetComponent<SelectabaleCollider>())
            {
                SelectableObject hitSelectable = hit.collider.GetComponent<SelectabaleCollider>().SelectableObject;
                if (Hovered)
                {
                    if (Hovered != hitSelectable)
                    {
                        Hovered.OnUnhover();
                        Hovered = hitSelectable;
                        Hovered.OnHover();
                    }
                }
                else
                {
                    Hovered = hitSelectable;
                    Hovered.OnHover();
                }
            }
            else
            {
                UnhoverCurrent();
            }
        }
        else
        {
            UnhoverCurrent();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (Hovered)
            {
                if (Input.GetKey(KeyCode.LeftControl) == false)
                {
                    UnselectAll();
                }
                CurrentSelectionState = SelectionState.UnitsSelected;
                Select(Hovered);
            } 
        }
        if (CurrentSelectionState == SelectionState.UnitsSelected)
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (hit.collider.tag == "Ground")
                {
                    int rowNumber = Mathf.CeilToInt(Mathf.Sqrt(ListOfSelected.Count));
                    
                    for (int i = 0; i < ListOfSelected.Count; i++)
                    {                        
                        int row = (i+1) / rowNumber;
                        int column = i % rowNumber;
                        Vector3 point = hit.point + new Vector3(row, 0f, column);

                        ListOfSelected[i].WhenClickOnGround(point);
                    }
                }
            }
        }
        //Сброс выделенного
        if (Input.GetMouseButtonDown(1))
        {
            UnselectAll();
        }

        //Выделение рамкой
        if (Input.GetMouseButtonDown(0))
        {
            _frameStart = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            
            _frameEnd = Input.mousePosition;
            Vector2 min = Vector2.Min(_frameStart, _frameEnd);
            Vector2 max = Vector2.Max(_frameStart, _frameEnd);
            Vector2 size = max - min;
            if (size.magnitude > 10)
            {                
                FrameImage.enabled = true;
                FrameImage.rectTransform.anchoredPosition = min;
                FrameImage.rectTransform.sizeDelta = size;
                Rect rect = new Rect(min, size);

                UnselectAll();
                Unit[] allUnits = FindObjectsOfType<Unit>();
                for (int i = 0; i < allUnits.Length; i++)
                {
                    Vector2 screenPosition = Camera.WorldToScreenPoint(allUnits[i].transform.position);
                    if (rect.Contains(screenPosition))
                    {
                        Select(allUnits[i]);
                    }
                }
                CurrentSelectionState = SelectionState.Frame;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            FrameImage.enabled = false;
            if (ListOfSelected.Count > 0)
            {
                CurrentSelectionState = SelectionState.UnitsSelected;
            }
            else
            {
                CurrentSelectionState = SelectionState.Other;
            }
        }
    }

    //Выбор одного объекта
    void Select(SelectableObject selectableObject)
    {
        if (ListOfSelected.Contains(selectableObject) == false)
        {
            ListOfSelected.Add(selectableObject);
            selectableObject.SelectView();
        }
    }

    public void Unselect(SelectableObject selectableObject)
    {
        if (ListOfSelected.Contains(selectableObject))
        {
            ListOfSelected.Remove(selectableObject);
        }
    }
    void UnselectAll()
    {
        for (int i = 0; i < ListOfSelected.Count; i++)
        {
            ListOfSelected[i].UnselectView();
        }
        ListOfSelected.Clear();
        CurrentSelectionState = SelectionState.Other;
    }

    void UnhoverCurrent()
        {
            if (Hovered)
            {
                Hovered.OnUnhover();
                Hovered = null;
            }
        }
}

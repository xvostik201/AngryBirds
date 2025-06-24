using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private bool _isDragging;

    void Start()
    {
        
    }

    void Update()
    {
        if (_isDragging)
        {
            transform.position = GetMousePosition();
        }
    }

    private Vector3 GetMousePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 camMousePosition = Camera.main.ScreenToWorldPoint(mousePos);
        camMousePosition.z = 0;
        return camMousePosition;
    }

    private void OnMouseDown()
    {
        _isDragging = true;
    }

    private void OnMouseUp()
    {
        _isDragging = false;
    }

}

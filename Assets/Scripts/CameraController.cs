using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 _mouseOriginPoint;
    private Vector3 _mouseOffset;
    private bool _dragging;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * (Camera.main.orthographicSize), 2.5f, 50f);
        
        if(Input.GetMouseButton(2))
        {
            _mouseOffset = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position); //calculate the offset from mouse click to center of camera position
            if(!_dragging)
            {
                _dragging = true;
                _mouseOriginPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition); //get the current mouse click position
            }
        }
        else
        {
            _dragging = false;
        }

        if(_dragging)
        {
            transform.position = _mouseOriginPoint - _mouseOffset; //make the camera movement based on mouse click position minus offset.
        }
    }
}

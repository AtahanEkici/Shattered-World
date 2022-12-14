﻿using UnityEngine;
public class Camera_Controller : MonoBehaviour
{
    [Header("Mouse Inputs")]
    [SerializeField]private float Magnify_speed = 100f;
    [SerializeField]private float max_magnify = 0.2f;
    [SerializeField]private float min_magnify = -10f;
    //[SerializeField] private bool InvertControlls = false;
    [SerializeField]private float MouseMoveSpeed = 100f;
    [SerializeField]private float MouseInputTemp = 0f;
    

    [Header("Keyboard  Inputs")]
    [SerializeField] private float KeyboardMoveSpeed = 100f;

    private void LateUpdate()
    {
       MoveWithKeyboard();
       MoveWithMouse();
       Magnify();
    }

    private void MoveWithMouse()
    {
        Vector3 mousepos = Input.mousePosition;

        float mouse_x = mousepos.x;
        float mouse_y = mousepos.y;

        float width = Screen.width;
        float height = Screen.height;

        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Width: "+width+" - Mouse_x: "+mouse_x+"");
        }

        if(mouse_x <= (0 + MouseInputTemp))
        {
            Debug.Log("Left");
            transform.Translate(-1f * MouseMoveSpeed * Time.unscaledDeltaTime * Vector3.right);
        }
        else if(mouse_x >= (width - MouseInputTemp))
        {
            Debug.Log("Right");
            transform.Translate(MouseMoveSpeed * Time.unscaledDeltaTime * Vector3.right); 
        }

        if (mouse_y >= (height - MouseInputTemp))
        {
            Debug.Log("Up");
            transform.Translate(MouseMoveSpeed * Time.unscaledDeltaTime * (0.5f * (Vector3.up + Vector3.forward)));
        }
        else if (mouse_y <= (0 + MouseInputTemp))
        {
            Debug.Log("Down");
            transform.Translate(-1f * MouseMoveSpeed * Time.unscaledDeltaTime * (0.5f * (Vector3.up + Vector3.forward)));
        }

        /*
        if(mouse_x >= (width - MouseInputTemp) || mouse_y >= (height - MouseInputTemp) || mouse_x  <= (0 + MouseInputTemp) || mouse_y <= (0 + MouseInputTemp))
        {
            Debug.Log("X:"+x+", Y: "+y+"");
            Vector3 moveBy = (Vector3.right * x) + (0.5f * y * (Vector3.up + Vector3.forward));
            transform.Translate(MouseMoveSpeed * Time.deltaTime * moveBy.normalized);
        }
        */
;    }
    /*
    private void MoveWithMouse()
    {
        float x = Input.GetAxisRaw("Mouse X");
        float y = Input.GetAxisRaw("Mouse Y");

        if(Input.GetMouseButton(0)) // traverse the plane //
        {
            if (x == 0) { return; }

            if(InvertControlls)
            {
                transform.Translate(x * MouseMoveSpeed * Time.deltaTime * Vector3.right);
            }
            else
            {
                transform.Translate(-x * MouseMoveSpeed * Time.deltaTime * Vector3.right);
            } 
        }
        else if (Input.GetMouseButton(1)) // rotate camera with left click //
        {
            Vector3 rotation = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler((y+rotation.x), (x+rotation.y), 0); 
        }
    }
    */
    private void MoveWithKeyboard() // the script that moves the camera through keyboard input //
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        if (x == 0 && y == 0) { return; }
        Vector3 moveBy = (Vector3.right * x) + ((Vector3.up+Vector3.forward) * 0.5f * y);
        Debug.DrawRay(Camera.main.transform.position, Vector3.forward * 100f,Color.black);
        transform.Translate(KeyboardMoveSpeed * Time.deltaTime * moveBy.normalized);
    }

    private void Magnify()
    {
        float MouseWheelInput = Input.GetAxisRaw("Mouse ScrollWheel");

        // Constraints - START// 
        if (MouseWheelInput == 0f) { return; } // if mouse 3 not engaged  do not execute//
        float z = transform.position.z;
        if (z < min_magnify && MouseWheelInput < 0f) { z = min_magnify; Debug.Log("minimum magnification reached"); return; }
        if (z > max_magnify && MouseWheelInput > 0f) { z = max_magnify; Debug.Log("maximum magnification reached"); return; }
        // Constraints - END // 

        transform.Translate(Magnify_speed * MouseWheelInput * Time.unscaledDeltaTime * Vector3.forward);
    }
}
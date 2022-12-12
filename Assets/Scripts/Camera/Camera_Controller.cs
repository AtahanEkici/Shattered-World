
using UnityEngine;
public class Camera_Controller : MonoBehaviour
{
    private float camera_x_rotation;
    private float camera_y_rotation;
    private float camera_z_rotation;

    [Header("Mouse Middle Button Inputs")]
    [SerializeField] private float Magnify_speed = 100f;
    [SerializeField] private float max_magnify = 0.2f;
    [SerializeField] private float min_magnify = -10f;

    [Header("Mouse Left Click Inputs")]
    [SerializeField] private bool InvertControlls = false;
    [SerializeField] private float MouseMoveSpeed = 10f;

    [Header("Keyboard  Inputs")]
    [SerializeField] private float KeyboardMoveSpeed = 100f;
    /*
    [SerializeField] private float max_vertical = 100f;
    [SerializeField] private float mim_vertical = 100f;

    [SerializeField] private float max_horizontal = 100f;
    [SerializeField] private float min_horizontal = 100f;
    */
    private void Awake()
    {
        camera_x_rotation = transform.localEulerAngles.x;
        camera_y_rotation = transform.localEulerAngles.y;
        camera_z_rotation = transform.localEulerAngles.z;
    }

    private void LateUpdate()
    {
        MoveWithKeyboard();
        MoveWithMouse();
        Magnify();
    }
    private void MoveWithMouse()
    {
        float x = Input.GetAxisRaw("Mouse X");
        float y = Input.GetAxisRaw("Mouse Y");

        if(Input.GetMouseButton(0)) // traverse the plane //
        {
            /*
            if (x == 0) { return; }

            if(InvertControlls)
            {
                transform.Translate(x * MouseMoveSpeed * Time.deltaTime * Vector3.right);
            }
            else
            {
                transform.Translate(-x * MouseMoveSpeed * Time.deltaTime * Vector3.right);
            }
            */
        }
        else if (Input.GetMouseButton(1)) // rotate camera with left click //
        {
            /*
            Vector3 rotation = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler((y+rotation.x), (x+rotation.y), 0);
            */
        }
    }
    private void MoveWithKeyboard() // the script that moves the camera through keyboard input //
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        //if (x == 0 && y == 0) { return; }
        Vector3 moveBy = (Vector3.right * x) + ((Vector3.up+Vector3.forward) * 0.5f * y);
        //Vector3 bisaction = (moveBy.normalized / 2.0f);
        //bisaction = (Quaternion.Euler(camera_x_rotation, camera_y_rotation, camera_z_rotation) * bisaction).normalized;
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
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
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
    private void LateUpdate()
    {
        MoveWithKeyboard();
        MoveWithMouse();
        Magnify();
    }
    private void MoveWithMouse()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        if(Input.GetMouseButton(0)) // traverse the plane //
        {
            if (x == 0) { return; }

            if(InvertControlls)
            {
                transform.Translate(x * MouseMoveSpeed * Time.unscaledDeltaTime * Vector3.right);
            }
            else
            {
                transform.Translate(-x * MouseMoveSpeed * Time.unscaledDeltaTime * Vector3.right);
            }
        }
        else if (Input.GetMouseButton(1)) // rotate camera with left click //
        {
            Vector3 rotation = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler((y+rotation.x), (x+rotation.y), 0);
        }
    }
    private void MoveWithKeyboard() // the script that moves the camera through keyboard input //
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 moveBy = transform.right * x + transform.forward * y;
        transform.Translate(KeyboardMoveSpeed * Time.unscaledDeltaTime * moveBy.normalized);
    }

    private void Magnify()
    {
        float MouseWheelInput = Input.GetAxis("Mouse ScrollWheel");

        // Constraints - START// 
        if (MouseWheelInput == 0f) { return; } // if mouse 3 not engaged  do not execute//
        float z = transform.position.z;
        if (z < min_magnify && MouseWheelInput < 0f) { Debug.Log("minimum magnification reached"); return; }
        if (z > max_magnify && MouseWheelInput > 0f) { Debug.Log("maximum magnification reached"); return; }
        // Constraints - END // 

        transform.Translate(Magnify_speed * MouseWheelInput * Time.unscaledDeltaTime * Vector3.forward);
    }
}
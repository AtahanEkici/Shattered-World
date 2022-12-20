using UnityEngine;
public class Camera_Controller : MonoBehaviour
{
    [Header("Mouse Magnify Inputs")]
    [SerializeField]private float Magnify_speed = 100f;
    [SerializeField]private float max_magnify = 0.2f;
    [SerializeField]private float min_magnify = -10f;

    [Header("Mouse Movement Inputs")]
    [SerializeField]private float MouseMoveSpeed = 100f;
    [SerializeField]private float MouseInputTemp = 0f;

    [Header("Mouse Boundary Restrictions")]
    [SerializeField]private float up_boundary = 50f;
    [SerializeField]private float down_boundary = -50f;
    [SerializeField] private float left_boundary = -50f;
    [SerializeField] private float right_boundary = 50f;

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

        /*
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Width: "+width+" - Mouse_x: "+mouse_x+"");
        }
        */

        Vector3 pos = this.transform.position;
        float pos_x = pos.x;
        float pos_z = pos.z;

        if(mouse_x <= (0 + MouseInputTemp) && pos_x >= left_boundary) // Move left //
        {
            //Debug.Log("Left");
            transform.Translate(-1f * MouseMoveSpeed * Time.unscaledDeltaTime * Vector3.right);
        }
        else if(mouse_x >= (width - MouseInputTemp) && pos_x <= right_boundary) // Move Right //
        {
            //Debug.Log("Right");
                transform.Translate(MouseMoveSpeed * Time.unscaledDeltaTime * Vector3.right); 
        }

        if (mouse_y >= (height - MouseInputTemp) && pos_z <= up_boundary) // Move up //
        {
            //Debug.Log("Up "+ pos_z + " :  "+up_boundary+"");
                transform.Translate(MouseMoveSpeed * Time.unscaledDeltaTime * (0.5f * (Vector3.up + Vector3.forward))); // 
        }
        else if (mouse_y <= (0 + MouseInputTemp) && pos_z >= down_boundary) // Move Down //
        {
            //Debug.Log("Down");
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
        //Debug.DrawRay(Camera.main.transform.position, Vector3.forward * 100f,Color.black);
        transform.Translate(KeyboardMoveSpeed * Time.unscaledDeltaTime * moveBy.normalized);
    }

    private void Magnify()
    {
        float MouseWheelInput = Input.GetAxisRaw("Mouse ScrollWheel");

        // Constraints - START// 
        if (MouseWheelInput == 0f) { return; } // if mouse 3 not engaged  do not execute//
        float x_pos = transform.position.x;
        float y_pos = transform.position.y;
        float z_pos = transform.position.z;
        if (z_pos < min_magnify && MouseWheelInput < 0f) { transform.Translate(x_pos, y_pos, min_magnify); Debug.Log("minimum magnification reached"); return; }
        if (z_pos > max_magnify && MouseWheelInput > 0f) { transform.Translate(x_pos, y_pos, max_magnify); Debug.Log("maximum magnification reached"); return; }
        // Constraints - END // 

        transform.Translate(Magnify_speed * MouseWheelInput * Time.unscaledDeltaTime * Vector3.forward);
    }
}
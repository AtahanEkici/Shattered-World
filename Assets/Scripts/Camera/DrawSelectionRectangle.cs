using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class DrawSelectionRectangle : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Vector3 Start_pos;
    [SerializeField] private Vector3 End_pos;
    [SerializeField] private GameObject InventoryCanvas;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        InventoryCanvas = cam.GetComponent<Select>().Inventory_Canvas;
        Start_pos = Vector3.zero;
        End_pos = Vector3.zero;
    }

    private void DrawQuad(Rect position, Color color)
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, color);
        texture.Apply();
        GUI.skin.box.normal.background = texture;
        GUI.Box(position, GUIContent.none);
    }

    private void OnGUI()
    {
        DrawQuad(new Rect(Start_pos, End_pos), Color.blue);
    }

    private void DrawRectangle()
    {
        Vector3 mousepos = Input.mousePosition;
        Vector3 mouse_point = Vector3.zero;

        float screen_height = Screen.height;
        float screen_width = Screen.width;

        if (Input.GetMouseButtonDown(0)) // Get the initial coordinates of the mouse 
        {
            //Debug.Log("Click");
            Start_pos = cam.ScreenToWorldPoint(new Vector3(mousepos.x, mousepos.y, cam.nearClipPlane));
            Debug.Log("Start Position: "+Start_pos);
        }

        else if(Input.GetMouseButton(0)) // if the mouse held down draw a simple rectangle from starting position to end position
        {
            //Debug.Log("Hold");
        }

        else if(Input.GetMouseButtonUp(0)) // if the user releases the mouse button //
        {
            //Debug.Log("Unclick");
            End_pos = cam.ScreenToWorldPoint(new Vector3(mousepos.x, mousepos.y, cam.nearClipPlane));
            Debug.Log("End Position: " +End_pos);
        }

        else
        {
            return;
        }
    }

    private void Update()
    {
        //DrawRectangle();
        Vector3 mousepos = Input.mousePosition;
        //DrawQuad(new Rect(Start_pos, End_pos), Color.blue);
    }
}

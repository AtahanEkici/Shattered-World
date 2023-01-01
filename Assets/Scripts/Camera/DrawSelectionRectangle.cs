using System.Drawing;
using UnityEditor;
using UnityEngine;

public class DrawSelectionRectangle : MonoBehaviour
{
    [SerializeField] private Vector2 _box_end_pos;
    [SerializeField] private Vector2 _box_start_pos;
    [SerializeField] private Texture2D texture;
    [SerializeField] private UnityEngine.Color color;

    private void Awake()
    {
        if(texture == null)
        {
            texture = Assigntexture();
        }
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _box_start_pos = Input.mousePosition;
            }
                
            else
            {
                _box_end_pos = Input.mousePosition;
            }      
        }
        else
        {
            if (_box_end_pos != Vector2.zero && _box_start_pos != Vector2.zero)
            {
                //HandleUnitSelection();
            }
            _box_end_pos = _box_start_pos = Vector2.zero;
        }
    }
    private Texture2D Assigntexture()
    {
        Texture2D tex = new Texture2D(1, 1);
        tex.SetPixel(0, 0, color);
        return tex;
    }
    private void OnGUI()
    {
        if (_box_start_pos != Vector2.zero && _box_end_pos != Vector2.zero)
        {
            var rect = new Rect(_box_start_pos.x, Screen.height - _box_start_pos.y,_box_end_pos.x - _box_start_pos.x, -1 * (_box_end_pos.y - _box_start_pos.y));
            GUI.DrawTexture(rect, texture);
        }
    }
}

using UnityEngine;
using UnityEngine.AI;

public class Select : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject CurrentSelectedEntity = null;
    [SerializeField] private LayerMask layer_mask;
    [SerializeField] private string tag_of_object;
    [SerializeField] public GameObject Inventory_Canvas;

    private void Awake()
    {
        cam = this.GetComponent<Camera>();
        Inventory_Canvas = GameObject.Find("Inventory_Canvas");
        Inventory_Canvas.SetActive(false);
    }

    [System.Obsolete]
    private void Update()
    {
        if(Input.GetMouseButtonDown(0)) // left click selects the target //
        {
            Select_Object();
        }
        else if(Input.GetMouseButtonDown(1)) // right click moves the target //
        {
            Debug.Log("Geçti");
            if (CurrentSelectedEntity == null) { Debug.Log("Nothing to move"); return; }
            if (!CurrentSelectedEntity.CompareTag("Unit")) { return; }
            if (!CurrentSelectedEntity.TryGetComponent<NavMeshAgent>(out var navmesh_temp)) { return; }

            Unit_Controller unit_temp = navmesh_temp.GetComponent<Unit_Controller>();

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                unit_temp.MoveTo(hit.transform);
            }      
        }
    }

    [System.Obsolete] // because of GetAllChildren() //
    private void Select_Object() // Select a playable object by raycasting the target should be on the corresponding layer mask //
    {
        //Debug.Log("It works");
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layer_mask))
        {
            GameObject temp = hit.transform.gameObject;
            CurrentSelectedEntity = temp;
            Debug.Log("Hit: " + temp.name);
            if (temp == null) { return; } // if nothing hit the raycast return //
            if (!hit.transform.CompareTag(tag_of_object)) { return; } // if the object is not on a desirable tag end execution of this function //
            if (temp.GetComponent<Inventory_Manager>() == null) { return; } // if the object does not have an inventory script exit execution //

            
            Inventory_Canvas.SetActive(true); // Activate Inventory Canvas for display //
            //Debug.Log("Display ınventory");
            temp.GetComponent<Inventory_Manager>().DisplayInventory();
        }
        else
        {
            Inventory_Canvas.SetActive(false); // If nothing is selected don't dislpay the Inventory Canvas //
            if (CurrentSelectedEntity == null) { Debug.Log("Nothing preferable is selected"); }
            else
            {
                CurrentSelectedEntity = null; // if the raycast did not hit a playable object drop the current selected object //
                Canvas_Controller.DeleteAllChildren();
                Debug.Log("Selected object dropped");
            }
        }
    }
}
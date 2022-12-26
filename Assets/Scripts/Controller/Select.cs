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
        cam = GetComponent<Camera>();
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
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (CurrentSelectedEntity == null) { Debug.Log("Nothing to move"); return; }
            if (!CurrentSelectedEntity.CompareTag("Unit")) { return; }
            if (!CurrentSelectedEntity.TryGetComponent<NavMeshAgent>(out var navmesh_temp)) { return; }

            Unit_Controller unit_temp = navmesh_temp.GetComponent<Unit_Controller>();

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                unit_temp.ChangeDirection(hit.point);
                Debug.Log(hit.transform.position);
            }
            else
            {
                Debug.Log("Could not move");
            }
        }
    }

    [System.Obsolete] // because of GetAllChildren() this function is obsolete //
    private void Select_Object() // Select a playable object by raycasting the target should be on the corresponding layer mask //
    {
        //Debug.Log("It works");
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layer_mask))
        {
            CurrentSelectedEntity = hit.transform.gameObject;
            Debug.Log("Hit: " + CurrentSelectedEntity.name);
            if (CurrentSelectedEntity == null) { return; } // if nothing hit the raycast return //
            if (!CurrentSelectedEntity.CompareTag(tag_of_object)) { return; } // if the object is not on a desirable tag end execution of this function //
            if (CurrentSelectedEntity.GetComponent<Inventory_Manager>() == null) { return; } // if the object does not have an inventory script exit execution //

            
            Inventory_Canvas.SetActive(true); // Activate Inventory Canvas for display //
            //Debug.Log("Display ınventory");
            CurrentSelectedEntity.GetComponent<Inventory_Manager>().DisplayInventory();
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
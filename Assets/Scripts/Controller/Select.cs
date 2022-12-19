using UnityEngine;
public class Select : MonoBehaviour
{
    [SerializeField] private GameObject CurrentSelectedEntity = null;
    [SerializeField] private LayerMask layer_mask;
    [SerializeField] private string tag_of_object;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Select_Object();
        }
    }
    private void Select_Object()
    {
        //Debug.Log("It works");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layer_mask))
        {
            GameObject temp = hit.transform.gameObject;

            Debug.Log("Hit: " + temp.name);

            if (hit.transform.CompareTag(tag_of_object))
            {
                CurrentSelectedEntity = temp;
                Debug.Log("Display ınventory");
                // Display Inventory //
            }
        }
    }
}

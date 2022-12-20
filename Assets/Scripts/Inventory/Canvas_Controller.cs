using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Controller : MonoBehaviour
{
    [SerializeField]private static GameObject Inventory_Panel;
    [SerializeField]private static Transform this_transform;

    private void Awake()
    {
        Inventory_Panel = transform.GetChild(0).gameObject;
        this_transform = this.transform;
    }
    public static void AddImages(List<Item> list)
    {
        for (int i=0;i<list.Count;i++)
        {
            GameObject imgObject = new(list[i].name);
            /*
            RectTransform trans = imgObject.AddComponent<RectTransform>();
            trans.anchoredPosition = new Vector2(0.5f, 0.5f);
            trans.localPosition = Vector3.zero;
            trans.position = Vector3.zero;
            */

            Image image = imgObject.AddComponent<Image>();
            image.sprite = list[i].icon; 
            imgObject.transform.SetParent(Inventory_Panel.transform);
        }
    }
    [System.Obsolete]
    public static void DeleteAllChildren()
    {
        for(int i=0;i<this_transform.GetChildCount(); i++)
        {
            Destroy(this_transform.GetChild(i).gameObject);
        }
    }
}

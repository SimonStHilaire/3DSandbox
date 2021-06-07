using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiItemsController : MonoBehaviour
{

    public GameObject itemRef;

    // Start is called before the first frame update
    void Start()
    {
        clearList();
    }

    void displayItems(List<UiItem> items)
    {
        foreach (UiItem item in items)
        {
            GameObject i = Instantiate(itemRef);
            i.GetComponentInChildren<Text>().text = item.displayName;
        }

    }

    void clearList()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}

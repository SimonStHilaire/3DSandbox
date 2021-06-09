using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiItemsController : MonoBehaviour
{

    public GameObject itemRef;

    public void displayItems(List<UiItemDTO> items)
    {
        foreach (UiItemDTO item in items)
        {
            clearList();
            GameObject i = Instantiate(itemRef);
            i.GetComponent<UiItem>().setDisplayText(item.displayName);
            i.transform.SetParent(transform, false);
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

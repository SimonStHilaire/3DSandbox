using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiItemsController : MonoBehaviour
{

    public GameObject itemRef;

    public void displayItems(List<UiItemDTO> items)
    {
        clearList();
        foreach (UiItemDTO item in items)
        {
            GameObject i = Instantiate(itemRef);
            UiItem iUI = i.GetComponent<UiItem>();
            iUI.setDisplayText(item.displayName);
            iUI.id = item.id;
            if (item.thumbnail != null)
            {
                iUI.setThumbnail(item.thumbnail);
            }
            i.transform.SetParent(transform, false);
        }
        float spacing = GetComponent<HorizontalLayoutGroup>().spacing;

        float finalSize = (items.Count * (itemRef.GetComponent<RectTransform>().sizeDelta.x + spacing)) + spacing;

        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, finalSize);

    }

    void clearList()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

}

using System;
using UnityEngine;
using UnityEngine.UI;

public class UiItem : MonoBehaviour
{

    public Guid id { get; set; }

    private Text text;

    private Button button;


    public void setDisplayText(string text)
    {
        GetComponentInChildren<Text>().text = text;
    }

    public void setThumbnail(Sprite sprite)
    {
        GetComponent<Image>().color = Color.white;
        GetComponent<Image>().sprite = sprite;
    }

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => click(this.id));
    }

    void click(Guid id)
    {
        custoCarManager.Instance.btnClick(id);
    }
}
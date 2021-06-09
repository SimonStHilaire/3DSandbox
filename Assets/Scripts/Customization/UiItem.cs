using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UiItem : MonoBehaviour
{

    private int id { get; set; }

    private Text text;

    private Button button;


    public void setDisplayText(string text)
    {
        GetComponentInChildren<Text>().text = text;
    }

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => click(this.id));
    }

    void click(int id)
    {
        custoCarManager.Instance.btnClick(id);
    }
}
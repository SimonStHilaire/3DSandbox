using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
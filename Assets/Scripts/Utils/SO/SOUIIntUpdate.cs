using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SOUIIntUpdate : MonoBehaviour
{
    public SOInt soInt;
    public TextMeshProUGUI uiTextValue;

    public void Start()
    {
        uiTextValue.text = "X" + soInt.value.ToString();
    }

    public void Update()
    {
        uiTextValue.text = "X" + soInt.value.ToString();        
    }
}
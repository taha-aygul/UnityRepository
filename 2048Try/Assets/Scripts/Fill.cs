using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Fill : MonoBehaviour
{
    int value;
   [SerializeField] TextMeshProUGUI valueDisplay;


    public void FillValueUpdate(int value)
    {
        this.value = value;
        valueDisplay.text = this.value.ToString();
    }
}

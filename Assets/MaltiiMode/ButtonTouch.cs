using System;
using UnityEngine;

public class ButtonTouch : MonoBehaviour
{
    void Start()
    {
        GetComponent<Animation>().Stop("ButtonTouch");
    }
    public void ButtonClick()
    {
        GetComponent<Animation>().Play("ButtonTouch");
    }
}
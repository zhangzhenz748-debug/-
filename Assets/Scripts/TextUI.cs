using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUI : MonoBehaviour
{
    public Image image;
    public Text text;
    public void OnShow(string Description)
    {
        image.enabled=true;
        text.gameObject.SetActive(true);
        text.text=Description;
    }
    public void OnHide()
    {
        image.enabled=false;
        text.gameObject.SetActive(false);
    }
}

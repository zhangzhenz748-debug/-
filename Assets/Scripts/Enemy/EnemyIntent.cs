using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyIntent : MonoBehaviour//意图系统
{
    public Image image;
    public void Intent(Sprite sprite)
    {
        image.sprite=sprite;
    }
}

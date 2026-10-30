using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intent : MonoBehaviour
{
    [Header("目标物体")] public RectTransform Target;
    [Header("需要被改变位置的物体")] public RectTransform Intentimage;
    [Header("是否在上面")] public bool isTop;
    void Awake()
    {
        SetDistance();
    }
    public void SetDistance()
    {
        float height = (Target.rect.height + Intentimage.rect.height) / 2;
        if (isTop)
        {
            Intentimage.position = Target.position;
            Intentimage.position = new Vector3(Intentimage.position.x, Intentimage.position.y + height);
        }
        else
        {
            Intentimage.position = Target.position;
            Intentimage.position = new Vector3(Intentimage.position.x, Intentimage.position.y - height*2);
        }
    }
}

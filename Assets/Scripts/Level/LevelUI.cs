using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelUI : MonoBehaviour
{
    public List<Image> images;
    public Sprite Cleared;//过关显示
    public Sprite NotCleared;//没过关显示
    public void AddLevelUI(int i)
    {
        if(i<images.Count&&i!=-1)
        images[i].sprite=Cleared;
    }
    public void InitLevelUI()
    {
        foreach(Image i in images)
        {
            i.sprite=NotCleared;
        }
    }
}

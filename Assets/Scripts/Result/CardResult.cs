using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardResult : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public int ID;//卡牌ID
    public string Description;//卡牌描述
    private Image image;//卡牌图片
    private TextUI textUI;//卡牌描述
    private Intent intent;//卡牌描述显示的位置
    void Awake()
    {
        intent = GetComponent<Intent>();
        image = GetComponent<Image>();
        textUI = GetComponent<TextUI>();
    }
    public void SetImage(Sprite sprite)
    {
        image.sprite = sprite;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        intent.SetDistance();
        textUI.OnShow(Description);
        //使用计时器
        //通知显示卡牌效果文字
        //EventHandler.CallShow(Description);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        textUI.OnHide();
        //关闭卡牌效果文字
        //EventHandler.CallHide();
    }

    public void OnPointerClick(PointerEventData eventData)//点击
    {
        //将数据传到玩家手牌里
        textUI.OnHide();
        EventHandler.CallCardID(ID);
        EventHandler.CallCloseCardResult();
    }
}

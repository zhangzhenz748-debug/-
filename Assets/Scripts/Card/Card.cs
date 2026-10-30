using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int ID;//卡牌ID
    public GameObject player;//玩家
    public int energy;
    private Vector3 pos;
    private CanvasGroup canvasGroup;
    private Image image;//卡牌图片
    public string Description;//卡牌描述
    private TextUI textUI;//卡牌描述
    private Intent intent;//卡牌描述显示的位置
    private int index;//位置
    private Vector3 Angle;
    void Awake()
    {
        intent = GetComponent<Intent>();
        image = GetComponent<Image>();
        canvasGroup = GetComponent<CanvasGroup>();
        textUI = GetComponent<TextUI>();
    }
    public void SetImage(Sprite sprite)
    {
        image.sprite = sprite;
        canvasGroup.blocksRaycasts = true;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        pos = (Vector3)eventData.position - transform.position;
        transform.localScale = new Vector3(1.2f, 1.2f, 0);
        canvasGroup.blocksRaycasts = false;
        transform.DORotate(Vector3.zero, 0.5f);
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition - pos;
        transform.DORotate(Vector3.zero, 0.5f);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject obj = eventData.pointerCurrentRaycast.gameObject;
        if (obj == null)
        {
            EventHandler.CallCardPositionUpdate();
            canvasGroup.blocksRaycasts = true;
            return;
        }
        if (obj.tag == "NPC")
        {
            if (Energy.instance.ConsumEnergy(energy))
            {
                //执行卡牌功能
                Debug.Log("执行卡牌");
                //检测卡牌是否能执行
                EventHandler.CallExecute(player, obj, ID);
                //执行完卡牌将数据给到弃牌组
                EventHandler.CallRecycleCard(gameObject, ID);
                //播放玩家攻击动画
                //播放音效
                EventHandler.CallAudiosfx();
            }
            else
            {
                Debug.Log("没能量了");
            }
        }
        else
        {
            //回到原位置
            Debug.Log("回到原位置");
            //通知
        }
        transform.DORotate(Angle, 0.5f);
        transform.localScale = new Vector3(1f, 1f, 0);
        EventHandler.CallCardPositionUpdate();
        Debug.Log(obj.tag);
        canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1.2f, 1.2f, 0);
        index = transform.GetSiblingIndex();
        transform.SetAsLastSibling();
        intent.SetDistance();
        textUI.OnShow(Description);
        Angle = transform.localEulerAngles;
        transform.DORotate(Vector3.zero, 0.5f);
        //transform.position = new Vector3();
        //使用计时器
        //通知显示卡牌效果文字
        //EventHandler.CallShow(Description);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1f, 1f, 0);
        transform.SetSiblingIndex(index);
        textUI.OnHide();
        transform.DORotate(Angle, 0.5f);
        //关闭卡牌效果文字
        //EventHandler.CallHide();
    }
}

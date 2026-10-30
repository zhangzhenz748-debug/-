using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using System.Linq;
using DG.Tweening;
public class CardManager : MonoBehaviour
{
    [Header("卡牌预制体")] public GameObject Prefabe;//卡牌预制体
    [Header("最大卡牌数量")] public int MaxCard;//最大卡牌数量
    [Header("每次发牌数量")] public int DrawCount;
    [Header("玩家")] public GameObject player;
    [Header("扇形布局")]
    //距离圆心距离
    public float W = 300f;                 // 半宽，按你手牌区调
    public float thetaDeg = 30f;
    public float CAngle;//每张牌的固定角度
    public Card_SO card_SO;//卡牌SO文件 用于存储所有卡牌数据 
    //当前手牌
    public List<int> Hand;
    private IObjectPool<GameObject> CardPool;//对象池
    //发牌组
    private List<int> DrawPile;
    //弃牌组
    private List<int> DiscardPile;

    private float width;
    private float height;
    private RectTransform rect;
    void Awake()
    {
        //初始化List
        DrawPile = new List<int>();
        DiscardPile = new List<int>();
        //初始化对象池
        CardPool = new ObjectPool<GameObject>(createFunc: CreatePooledItem,
            actionOnGet: OnTakeFromPool,
            actionOnRelease: OnReturnedToPool,
            actionOnDestroy: OnDestroyPoolObject);
        //获取预制体宽高
        RectTransform rt = Prefabe.GetComponent<RectTransform>();
        width = rt.rect.width;
        height = rt.rect.height;
        rect = GetComponent<RectTransform>();
    }
    void Start()
    {
        DrawPile.AddRange(Hand);//第一次将所有手牌加到发牌组
                                // StartDeal();
    }
    void OnEnable()
    {
        EventHandler.CardPositionUpdate += UpdateHandLayout;
        EventHandler.RecycleCard += OnRecycleCard;
        EventHandler.Deal += StartDeal;
        EventHandler.RecycleAllCard += OnRecycleAllCard;
        EventHandler.CardID += OnCardID;
    }
    void OnDisable()
    {
        EventHandler.CardPositionUpdate -= UpdateHandLayout;
        EventHandler.RecycleCard -= OnRecycleCard;
        EventHandler.Deal -= StartDeal;
        EventHandler.RecycleAllCard -= OnRecycleAllCard;
        EventHandler.CardID -= OnCardID;
    }

    private void OnCardID(int ID)
    {
        //将新加入的卡牌加入发牌组，同时更新手牌
        DrawPile.Add(ID);
        Hand.Add(ID);
    }

    private void StartDeal()//开始发牌
    {
        StartCoroutine(Deal());
    }
    /// <summary>
    /// 回收卡牌
    /// </summary>
    /// <param name="obj">回收的物体</param>
    /// <param name="ID">回收的ID</param>
    private void OnRecycleCard(GameObject obj, int ID)
    {
        DiscardPile.Add(ID);
        CardPool.Release(obj);
        //UpdateHandLayout();
    }
    #region 对象池
    // 1. 当池中没有对象时，如何创建新对象
    private GameObject CreatePooledItem()
    {
        GameObject obj = Instantiate(Prefabe, transform);
        obj.transform.position=Vector3.zero;
        return obj;
    }
    // 2. 从池中获取对象时调用
    private void OnTakeFromPool(GameObject obj)
    {
        obj.SetActive(true);
    }
    // 3. 将对象归还给池时调用
    private void OnReturnedToPool(GameObject obj)
    {
        obj.transform.position=Vector3.zero;
        obj.SetActive(false);
    }
    // 4. 当池满销毁多余对象时调用
    private void OnDestroyPoolObject(GameObject obj)
    {
        Destroy(obj);
    }
    #endregion
    #region 修改卡牌位置
    private List<Card> cards = new List<Card>();//自身所有的子物体卡牌
    private void GetChildrenCard()
    {
        cards.Clear();
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
            {
                cards.Add(child.GetComponent<Card>());
            }
        }
    }
    //使用算法计算卡牌的位置
    public void UpdateHandLayout()
    {
        GetChildrenCard();
        int count = cards.Count;
        if (count == 0) return;
        float thetaRad = thetaDeg * Mathf.Deg2Rad;
        float R = W / Mathf.Sin(thetaRad);
        //每一步角度
        float Angle = (thetaDeg - (-thetaDeg)) / (count - 1);
        float CardAngle = -thetaDeg;
        float rand = CardAngle * Mathf.PI / 180f; ;
        float data = R * Mathf.Cos(rand);
        if(CAngle*cards.Count<thetaDeg*2)
        {
            Angle=CAngle;
            CardAngle=-CAngle*cards.Count/2;
        }
        for (int i = 0; i < count; i++)
        {
            RectTransform cardRect = cards[i].GetComponent<RectTransform>();
            rand = CardAngle * Mathf.PI / 180f;
            float X = R * Mathf.Sin(rand);
            float Y = R * Mathf.Cos(rand);
            cardRect.DOMove(new Vector2(transform.position.x+X, Y - data),1);
           // = new Vector2(X, Y - data);
            cardRect.DORotate(new Vector3(0,0,-CardAngle), 1f);
            CardAngle += Angle;
        }
    }

    #endregion
    //使用对象池进行创建卡牌
    #region 洗牌/发牌/回收所有卡牌
    private void OnRecycleAllCard()
    {
        GetChildrenCard();
        foreach (var i in cards)
        {
            GameObject obj = i.gameObject;
            int ID = i.ID;
            OnRecycleCard(obj, ID);
        }
    }
    //使用洗牌算法
    private void Shuffle(List<int> DrawPile)
    {
        int Len = DrawPile.Count - 1;
        if (Len < DrawCount)//当卡牌小于发牌数量
        {
            DrawPile.AddRange(DiscardPile);
            Len = DrawPile.Count - 1;
            DiscardPile.Clear();//清空弃牌组
            //将弃牌组的卡牌拿过来
        }
        int i;
        for (int j = Len; j > 0; j--)
        {
            i = Random.Range(0, j + 1);
            int m = DrawPile[i];
            DrawPile[i] = DrawPile[j];
            DrawPile[j] = m;
        }
    }
    public IEnumerator Deal()//发牌
    {
        for (int i = 0; i < DrawCount; i++)
        {
            yield return new WaitForSeconds(0.2f);
            yield return StartCoroutine(Draw());
        }
    }
    private IEnumerator Draw()//发牌
    {
        if (DrawPile.Count >= 1)
        {
            //限制一下手牌数量不能超过最大值

            GameObject obj = CardPool.Get();
            if (obj != null)
            {
                Card card = obj.GetComponent<Card>();
                card.ID = DrawPile[0];//赋值ID
                //设置卡牌图片
                CardData date = card_SO.cardDates.FirstOrDefault(i => i.ID == card.ID);
                //date.function.value=date.date;
                if (date == null)
                {
                    CardPool.Release(obj);
                    yield break;
                }
                card.Description = date.Description;
                card.player = player;
                card.SetImage(date.sprite);
                card.energy = date.energy;
                UpdateHandLayout();//更新卡牌位置
                                   //修改card的数据用于初始化
                DrawPile.RemoveAt(0);
            }
        }
        else
        {
            Shuffle(DrawPile);
        }
        yield return null;
    }
    #endregion
}

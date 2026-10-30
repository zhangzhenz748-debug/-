using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using System.Linq;
public class EnemyManager : MonoBehaviour//敌人管理器
{
    public static EnemyManager instance;//单例模式
    public Enemy_SO enemy_SO;
    public GameObject Player;//玩家
    public GameObject Prefabe;//敌人预制体
    public bool PlayerTrun = false;
    private List<Enemy> Enemies;//所有敌人
    private IObjectPool<GameObject> EnemyPool;//对象池
    public bool EnemyDeath = true;
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else if (instance == null)
        {
            instance = this;
        }
        Enemies = new List<Enemy>();
        EnemyPool = new ObjectPool<GameObject>(createFunc: CreatePooledItem,
            actionOnGet: OnTakeFromPool,
            actionOnRelease: OnReturnedToPool,
            actionOnDestroy: OnDestroyPoolObject);
    }
    void OnEnable()
    {
        EventHandler.EnemyAttack += EnemyAttack;
        EventHandler.EnemyDeath += OnEnemyDeath;
        EventHandler.EnemySpaw += OnEnemySpawn;
    }
    void OnDisable()
    {
        EventHandler.EnemyAttack -= EnemyAttack;
        EventHandler.EnemyDeath -= OnEnemyDeath;
        EventHandler.EnemySpaw -= OnEnemySpawn;
    }

    private void OnEnemyDeath(GameObject obj)
    {
        // EnemyDeath=false;
        EnemyPool.Release(obj);
    }
    private void OnEnemySpawn(int EnemyID)//创建敌人
    {
        GameObject EnemyObj = EnemyPool.Get();
        //给怪物数据
        if (EnemyObj != null)
        {
            Enemy enemy = EnemyObj.GetComponent<Enemy>();
            if (enemy != null)
            {
                EnemyDate date = enemy_SO.enemyDates.FirstOrDefault(i => i.ID == EnemyID);

                if (date != null)
                {
                    enemy.SetEnemy(Player, date);
                }
            }
        }
    }
    #region 对象池
    //使用对象池回收已经死亡的敌人
    private GameObject CreatePooledItem()//创建敌人
    {
        GameObject obj = Instantiate(Prefabe, transform);
        return obj;
    }
    private void OnTakeFromPool(GameObject obj)//获取敌人
    {
        obj.SetActive(true);
    }
    private void OnReturnedToPool(GameObject obj)//回收敌人
    {
        obj.SetActive(false);
        CheckWin();
    }
    private void OnDestroyPoolObject(GameObject obj)//清除多余敌人
    {
        Destroy(obj);
    }
    #endregion
    #region 敌人攻击
    //获取所有敌人
    void GetChildrenEnemy()
    {
        Enemies.Clear();
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
            {
                Enemy enemy = child.GetComponent<Enemy>();
                Enemies.Add(enemy);
            }
        }
    }
    void CheckWin()
    {
        GetChildrenEnemy();
        if (Enemies.Count == 0)//所有怪物死亡开启胜利选择
        {
            StartCoroutine(AllEnemyDeath());
        }
        EnemyDeath = true;
    }
    //等待0.2f
    IEnumerator AllEnemyDeath()
    {
        yield return new WaitForSeconds(0.3f);
        EventHandler.CallWin();
        EventHandler.CallRecycleAllCard();//回收所有卡牌
        Debug.Log("胜利");
    }
    //所有敌人攻击
    void EnemyAttack()
    {
        StartCoroutine(AllEnemyAttack());
    }
    IEnumerator AllEnemyAttack()
    {
        Debug.Log("怪物控制器敌人攻击1");
        yield return new WaitUntil(() => EnemyDeath);//怪物非死亡的情况下才能攻击
        Debug.Log("怪物控制器敌人攻击2");
        GetChildrenEnemy();
        foreach (var enemy in Enemies)
        {
            yield return new WaitForSeconds(0.2f);
            yield return StartCoroutine(Attack(enemy));
            yield return new WaitUntil(() => enemy.isPlay);
            enemy.isPlay = false;
        }
        PlayerTrun = true;
    }
    IEnumerator Attack(Enemy enemy)
    {
        enemy.Attack();
        yield return null;
    }
    #endregion
}

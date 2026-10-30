using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TurnState
{
    PlayerTurn,//玩家回合
    EnemyTurn//敌人回合
}
public class TurnManager : MonoBehaviour//回合控制器
{
    public GameObject game;
    void OnEnable()
    {
        EventHandler.PlayerTrun+=OnPlayerTrun;
        EventHandler.EnemyTurn+=OnEnemyTurn;
    }
    void OnDisable()
    {
        EventHandler.PlayerTrun-=OnPlayerTrun;
        EventHandler.EnemyTurn-=OnEnemyTurn;
    }
    public void OnPlayerTrun()//玩家回合
    {
        //发牌
        EventHandler.CallDeal();
        //开启回合结束
        game.SetActive(true);
        Energy.instance.InitEnergy();
        //执行一些玩家操作
    }
    public void OnEnemyTurn()//敌人回合
    {
        //关闭回合结束
        game.SetActive(false);
        //回收玩家的卡牌
        EventHandler.CallRecycleAllCard();
        //敌人攻击
        //通知敌人攻击
        EventHandler.CallEnemyAttack();
        StartCoroutine(EnemyTurn());
        Debug.Log("敌人回合");
    }
    private IEnumerator EnemyTurn()
    {
        yield return new WaitUntil(()=>EnemyManager.instance.PlayerTrun);//等待敌人攻击完成
        EnemyManager.instance .PlayerTrun=false;
        Debug.Log("敌人攻击");
        //进入玩家回合
        OnPlayerTrun();
    }
}

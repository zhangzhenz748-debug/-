using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class EventHandler
{
    public static event Action CardPositionUpdate;//修改卡牌位置
    public static void CallCardPositionUpdate()
    {
        CardPositionUpdate?.Invoke();
    }
    public static event Action<GameObject, int> RecycleCard;//回收单个卡牌
    public static void CallRecycleCard(GameObject obj, int ID)
    {
        RecycleCard?.Invoke(obj, ID);
    }
    public static event Action RecycleAllCard;//回收所有卡牌
    public static void CallRecycleAllCard()
    {
        RecycleAllCard?.Invoke();
    }
    public static event Action<float, float, float> Audio;//修改声音
    public static void CallAudio(float master, float bgm, float sfx)
    {
        Audio?.Invoke(master, bgm, sfx);
    }
    public static event Action Audiosfx;//播放音效
    public static void CallAudiosfx()
    {
        Audiosfx?.Invoke();
    }
    public static event Action SetAudio;//修改声音
    public static void CallSetAudio()
    {
        SetAudio?.Invoke();
    }
    public static event Action Deal;//发牌
    public static void CallDeal()
    {
        Deal?.Invoke();
    }
    public static event Action PlayerTrun;//玩家回合
    public static void CallPlayerTrun()
    {
        PlayerTrun?.Invoke();
    }
    public static event Action EnemyTurn;//敌人回合
    public static void CallEnemyTurn()
    {
        EnemyTurn?.Invoke();
    }
    public static event Action<GameObject, GameObject, int> Execute;//执行攻击
    /// <summary>
    /// 执行攻击
    /// </summary>
    /// <param name="Caster">发起者</param>
    /// <param name="Target">目标</param>
    /// <param name="ID">卡牌编号</param>
    public static void CallExecute(GameObject Caster, GameObject Target, int ID)
    {
        Execute?.Invoke(Caster, Target, ID);
    }
    public static event Action EnemyAttack;//敌人攻击
    public static void CallEnemyAttack()
    {
        EnemyAttack?.Invoke();
    }
    public static event Action<string> Show;//显示卡牌描述
    public static void CallShow(string Description)
    {
        Show?.Invoke(Description);
    }
    public static event Action Hide;//关闭卡牌描述
    public static void CallHide()
    {
        Hide?.Invoke();
    }
    public static event Action<GameObject> EnemyDeath;//怪物死亡
    public static void CallEnemyDeath(GameObject game)
    {
        EnemyDeath?.Invoke(game);
    }
    public static event Action<int> EnemySpaw;//敌人生成
    public static void CallEnemySpaw(int ID)
    {
        EnemySpaw?.Invoke(ID);
    }
    public static event Action<Sprite> EnemyIntent;//敌人意图
    public static void CallEnemyIntent(Sprite ID)
    {
        EnemyIntent?.Invoke(ID);
    }
    public static event Action Win;//胜利
    public static event Action Lose;//失败
    public static void CallWin()
    {
        Win?.Invoke();
    }
    public static void CallLose()
    {
        Lose?.Invoke();
    }
    public static event Action NextLevel;//开启下一关
    public static void CallNextLevel()
    {
        NextLevel?.Invoke();
    }
    public static event Action<Transform, int> DamageText;//伤害数值显示
    public static void CallDamageText(Transform rect, int data)
    {
        DamageText?.Invoke(rect, data);
    }
    public static event Action CloseCardResult;//关闭卡牌选择
    public static void CallCloseCardResult()
    {
        CloseCardResult?.Invoke();
    }
    public static event Action<int> CardID;//选择卡牌传输卡牌数据
    public static void CallCardID(int ID)
    {
        CardID?.Invoke(ID);
    }
    public static event Action<string> Scene;
    public static void CallScene(string name)
    {
        Scene?.Invoke(name);
    }
}

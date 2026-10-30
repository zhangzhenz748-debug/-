using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IEnemyLogic
{
    public void EnemyLogic();
    public void EnemyLogic(GameObject Enemy);//攻击逻辑
    public void EnemyLogic(GameObject Enemy,GameObject Player);
    public void EnemyLogic(GameObject Enemy,EnemyDate enemyDate);
    public void EnemyLogic(GameObject Player, EnemyDate enemyDate, GameObject Enemy, bool StartIntent,int CurrID, out int NextID);
}
public class EnemyLogicBase : ScriptableObject,IEnemyLogic
{
    public virtual void EnemyLogic(GameObject Enemy)
    {
    }
    public virtual void EnemyLogic(GameObject Enemy, GameObject Player)
    {
    }

    public virtual void EnemyLogic(GameObject Enemy, EnemyDate enemyDate)
    {
    }
    public virtual void EnemyLogic()
    {
    }
    public virtual void EnemyLogic(GameObject Player, EnemyDate enemyDate, GameObject Enemy, bool StartIntent,int CurrID,out int NextID)
    {
        NextID=0;
    }
}

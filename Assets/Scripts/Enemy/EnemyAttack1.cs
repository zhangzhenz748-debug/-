using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyAttack1", menuName = "EnemyLogic/EnemyAttack1")]
public class EnemyAttack1 : EnemyLogicBase
{
    public override void EnemyLogic(GameObject Player, EnemyDate enemyDate, GameObject Enemy, bool StartIntent, int CurrID, out int NextID)
    {
        int ID = CurrID;
        if (CurrID == enemyDate.skills.Count - 1)
        {
            NextID = 0;
        }
        else
        {
            NextID=CurrID+1;
        }
        if (StartIntent&& ID >= 0 && ID < enemyDate.skills.Count)
        {
            EventHandler.CallExecute(Enemy,Player, enemyDate.skills[ID].ID);
            Debug.Log("敌人攻击");
        }
        //显示下一步意图
        Enemy.GetComponent<EnemyIntent>().Intent(enemyDate.skills[NextID].sprite);
        //EventHandler.CallEnemyIntent(enemyDate.skills[NextID].sprite);
        Debug.Log("敌人意图");
    }
}

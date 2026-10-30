using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class LevelManager : MonoBehaviour
{
    //关卡配置文件
    //胜利后调出胜利面板
    //继续游戏后进入下一关
    public Enemy_SO enemy_SO;
    public LevelUI levelUI;
    //难度
    private int Difficulty;//难度
    private int Level = 0;//关卡
    List<EnemyDate> Minions;
    List<EnemyDate> Elites;
    List<EnemyDate> Booses;
    void Awake()
    {
        Minions = enemy_SO.enemyDates.Where(enemy => enemy.Minion == true).ToList();
        Elites = enemy_SO.enemyDates.Where(enemy => enemy.Elite == true).ToList();
        Booses = enemy_SO.enemyDates.Where(enemy => enemy.Boos == true).ToList();
    }
    void Start()
    {
        LevelSpawn();
    }
    void OnEnable()
    {
        EventHandler.NextLevel += LevelSpawn;
    }
    void OnDisable()
    {
        EventHandler.NextLevel -= LevelSpawn;
    }
    public void LevelSpawn()//关卡生成
    {
        levelUI.AddLevelUI(Level);
        int i;
        Level++;//关卡自增
        EnemyDate enemyDate;
        if (Level == 3)//精英怪
        {
            i = Random.Range(0, Elites.Count);
            enemyDate = Elites[i];
        }
        else if (Level == 5)//BOOS
        {
            Difficulty += 1;//难度加1
            i = Random.Range(0, Booses.Count);
            enemyDate = Booses[i];
            
        }
        else//小怪
        {
            i = Random.Range(0, Minions.Count);
            enemyDate = Minions[i];
        }
        if (Level == 6)
        {
            Level = 0;
            levelUI.InitLevelUI();
            levelUI.AddLevelUI(Level);
            Debug.Log("通关");
        }
        EventHandler.CallEnemySpaw(enemyDate.ID);
        EventHandler.CallDeal();
        Energy.instance.InitEnergy();
    }
    IEnumerator AllEnemyDeath()
    {
        yield return new WaitForSeconds(0.3f);
        EventHandler.CallWin();
        EventHandler.CallRecycleAllCard();//回收所有卡牌
        Debug.Log("胜利");
    }
}

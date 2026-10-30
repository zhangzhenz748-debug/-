using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour// 当一个敌人基类
{
    private EnemyDate enemyDate;//敌人数据
    //
    private GameObject Player;//玩家
    public bool isPlay = false;
    private Animator animator;
    private Image image;
    private Health health;
    private int NextID=-1;
    //private bool StartIntent=false;
    void Awake()
    {
        health = GetComponent<Health>();
        image = GetComponent<Image>();
        animator = GetComponent<Animator>();
    }
    public void SetEnemy(GameObject Player, EnemyDate enemy)//设置怪物
    {
        this.Player = Player;
        isPlay=false;
        enemyDate = enemy;
        NextID = -1;//状态重置
        animator.runtimeAnimatorController = enemy.animator;
        image.sprite = enemy.sprite;
        health.StartHealth(enemy.MaxHealth);
        enemyDate.AttackLogic.EnemyLogic(Player, enemyDate,gameObject,false,NextID,out NextID);
    }
    public void Attack()
    {
        //播放攻击动画
        StartCoroutine(IAttack());
    }
    IEnumerator IAttack()
    {
        animator.SetTrigger("攻击");
        yield return null;
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(stateInfo.length + 0.2f);
        isPlay = true;
        EventHandler.CallAudiosfx();
        enemyDate.AttackLogic.EnemyLogic(Player, enemyDate,gameObject,true,NextID,out NextID);//开始攻击
    }
}

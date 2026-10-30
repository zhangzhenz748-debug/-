using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[System.Serializable]
public class EnemyDate
{
    [Header("敌人名称")] public string name;//敌人名称
     [Header("敌人名称")] public int ID;//敌人ID
    [Header("敌人图片")] public Sprite sprite;
    [Header("敌人血量")] public float MaxHealth;
    //1.敌人动画
    [Header("敌人动画")] public AnimatorOverrideController animator;
    [Header("攻击逻辑")] public EnemyLogicBase AttackLogic;
    [Header("敌人技能")] public List<Skill> skills;
    [Header("是否是小怪")]public bool Minion;
    [Header("是否是精英怪")]public bool Elite;
    [Header("是否是Boos")]public bool Boos;
}
[System.Serializable]
public class Skill
{
    //2.敌人技能
    public int ID;//技能编号
    //3.技能图标
    public Sprite sprite;
   //public string str;//技能描述
}

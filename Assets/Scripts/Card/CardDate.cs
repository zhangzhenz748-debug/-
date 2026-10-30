using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CardData
{
    [Header("卡牌图片")] public Sprite sprite;//卡牌图片
    [Header("卡牌ID")] public int ID;
    [Header("卡牌数值")] public int date;//卡牌数值 多少回合启动/打多少伤害/回复多少血量
    [Header("特效动画")] public AnimatorOverrideController animator;
    [Header("消耗多少能量")] public int energy;//消耗多少能量
    [Header("卡牌名称")] public string name;//卡牌名称
    [Header("卡牌描述")] public string Description;//卡牌描述
    [SerializeReference]
    [Header("卡牌功能")] public Function function;

}

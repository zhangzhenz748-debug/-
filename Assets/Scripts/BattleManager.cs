using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleManager : MonoBehaviour//战斗管理器
{
    public Card_SO card_SO;
    void OnEnable()
    {
        EventHandler.Execute += Battle;
    }
    void OnDisable()
    {
        EventHandler.Execute -= Battle;
    }
    public void Battle(GameObject Caster,GameObject Target, int ID)
    {
        if (Target == null)
            return;
        //传入目标和功能函数
        //检测自身状态
        //执行卡牌功能
        CardData Date = card_SO.cardDates.FirstOrDefault(i => i.ID == ID);
        if (Date != null)
        {
            Date.function.value = Date.date;
            //对特效进行赋值
            Caster.GetComponent<VfxPlayer>().SetVfxAnimator(Date.animator);
            Target.GetComponent<VfxPlayer>().SetVfxAnimator(Date.animator);
            //再到卡牌功能类里启动
            Date.function.Functions(Caster,Target);
        }
    }
}

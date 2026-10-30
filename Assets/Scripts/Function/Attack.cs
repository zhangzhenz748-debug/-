using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Attack", menuName = "Function/Attack")]
public class Attack : Function
{
    public override void Functions(GameObject Caster, GameObject Target)
    {
        Health health = Target.GetComponent<Health>();
        health.SetCurrHealth(-base.value);
        Target.GetComponent<VfxPlayer>().OnPlayerAnimator();
        //显示伤害数值
        EventHandler.CallDamageText(Target.GetComponent<Transform>(), (int)base.value);
        health.SetHealth();
    }
}

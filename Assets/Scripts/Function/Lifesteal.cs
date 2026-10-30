using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Lifesteal", menuName = "Function/Lifesteal")]
public class Lifesteal : Function
{
    public override void Functions(GameObject Caster, GameObject Target)
    {
        Health health = Target.GetComponent<Health>();
        health.SetCurrHealth(-base.value);
        Health Casterhealth = Caster.GetComponent<Health>();
        Casterhealth.SetCurrHealth(base.value / 2);
        Casterhealth.SetHealth();
        EventHandler.CallDamageText(Caster.GetComponent<Transform>(), (int)base.value);
        EventHandler.CallDamageText(Target.GetComponent<Transform>(), (int)base.value);
        health.SetHealth();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Heal", menuName = "Function/Heal")]
public class Heal : Function//回血
{
    public override void Functions(GameObject Caster, GameObject Target)
    {
        Health health = Caster.GetComponent<Health>();
        health.SetCurrHealth(base.value);
        Caster.GetComponent<VfxPlayer>().OnPlayerAnimator();
        EventHandler.CallDamageText(Caster.GetComponent<Transform>(), (int)base.value);
        health.SetHealth();
    }
}


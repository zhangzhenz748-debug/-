using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VfxPlayer : MonoBehaviour
{
    public Animator Vfxanimator;//特效动画
    public Image image;
    private AnimatorOverrideController animatorController;
    public void SetVfxAnimator(AnimatorOverrideController controller)
    {
        Vfxanimator.runtimeAnimatorController = controller;
    }
    public void OnPlayerAnimator()
    {
        StartCoroutine(PlayerAnimator());
    }
    IEnumerator PlayerAnimator()
    {
        image.enabled = true;
        Vfxanimator.Play("VFXBase");
        AnimatorStateInfo stateInfo = Vfxanimator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(stateInfo.length);
        image.enabled = false;
    }
}

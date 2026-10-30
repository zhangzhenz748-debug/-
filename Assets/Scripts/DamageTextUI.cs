using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DamageTextUI : MonoBehaviour
{
    public Text text;//显示的伤害数字
    public float Speed;//速度
    void OnEnable()
    {
        EventHandler.DamageText += SetPos;
    }
    void OnDisable()
    {
        EventHandler.DamageText -= SetPos;
    }
    private void SetPos(Transform rect, int data)
    {
        transform.position = rect.position;
        transform.localScale = Vector3.one;
        text.text = data.ToString();
        text.enabled = true;
        StartCoroutine(Mov());
    }
    //进行回收
    //使用协程进行移动
    IEnumerator Mov()
    {
        Color c = text.color;
        transform.DOMove(new Vector3(transform.position.x, transform.position.y + 40, 0), 1);
        transform.DOScale(new Vector3(1.5f, 1.5f, 0), 1);
        yield return new WaitForSeconds(1f);
        text.enabled = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Health : MonoBehaviour
{
    public float MaxHealth;//最大生命值
    public float CurrHealth;//当前生命值
    public Slider slider;
    public Text text;//血量显示
    private Animator animator;
    private Image image;//图片
    void Awake()
    {
        image=GetComponent<Image>();
        animator = GetComponent<Animator>();
        StartHealth(MaxHealth);
    }
    public void StartHealth(float health)
    {
        MaxHealth = CurrHealth = health;
        text.text = CurrHealth.ToString() + "/" + MaxHealth.ToString();
        slider.value = 1;
    }
    public void SetCurrHealth(float data)
    {
        if(CurrHealth+data>=MaxHealth)
        {
            CurrHealth=MaxHealth;
        }
        else
        {
            CurrHealth+=data;
        }
    }
    public void SetMaxHealth(float data)
    {
        MaxHealth+=data;
    }
    public void SetHealth()
    {
        text.text = CurrHealth.ToString() + "/" + MaxHealth.ToString();
        if( slider.value > CurrHealth / MaxHealth)
        {
            StartCoroutine(IHurt());
        }
        slider.value = CurrHealth / MaxHealth;
        
    }
    IEnumerator IHurt()//受伤
    {
        if (gameObject.tag == "NPC")
        {
            EnemyManager.instance.EnemyDeath = false;
            Debug.Log("怪物受伤1");
        }
        animator.SetTrigger("受伤");
        yield return null;
        Color c=image.color;
        
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(stateInfo.length/5);
        c=Color.red;
        image.color=c;
        yield return new WaitForSeconds(stateInfo.length/5);
        c=Color.white;
        image.color=c;
        yield return new WaitForSeconds(stateInfo.length/5);
        c=Color.red;
        image.color=c;
        yield return new WaitForSeconds(stateInfo.length/5);
        c=Color.white;
        image.color=c;
        yield return new WaitForSeconds(stateInfo.length/5);
        if (CurrHealth <= 0)
        {
            StartCoroutine(IDeath());
        }
        else
        {
            if (gameObject.tag == "NPC")
            {
                EnemyManager.instance.EnemyDeath = true;
                Debug.Log("怪物受伤2");
            }
        }
        c=Color.white;
        image.color=c;
    }
    IEnumerator IDeath()//死亡
    {
        animator.SetTrigger("死亡");
        //通知怪物管理器回收
        if (gameObject.tag == "NPC")
        {
            EnemyManager.instance.EnemyDeath = false;
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            yield return new WaitForSeconds(stateInfo.length + 0.5f);
            EventHandler.CallEnemyDeath(gameObject);
        }
        else
        {
            //玩家死亡
            EventHandler.CallLose();
            Debug.Log("玩家死亡，游戏结算");
            //EventHandler.CallLose();
        }
        Debug.Log("死亡");
        yield return null;
    }
}

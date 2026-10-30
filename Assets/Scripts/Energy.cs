using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Energy : MonoBehaviour
{
    public int CurrEnergy;//当前能量
    public int MaxEnergy;//能量上限
    public Text text;//能量UI
    public GameObject game;
    public static Energy instance;//单例模式
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        InitEnergy();
    }
    void SetEnergy()
    {
        text.text = CurrEnergy.ToString();
    }
    public void InitEnergy()
    {
        text.text = MaxEnergy.ToString();
        CurrEnergy = MaxEnergy;
    }
    public bool ConsumEnergy(int data)
    {
        if (CurrEnergy - data < 0)
        {
            StartCoroutine(NotEnergy());
            return false;
        }
        CurrEnergy -= data;
        SetEnergy();
        return true;
    }
    IEnumerator NotEnergy()
    {
        game.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        game.SetActive(false);
    }
}

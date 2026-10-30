using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultPanel : MonoBehaviour//开启结算面板
{
    public GameObject Wingame;
    public GameObject Losegame;
    public VictoryResult victoryResult;
    void OnEnable()
    {
        EventHandler.Win += ShowWin;
        EventHandler.Lose += ShowLose;
        EventHandler.NextLevel+=CloseWin;
    }
    void OnDisable()
    {
        EventHandler.Win -= ShowWin;
        EventHandler.Lose -= ShowLose;
        EventHandler.NextLevel-=CloseWin;
    }
    public void ShowWin()
    {
        Wingame.SetActive(true);
        victoryResult.SetCard();
    }
    public void ShowLose()
    {
        Losegame.SetActive(true);
    }
    public void CloseWin()
    {
        Wingame.SetActive(false);
    }
    public void CloseLose()
    {
        Losegame.SetActive(false);
    }
}

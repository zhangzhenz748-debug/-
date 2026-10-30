using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject game;
    public void Open()
    {
        game.SetActive(true);
    }
    public void Close()
    {
        game.SetActive(false);
    }
    public void OpenAudio()
    {
        EventHandler.CallSetAudio();
    }
    public void EnemyTurn()
    {
        //修改为敌人
        EventHandler.CallEnemyTurn();
    }
    public void NextLevel()//到下一关
    {
        EventHandler.CallRecycleAllCard();
        EventHandler.CallNextLevel();
    }
    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}

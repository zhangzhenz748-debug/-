using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneSwitchSystem : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void OnEnable()
    {
        EventHandler.Scene += SceneSwitch;
    }
    void OnDisable()
    {
        EventHandler.Scene -= SceneSwitch;
    }
    public void SceneSwitch(string name)
    {
        if (name != string.Empty)
        {
            SceneManager.LoadScene(name);
        }
    }
}

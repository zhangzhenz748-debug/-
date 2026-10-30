using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitch : MonoBehaviour
{
    public string SceneName;
    public void StartSceneSwitch()
    {
        EventHandler.CallScene(SceneName);
    }
}

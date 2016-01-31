using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class CanvasManager : MonoBehaviour {
    public static Dictionary<Scene, Canvas> Canvases = new Dictionary<Scene, Canvas>();
    public static Dictionary<Scene, EventSystem> EventSystems = new Dictionary<Scene, EventSystem>();

    public static Stack<Scene> scenes = new Stack<Scene>();
    public static bool init = false;
    public static void Init()
    {
        init = true;
        for(int i = 0; i < SceneManager.sceneCount; i++)
        {
            Canvases.Add(SceneManager.GetSceneAt(i), null);
            EventSystems.Add(SceneManager.GetSceneAt(i), null);
        }
    }

    public static CanvasManager instance;



    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;//Avoid doing anything else
        }

        instance = this;
        Init();
        DontDestroyOnLoad(this.gameObject);
    }

}

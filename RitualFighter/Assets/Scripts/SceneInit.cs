using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneInit : MonoBehaviour {

    public EventSystem eventSystem;
    public Canvas canvas;
    public string SceneName;

	// Use this for initialization
	void Start () {
	    if(!CanvasManager.init)
        {
            CanvasManager.Init();
        }
        CanvasManager.Canvases[SceneManager.GetSceneByName(SceneName)] = canvas;
        CanvasManager.EventSystems[SceneManager.GetSceneByName(SceneName)] = eventSystem;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using System.Collections.Generic;

public class LevelLoader : MonoBehaviour {

	public void LoadLevel(string level)
	{
        CanvasManager.scenes.Clear();
        CanvasManager.scenes.Push(SceneManager.GetSceneByName(level));
		SceneManager.LoadScene (level, LoadSceneMode.Single);
	}

	public void LoadLevelAdditive(string level)
    {
        SceneManager.LoadScene(level, LoadSceneMode.Additive);
        Scene old;
        if (CanvasManager.scenes.Count < 1)
        {
            old = SceneManager.GetActiveScene();
            CanvasManager.scenes.Push(old);
        }
        old = CanvasManager.scenes.Peek();
        CanvasManager.scenes.Push(SceneManager.GetSceneByName(level));
        //CanvasManager.Canvases[old].enabled = false;
        CanvasManager.EventSystems[old].gameObject.SetActive(false);
        CanvasManager.Canvases[old].gameObject.SetActive(false);
    }

	public void UnloadLevel(string level)
    {
        Scene scene = CanvasManager.scenes.Pop();
        SceneManager.UnloadScene (level);
        CanvasManager.EventSystems[CanvasManager.scenes.Peek()].gameObject.SetActive(true);
        CanvasManager.Canvases[CanvasManager.scenes.Peek()].gameObject.SetActive(true);
    }
}

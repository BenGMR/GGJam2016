using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class LevelLoader : MonoBehaviour {

	public void LoadLevel(string level)
	{
		SceneManager.LoadScene (level, LoadSceneMode.Single);
	}

	public void LoadLevelAdditive(string level)
	{
		SceneManager.LoadScene(level, LoadSceneMode.Additive);
		//SceneManager.SetActiveScene(SceneManager.GetSceneByName(level));
		//EditorSceneManager.OpenScene (level, OpenSceneMode.AdditiveWithoutLoading);
	}

	public void UnloadLevel(string level)
	{
		SceneManager.UnloadScene (level);
	}
}

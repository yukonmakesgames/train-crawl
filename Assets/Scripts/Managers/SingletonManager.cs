using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This manager simply loads in the singleton scene if it is not already loaded in.
/// </summary>
public class SingletonManager : MonoBehaviour
{
	[SerializeField]
	private string singletonScene;

	void Awake()
	{
		if(!SceneManager.GetSceneByName(singletonScene).isLoaded)
		{
			SceneManager.LoadScene(singletonScene, LoadSceneMode.Additive);
		}
	}
}
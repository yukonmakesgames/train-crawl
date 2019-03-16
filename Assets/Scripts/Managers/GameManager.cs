using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public int CarNumber = 0;

    public void Restart(Scene _sceneToUnload)
    {
        CarNumber = 0;

        LoadingManager.Instance.LoadScene("Main", _sceneToUnload);
    }

    public void NextCar(Scene _sceneToUnload)
    {
        CarNumber++;

        LoadingManager.Instance.LoadScene("Main", _sceneToUnload);
    }
}

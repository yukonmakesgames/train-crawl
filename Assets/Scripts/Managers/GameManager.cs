using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public int CarNumber = 0;
    public WeaponObject DefaultWeapon;

    [HideInInspector]
    public WeaponObject Weapon;

    private void Awake()
    {
        Weapon = DefaultWeapon;
    }

    public void Restart(Scene _sceneToUnload)
    {
        CarNumber = 0;
        Weapon = DefaultWeapon;

        LoadingManager.Instance.LoadScene("Main", _sceneToUnload);
    }

    public void NextCar(Scene _sceneToUnload)
    {
        CarNumber++;

        LoadingManager.Instance.LoadScene("Main", _sceneToUnload);
    }
}

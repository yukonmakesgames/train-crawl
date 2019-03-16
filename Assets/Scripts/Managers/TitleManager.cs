using UnityEngine;

public class TitleManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("Attack"))
        {
            GameManager.Instance.Restart(gameObject.scene);
        }

        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }
    }
}

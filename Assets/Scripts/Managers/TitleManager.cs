using UnityEngine;

public class TitleManager : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.Play("Main Theme");
    }

    void Update()
    {
        if (Input.GetButtonDown("Attack"))
        {
            AudioManager.Instance.Play("Select");

            GameManager.Instance.Restart(gameObject.scene);
        }

        if (Input.GetButtonDown("Cancel"))
        {
            AudioManager.Instance.Play("Select");

            Application.Quit();
        }
    }
}

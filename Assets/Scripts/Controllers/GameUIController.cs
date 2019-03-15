using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    [Header("Health")]
    [SerializeField]
    private Image healthFill;

    [Header("References")]
    [SerializeField]
    private PlayerController playerController;

    void Update()
    {
        if(playerController != null)
        {
            healthFill.fillAmount = playerController.juice;
        }
    }
}

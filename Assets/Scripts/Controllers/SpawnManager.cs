using UnityEngine;
using DG.Tweening;

public class SpawnManager : MonoBehaviour
{
    [Header("Chunks")]
    [SerializeField]
    private GameObject[] chunks;

    [Header("Door")]
    [SerializeField]
    private Vector3 positionToGoTo;
    [SerializeField]
    private Rigidbody door;
    [SerializeField]
    private float doorOpenTime = 1f;
    [SerializeField]
    private Ease doorOpenEase;

    private bool cleared = false;
    
    private void Start()
    {
        Instantiate(chunks[Random.Range(0, chunks.Length)], transform);
    }

    private void Update()
    {
        if(!cleared)
        {
            EnemyController[] enemies = GetComponentsInChildren<EnemyController>();

            if (enemies == null || enemies.Length == 0)
            {
                cleared = true;

                Debug.Log("Car cleared.");

                door.DOMove(positionToGoTo, doorOpenTime).SetEase(doorOpenEase);

                AudioManager.Instance.Play("Door Open");
            }
        }
    }
}

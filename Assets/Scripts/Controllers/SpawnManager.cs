using UnityEngine;
using DG.Tweening;

public class SpawnManager : MonoBehaviour
{
    [Header("Chunks")]
    [SerializeField]
    private GameObject[] chunks;

    [Header("Door")]
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

                door.DORotate(new Vector3(0f, 0f, -90f), doorOpenTime).SetEase(doorOpenEase);
                //Winning garbage.
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeEmitterController : MonoBehaviour
{
    [SerializeField]
    private float lifeSpan;

    private void OnEnable()
    {
        StartCoroutine(killDelay());
	}

    IEnumerator killDelay()
    {
        yield return new WaitForSeconds(lifeSpan);
        Destroy(gameObject);
    }
}

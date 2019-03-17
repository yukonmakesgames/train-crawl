using UnityEngine;

public class BillboardController : MonoBehaviour
{
    private Camera camera;

    private void LateUpdate()
    {
        if(camera != null)
        {
            transform.LookAt(transform.position + camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);
        }
        else
        {
            camera = GameObject.FindGameObjectWithTag("Player Camera").GetComponent<Camera>();
        }
    }
}

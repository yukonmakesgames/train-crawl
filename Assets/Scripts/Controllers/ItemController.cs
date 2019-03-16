using UnityEngine;

public class ItemController : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    private WeaponObject weaponObject;

    [Header("Effect")]
    [SerializeField]
    private float freq = 0.125f;
    [SerializeField]
    private float amp = 1f;

    [Header("References")]
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private GameObject root;

    private float spriteYPosition = 0f;

    private void Awake()
    {
        spriteYPosition = spriteRenderer.transform.localPosition.y;

        if (weaponObject != null)
        {
            SetItem(weaponObject);
        }
    }

    private void Update()
    {
        spriteRenderer.transform.localPosition = new Vector3(0f, spriteYPosition + Mathf.Sin(Time.time * freq) * amp, 0f);
    }

    public void SetItem(WeaponObject _weaponObject)
    {
        weaponObject = _weaponObject;

        spriteRenderer.sprite = weaponObject.Sprite;
    }

    public WeaponObject Pickup()
    {
        Destroy(root);
        return weaponObject;
    }
}

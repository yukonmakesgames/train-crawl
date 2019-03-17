using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class WeaponObject : ScriptableObject
{
    public Sprite Sprite;
    public float Damage;
    public float Speed = 1f;
    public float Knockback = 1f;
    public float Stun = 1f;
    [Range(-1f, 1f)]
    public float JuiceCost = -0.125f;
    public float SlashSpeed = 1f;
    public Vector3 SlashSize = Vector3.one;
}

using UnityEngine;

public class KillerShoot : MonoBehaviour
{
    public WeaponShoot weapon;
    public float shootDistance = 15f;
    public Transform player;

    void Update()
    {
        if (player == null || weapon == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= shootDistance)
        {
            weapon.Shoot();
        }
    }
}

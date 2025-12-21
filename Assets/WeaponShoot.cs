using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    [Header("Shoot Settings")]
    public float range = 100f;
    public int damage = 20;
    public float fireRate = 0.3f;

    [Header("References")]
    public Camera shootCamera;
    public Transform firePoint;
    public ParticleSystem muzzleFlash;
    public LineRenderer bulletLine;

    private float nextFireTime = 0f;

    void Start()
    {
        if (bulletLine != null)
            bulletLine.enabled = false;
    }

    void Update()
    {
        // PLAYER INPUT
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    // 🔥 MUST BE PUBLIC
    public void Shoot()
    {
        if (muzzleFlash != null)
            muzzleFlash.Play();

        Ray ray = new Ray(shootCamera.transform.position, shootCamera.transform.forward);
        RaycastHit hit;

        Vector3 endPoint;

        if (Physics.Raycast(ray, out hit, range))
        {
            endPoint = hit.point;

            // DAMAGE ENEMY
            EnemyHealth enemy = hit.collider.GetComponent<EnemyHealth>();
            if (enemy != null)
                enemy.TakeDamage(damage);

            // DAMAGE PLAYER (for killer)
            PlayerHealth player = hit.collider.GetComponent<PlayerHealth>();
            if (player != null)
                player.TakeDamage(damage);
        }
        else
        {
            endPoint = ray.origin + ray.direction * range;
        }

        // BULLET TRACE
        if (bulletLine != null)
        {
            bulletLine.enabled = true;
            bulletLine.SetPosition(0, firePoint.position);
            bulletLine.SetPosition(1, endPoint);
            Invoke(nameof(HideLine), 0.05f);
        }
    }

    void HideLine()
    {
        if (bulletLine != null)
            bulletLine.enabled = false;
    }
}

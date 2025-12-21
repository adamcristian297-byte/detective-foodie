using UnityEngine;
using System.Collections;

public class WeaponShoot1 : MonoBehaviour
{
    [Header("References")]
    public Camera shootCamera;             // Player camera
    public Transform muzzlePoint;          // Gun muzzle position
    public ParticleSystem muzzleFlash;     // Optional muzzle flash
    public LineRenderer bulletTrace;       // Optional bullet trace

    [Header("Settings")]
    public float range = 100f;             // Shooting range
    public int damage = 50;                // Damage per shot
    public float fireRate = 0.5f;          // Shots per second
    public float traceDuration = 0.05f;    // Duration of bullet trace

    private float nextTimeToFire = 0f;

    void Start()
    {
        // Auto-assign main camera if none assigned
        if (shootCamera == null)
            shootCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            Shoot();
            nextTimeToFire = Time.time + fireRate;
        }
    }

    public void Shoot()
    {
        if (muzzleFlash != null)
            muzzleFlash.Play();

        Ray ray = new Ray(shootCamera.transform.position, shootCamera.transform.forward);
        RaycastHit hit;
        Vector3 targetPoint = ray.origin + ray.direction * range;

        if (Physics.Raycast(ray, out hit, range))
        {
            targetPoint = hit.point;

            // Use GetComponentInParent to find EnemyHealth1
            EnemyHealth1 enemy = hit.collider.GetComponentInParent<EnemyHealth1>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Debug.Log("Hit " + enemy.name + " for " + damage + " damage.");
            }
        }

        // Bullet trace
        if (bulletTrace != null && muzzlePoint != null)
        {
            StartCoroutine(ShowBulletTrace(targetPoint));
        }
    }

    private IEnumerator ShowBulletTrace(Vector3 hitPoint)
    {
        bulletTrace.enabled = true;
        bulletTrace.SetPosition(0, muzzlePoint.position);
        bulletTrace.SetPosition(1, hitPoint);
        yield return new WaitForSeconds(traceDuration);
        bulletTrace.enabled = false;
    }
}

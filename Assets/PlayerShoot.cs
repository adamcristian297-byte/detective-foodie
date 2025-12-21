using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("Shooting")]
    public float damage = 25f;
    public float range = 100f;
    public float fireRate = 0.2f;

    [Header("References")]
    public Camera fpsCam;
    public Transform muzzlePoint;
    public ParticleSystem muzzleFlash;
    public LineRenderer tracer;

    private float nextFireTime = 0f;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        // 🔥 Muzzle flash
        if (muzzleFlash != null)
            muzzleFlash.Play();

        Ray ray = new Ray(fpsCam.transform.position, fpsCam.transform.forward);
        RaycastHit hit;

        Vector3 endPoint;

        if (Physics.Raycast(ray, out hit, range))
        {
            endPoint = hit.point;

            // 💥 Damage enemy
            EnemyHealth enemy = hit.collider.GetComponent<EnemyHealth>();
            if (enemy != null)
                enemy.TakeDamage((int)damage);
        }
        else
        {
            endPoint = fpsCam.transform.position + fpsCam.transform.forward * range;
        }

        // 🔫 Bullet trace
        StartCoroutine(ShowTracer(endPoint));
    }

    System.Collections.IEnumerator ShowTracer(Vector3 hitPoint)
    {
        tracer.enabled = true;
        tracer.SetPosition(0, muzzlePoint.position);
        tracer.SetPosition(1, hitPoint);

        yield return new WaitForSeconds(0.05f);

        tracer.enabled = false;
    }
}

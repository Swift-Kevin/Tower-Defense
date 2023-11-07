using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour
{
    // All variables and other assorted items to use during the script
    private Transform target;
    private Enemy targetEnemy;

    [Header("General")]
    public float range = 15f;

    // Bullet Information
    [Header("Use Bullets (default)")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    // Laser Specifications
    [Header("Use Laser")]
    public bool useLaser = false;
    public int damageOverTime = 25;
    public float slowPercent = 0.5f;

    // Render/Impacts information
    [Header("Impact Systems")]
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;

    [Header("Turret Shooting System")]
    public string enemyTag = "Enemy";
    public Transform partToRotate;
    public float turnSpeed = 10f;
    public Transform firePoint;

    // Repeats every 0.5 seconds to update the target
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    // Finds and updates the targeted enemy
    void UpdateTarget()
    {
        // Collects a list of enemies (with certain tags) and updates their location 
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        // Makes the shortestDistance the largest possible one so there are enemies at least in that range
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            // Sets the nearest enemy to the closest one
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        // Finds if the nearest enemy is not null and is within the the shortest distance
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }

    }

    // Every frame it will check to see if the target is equal to null
    // then if false, will see if the useLaser is checked, and then
    // will check to see if the lineRenderer is enabled (basically making sure a laser turret is in use)
    void Update()
    {
        if (target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    // Emits the impact effects on collision with the enemy target
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false;
                }
            }

            return;
        }

        // Runs the LockOnTarget Method
        LockOnTarget();

        // If the use laser is true then it will run the laser method
        if (useLaser)
        {
            Laser();
        }
        // otherwise it will do the fire rate of the turret at the rate inputted
        else
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }

    }

    // Locks the turret to look in the direciton of the enemy object
    void LockOnTarget()
    {
        // Creates a position to look at
        Vector3 dir = target.position - transform.position;
        // Rotates the turret in the direction of the enemy
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    // Method for the laser turret's laser
    void Laser()
    {
        // Makes sure that the enemy of type Enemey and then takes the damage per second
        targetEnemy.GetComponent<Enemy>().TakeDamage(damageOverTime * Time.deltaTime);
        // Alters the speed of the enemy so they are slowed
        targetEnemy.Slow(slowPercent);

        // If no line rendered then it enables it and does the impact effects
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }

        // Sets the line renderes position to that of the enemy and the firepoint
        // so it appears as it is shooting out of it and focusing on the enemy
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        // Rotates the impact effect by what direction it is from the turret
        Vector3 dir = firePoint.position - target.position;
        impactEffect.transform.position = target.position + dir.normalized;
        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }

    // Shooting method for MissleLauncher/BasicTurret
    void Shoot()
    {
        // Creates a game object of the bullet to move towards the target
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        // If the bullet does exist then it seeks the target out (follows it)
        if (bullet != null)
            bullet.Seek(target);
    }

    // In the scene view when inspecting a turret it will outline its radius for shooting
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

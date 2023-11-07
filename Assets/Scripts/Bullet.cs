using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Member fields for incoming/outgoing scripts
    private Transform target;

    [Header("Bullet Attributes")]
    public int damage = 50;
    public float speed = 60f;
    public float explosionRadius = 0f;
    public GameObject impactEffect;

    // Sets target to the one inputed in
    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        // Determines if there should be a bullet there or not and will kill it if there shouldnt be one.
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Gets the direction for the bullet to be heading
        Vector3 dir = target.position - transform.position;
        // Sets the distance it goes in a frame to the speed * the time actualized
        float distanceThisFrame = speed * Time.deltaTime;

        // Determines if the length to the target is within the range that it can reach in a frame
        // if it is then it decides it has hit its target
        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        // Moves the bullet across the environment at the distanceThisFrame in the world
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        // Sets the bullet to look at the target (useful for missles)
        transform.LookAt(target);
    }

    // Runs through and determins if the bullets impact should do certain things or not to the enemy
    void HitTarget()
    {
        // Creates an instance for the effect to be created at the point that the bullet hits the target
        GameObject effectInstance = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        // Destroys the effect after its been created (for 2 seconds)
        Destroy(effectInstance, 2f);

        // Explosion radius is dependant on if the explosion size is greater than 0 or not
        if (explosionRadius > 0)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }

        // Destroys the game object of the bullet
        Destroy(gameObject);
    }

    // Determines the effect of the explode function
    void Explode()
    {
        // Creates an array of all enemies in the area to be affected by the explosion (missle impact)
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            // Only damages enemies with the Enemy tag so no other tiles or structures are affected
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    // Damages the enemy passed through the field
    void Damage(Transform enemy)
    {
        // Gets the transform of the enemy and then the amount of health it has
        Enemy e = enemy.GetComponent<Enemy>();

        if (e != null)
        {
            // Gets the enemy to take a certain amount of damage that the bullet can damage
            e.TakeDamage(damage);
        }
    }

    // When in the scene, any selected bullet will show the range it can reach for (mainly for explosions)
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}

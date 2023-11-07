using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    // Starting member fields for stats, effects, and other stuff for the enemy
    [HideInInspector]
    public float speed;

    [Header("Starting Stats")]
    public float startSpeed = 10f;
    public float startHealth = 100;
    private float health;
    public int startWorth = 50;

    [Header("Effects")]
    public GameObject deathEffect;

    [Header("Unity Components")]
    public Image healthBar;

    private bool isDead = false;

    // Sets starting Speed/HP to a new stat so they can be updated and still reference
    // the starting value
    void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }

    // Takes damage from a bullet and subtracts it from the new hp variable
    // also updates the horizontal fill amount of the Sprite (White Dot)
    // so the HP will update to a correct amount (% of bar filled is hp/startHP)
    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;

        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    // Sets the speed of the enemy to a modified amount
    // after being affected by the laser turrets beam
    public void Slow(float percent)
    {
        speed = startSpeed * (1f - percent);
    }

    // Gives the player a certain amount of cash per enemy killed
    // also destroys the enemy gameObject
    void Die()
    {
        // Increases players money per kill
        PlayerStats.Money += startWorth;

        // Overrides the bool for the enemy isDead value so no other action can be done on
        // this enemy
        isDead = true;

        // Creates an effect for the enemy to die with (the enemy explodes)
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        // Decreases amount of enemies alive to update the counter so the game knows when
        // to go to the next round and when to not
        WaveSpawner.EnemiesAlive--;

        // Destroys the enemy object
        Destroy(gameObject);
    }

}

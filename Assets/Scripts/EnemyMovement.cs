using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    // Setting member fields for the following code
    private Transform target;
    // Wavepoint index is meant for how many waypoints there are to reach in the level
    private int wavepointIndex = 0;
    private Enemy enemy;

    // When the script runs for the first time this will be executed before the Update method is called
    void Start()
    {
        enemy = GetComponent<Enemy>();
        target = Waypoints.points[0];
    }

    // Gets called every frame to update what is happening to the script (Movement of the enemy)
    void Update()
    {
        // This is the movement for the enemy, as it will move across pointing at the target and follow the path
        // the enemies movement will follow waypoints designated in the scene (not visible in game)
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

        // This says if the enemy is close enough to the waypoint (a float of 0.2 units) then it will count it as reached
        // and go for the next waypoint.
        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }

        // Updates the speed in case the laser turret is no longer affecting the speed and will
        // return to its normal speed
        enemy.speed = enemy.startSpeed;
    }

    // Gets the next waypoint for the enemy to reach
    void GetNextWaypoint()
    {
        // Says if the amount of waypoints reached is greater or equal to the amount of waypoints existing
        // then it will end the path of the enemy and count it as completed
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }

        // Increases the amount of waypoints reached
        wavepointIndex++;
        // Sets the target to the next waypoint
        target = Waypoints.points[wavepointIndex];

    }

    // Method to end the path of the enemy
    void EndPath()
    {
        // This is for if the enemy reaches the end of the path and the player has not successfully
        // defeated the enemy, it will decrease 1 life off the player (want to make it so they do more hp the tougher/weaker they are)
        // also decreases the enemies that are alive so the game will know when it is done and destroys the enemy object so
        // nothing can be altered on it (such as defeating it and getting money, or taking away multiple lives)
        PlayerStats.Lives--;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}

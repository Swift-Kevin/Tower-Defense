using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    // Keeps track of how many enemies are alive
    public static int EnemiesAlive = 0;

    // Waves Array for every wave
    public Waves[] waves;

    // Where the enemies spawn at
    public Transform spawnPoint;

    // How long between waves to wait for the spawning of enemies to contineu
    public float timeBetweenWaves = 20f;
    private float countdown = 2f;

    // Countdown textbox to alter
    public Text waveCountdownText;
    public GameManager gameManager;
    private int waveIndex = 0;

    // Updates every frame to see if there are still enemies alive
    void Update()
    {
        // Checks if there are enemies still there
        if (EnemiesAlive > 0)
        {
            return;
        }

        // Checks if the waveIndex is equal or more than the length of the waves
        if (waveIndex >= waves.Length)
        {
            // If there are no more waves then they win the level
            gameManager.WinLevel();
            this.enabled = false;
        }
        
        // If the countdown reaches 0 or any number less than it, it will spawn the next wave
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }
        
        // Subtracts the time every second
        countdown -= Time.deltaTime;
        // Makes the countdown between 0 and infinity (only to display)
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        // Writes the countdown in the Minutes:Seconds.Miliseconds format
        waveCountdownText.text = string.Format("{0:00.0}", countdown);
    }

    // Spawns the next wave and adds to the round survived
    IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++;

        Waves wave = waves[waveIndex];

        EnemiesAlive = wave.count;

        // Spawns the enemies after a set amount of time
        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }
        waveIndex++;
    }

    // Creates an enemy on the passed through game object
    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }

}

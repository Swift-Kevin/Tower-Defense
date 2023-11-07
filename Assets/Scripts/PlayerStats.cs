using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Creates secondary variables for comparing to the original
    [Header("Starting Values")]
    public static int Money;
    public static int Lives;

    // The starting values for the money, lives and rounds
    public int startMoney = 400;
    public int startLives = 20;
    public static int Rounds;

    void Start()
    {
        // Sets all the values so they are in place when the game startsW
        Money = startMoney;
        Lives = startLives;
        Rounds = 0;
    }
}

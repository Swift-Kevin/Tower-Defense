using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Set UI objects so they can be inserted in the inspector and make a bool to see if the game is over or not
    public static bool GameIsOver = false;
    [Header("UI's")]
    public GameObject gameOverUI;
    public GameObject completeLevelUI;
    
    // Sets game over to false on start so it will not instantly end
    void Start()
    {
        GameIsOver = false;
    }

    // Update is called once per frame and sees if the game has ended by the bool or the amount of lives
    // the player has left
    void Update()
    {
        if (GameIsOver)
        { 
            return;
        }
        // Checks for players lives and ends if the lives are equal or below zero
        if (PlayerStats.Lives <= 0)
        {
            EndGame();
        }
    }

    // Endgame method acts as a way to end the game in coding aspects by setting the bool GameIsOver to true
    // and sets the game over ui to be visible, this ui is for if the player loses all their lives
    private void EndGame()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);
    }

    // WinLevel method shows the level complete UI if the player succesfully
    // defeats all the enemies while lives are still >=1
    public void WinLevel()
    {
        completeLevelUI.SetActive(true);
    }

}

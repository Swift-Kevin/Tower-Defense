using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // In the inspector, availability to select different menus is an option and will fade to them
    // Sticking to the main menu is the best option
    [Header("Main Menu Level Name")]
    public string menuSceneName = "MainMenu";
    // Place for the scene fader to be inserted
    [Header("Scene Fader")]
    public SceneFader sceneFader;
    
    // When the retry button is pressed this method will run fading the
    // scene to the same level of the same name and resets the scene
    public void Retry()
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    // When the menu button is pressed, the screen will fade to the main menu
    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
    }

}

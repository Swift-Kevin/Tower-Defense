using UnityEngine;

public class CompleteLevel : MonoBehaviour
{
    // Member fields for the script to use
    [Header("Level Completion Menus")]
    [Tooltip("Main menus name.")]
    public string menuSceneName = "MainMenu";
    [Tooltip("Level name for after completing")]
    public string nextLevel = "Level02";
    [Tooltip("Unlocks the next level in the ")]
    public int levelToUnlock = 2;

    [Header("Apply Scene Fader")]
    public SceneFader sceneFader;

    // Logs a Level Won statement in the debug to clarify when levels were won.
    // Also sets the levelreached and won when completed a level and fades to it
    public void Continue()
    {
        Debug.Log("LEVEL WON");
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
        sceneFader.FadeTo(nextLevel);
    }

    // Fades to the set scene
    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
    }  
}

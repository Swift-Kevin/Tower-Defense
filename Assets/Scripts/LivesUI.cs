using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    // Area to place the text box field for the level (display the lives the user has left)
    [Header("Text Components")]
    public Text livesText;

    // Updates the lives every frame to the current amount (and has a Heart Unicode character!)
    void Update()
    {
        livesText.text = PlayerStats.Lives + "\u2764";
    }
}

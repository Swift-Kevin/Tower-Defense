using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RoundsSurvived : MonoBehaviour
{
    // Text object that will display the rounds survived
    public Text roundsText;

    // When the text box is enabled it will animate the text as well
    void OnEnable()
    {
        StartCoroutine(AnimateText());
    }

    // This will count the number of rounds up starting at 0
    IEnumerator AnimateText()
    {
        roundsText.text = "0";
        int round = 0;

        // Waits 0.7 floated seconds until displaying the actual number increase
        // to provide time for the animation to happen
        yield return new WaitForSeconds(0.7f);

        // While the number of rounds # is less than the amount reached it will
        // add to the counter and then wait a bit to recheck if it has reached the
        // amount survived
        while (round < PlayerStats.Rounds)
        {
            round++;
            roundsText.text = round.ToString();

            yield return new WaitForSeconds(0.05f);
        }

    }
    
}

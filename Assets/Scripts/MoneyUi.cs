using UnityEngine;
using UnityEngine.UI;

public class MoneyUi : MonoBehaviour
{
    [Header("Balance Text Box")]
    public Text moneyText;

    // Updates the money amount every frame.
    void Update()
    {
        moneyText.text = "$" + PlayerStats.Money.ToString();
    }
}

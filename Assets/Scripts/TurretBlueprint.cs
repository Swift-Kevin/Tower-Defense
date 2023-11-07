using UnityEngine;

[System.Serializable]
public class TurretBlueprint
{
    // Gets the 
    public GameObject prefab;
    public int cost;

    // Makes a game object of the upgraded prefab
    public GameObject upgradedPrefab;
    public int upgradeCost;

    // Sell amount is half the cost amount
    public int GetSellAmount()
    {
        return cost / 2;
    }

}

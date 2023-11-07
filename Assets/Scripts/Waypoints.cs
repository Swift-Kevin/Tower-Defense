using UnityEngine;

public class Waypoints : MonoBehaviour
{
    // Makes an array of points
    public static Transform[] points;

    // Gets every waypoint in order and adds it to the list so 
    // enemies can follow the path listed before them
    void Awake()
    {
        points = new Transform[transform.childCount];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);

        }
    }
}

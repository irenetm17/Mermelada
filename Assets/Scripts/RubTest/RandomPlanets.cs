using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlanets : MonoBehaviour
{
    public Vector2 topLeft = new Vector2(-960f, 520f);
    public Vector2 bottomRight = new Vector2(960f, -520f);
    public GameObject[] planets = new GameObject[3];

    private void Awake()
    {
        Vector3 vector = new Vector3(Random.Range(topLeft.x, bottomRight.x), Random.Range(topLeft.y, bottomRight.y), 0f);
        planets[0].transform.position = vector;
        Vector3 vector2 = new Vector3(Random.Range(topLeft.x, bottomRight.x), Random.Range(topLeft.y, bottomRight.y), 0f);
        while (Vector2.Distance(vector2, planets[0].transform.position) < 2f)
            vector2 = new Vector3(Random.Range(topLeft.x, bottomRight.x), Random.Range(topLeft.y, bottomRight.y), 0f);
        planets[1].transform.position = vector2;
        Vector3 vector3 = new Vector3(Random.Range(topLeft.x, bottomRight.x), Random.Range(topLeft.y, bottomRight.y), 0f);
        while (Vector2.Distance(vector3, planets[0].transform.position) < 2f && Vector2.Distance(vector, planets[1].transform.position) < 2f)
            vector3 = new Vector3(Random.Range(topLeft.x, bottomRight.x), Random.Range(topLeft.y, bottomRight.y), 0f);
        planets[2].transform.position = vector3;
    }
}

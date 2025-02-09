using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class RandomMeteorite : MonoBehaviour
{
    private float maxTime;
    private float elapsedTime;
    public Transform[] planets;
    public GameObject[] meteorsPrefab;
    private List<GameObject> prefabList;
    public Vector2 bottomLeft;
    public Vector2 topRight;
    private float forceStrength = 2f;
    void Start()
    {
        maxTime = 0.1f;
        prefabList = new List<GameObject>();
    }

    private const float MIN_SPAWN_DISTANCE = 20;
    private const float MAX_SPAWN_DISTANCE = 40;
    void Update()
    {
        if (elapsedTime > maxTime && prefabList.Count < 100)
        {
            Vector3 temp = new Vector3(Random.Range(bottomLeft.x, topRight.x), Random.Range(bottomLeft.y, topRight.y), 0);

            while (CollidesWithOthers(temp))
                temp = new Vector3(Random.Range(bottomLeft.x, topRight.x), Random.Range(bottomLeft.y, topRight.y), 0);

            Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            if (Vector3.Distance(temp, playerPos) < MIN_SPAWN_DISTANCE || Vector3.Distance(temp, playerPos) > MAX_SPAWN_DISTANCE) return;

            GameObject instantiated = Instantiate(meteorsPrefab[Random.Range(0, meteorsPrefab.Length)], temp, Quaternion.identity, transform);
            prefabList.Add(instantiated);

            int rand = Random.Range(0, 6);
            if (rand == 1 || rand == 2) instantiated.transform.localScale = Random.Range(1, 3) * Vector3.one;
            else if(rand == 3) instantiated.transform.localScale = Random.Range(24, 32) * Vector3.one;
            else instantiated.transform.localScale = Random.Range(8, 16) * Vector3.one;

            Vector2 forceToAply = Random.insideUnitCircle.normalized;
            instantiated.GetComponent<Rigidbody2D>().AddForce(forceToAply * forceStrength, ForceMode2D.Impulse);
            
            elapsedTime = 0;
        }
        else
        {
            elapsedTime += Time.deltaTime;
        }
        CheckOutsiders();
        RepelatePlanets();
    }

    private bool CollidesWithOthers(Vector3 vector)
    {
        foreach(GameObject obj in prefabList)
        {
            if (Vector2.Distance(vector, obj.transform.position) < obj.transform.localScale.magnitude * 1.1f)
                return true;
        }
        for(int i = 0; i < planets.Length; i++)
        {
            if (Vector2.Distance(planets[i].position, vector) < PLANETS_REPULSION_RANGE * 2)
                return true;
        }
        return false;
    }
    //This function checks wheter the meteorites are outside the bounds of the map (then, if they are, they get destroyed)
    private void CheckOutsiders()
    {
        for (int i = prefabList.Count - 1; i >= 0; i--)
        {
            GameObject obj = prefabList[i];
            if (obj.transform.position.x > topRight.x || obj.transform.position.x < bottomLeft.x ||
                obj.transform.position.y > topRight.y || obj.transform.position.y < bottomLeft.y)
            {
                prefabList.RemoveAt(i);
                Destroy(obj);
            }
        }
    }

    private const float PLANETS_REPULSION_RANGE = 22;
    private void RepelatePlanets()
    {
        foreach (GameObject obj in prefabList)
        {
            for (int i = 0; i < planets.Length; i++)
            {
                float distance = Vector2.Distance(planets[i].position, obj.transform.position);
                if (distance < PLANETS_REPULSION_RANGE)
                {
                    Vector2 directionMtoF = (obj.transform.position - planets[i].position).normalized;
                    obj.GetComponent<Rigidbody2D>().AddForce(directionMtoF * (PLANETS_REPULSION_RANGE - distance), ForceMode2D.Impulse);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(bottomLeft, 1);
        Gizmos.DrawWireSphere(topRight, 1);
    }
}

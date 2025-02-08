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
    public GameObject[] planets = new GameObject[3];
    public GameObject prefabMeteor;
    private List<GameObject> prefabList;
    public Vector2 topleft;
    public Vector2 bottomright;
    private float forceStrength = 2f;
    void Start()
    {
        maxTime = 2f;
        prefabList = new List<GameObject>();
    }

    void Update()
    {
        if (elapsedTime > maxTime)
        {
            Vector3 temp = new Vector3(Random.Range(topleft.x, bottomright.x), Random.Range(topleft.y, bottomright.y), 0);
            while (CollidesWithOthers(temp) == true)
                temp = new Vector3(Random.Range(topleft.x, bottomright.x), Random.Range(topleft.y, bottomright.y), 0);
            GameObject instantiated = Instantiate(prefabMeteor, temp, Quaternion.identity);
            prefabList.Add(instantiated);
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
            if (Vector2.Distance(vector, obj.transform.position) < 2f)
                return true;
        }
        for(int i = 0; i < 3; i++)
        {
            if (Vector2.Distance(planets[i].transform.position, vector) < 1f)
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
            if (obj.transform.position.x > bottomright.x || obj.transform.position.x < topleft.x ||
                obj.transform.position.y > bottomright.y || obj.transform.position.y < topleft.y)
            {
                prefabList.RemoveAt(i);
                Destroy(obj);
            }
        }
    }

    private void RepelatePlanets()
    {
        foreach (GameObject obj in prefabList)
        {
            for (int i = 0; i < 3; i++)
            {
                float distance = Vector2.Distance(planets[i].transform.position, obj.transform.position);
                if (distance < 1.75f)
                {
                    Debug.Log("Apply force");
                    Vector2 directionMtoF = -(planets[i].transform.position - obj.transform.position).normalized;
                    obj.GetComponent<Rigidbody2D>().AddForce((directionMtoF * (forceStrength - 0.5f)) * (1.75f - distance), ForceMode2D.Impulse);
                }
            }
        }
    }
}

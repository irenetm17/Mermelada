using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlanets : MonoBehaviour
{
    public Vector2 topLeft = new Vector2(-960f, 520f);
    public Vector2 bottomRight = new Vector2(960f, -520f);

    private void Awake()
    {
        this.transform.GetChild(0).transform.position = new Vector3(Random.Range(topLeft.x, bottomRight.x), 
                                                                    Random.Range(topLeft.y, bottomRight.y),
                                                                    this.transform.GetChild(0).transform.position.z);
        this.transform.GetChild(1).transform.position = new Vector3(Random.Range(topLeft.x, bottomRight.x),
                                                            Random.Range(topLeft.y, bottomRight.y),
                                                            this.transform.GetChild(0).transform.position.z);
        this.transform.GetChild(2).transform.position = new Vector3(Random.Range(topLeft.x, bottomRight.x),
                                                            Random.Range(topLeft.y, bottomRight.y),
                                                            this.transform.GetChild(0).transform.position.z);
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ray : MonoBehaviour
{
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        transform.Translate(Time.deltaTime * 16 * transform.up);
        if (Vector3.Distance(transform.position, player.transform.position) > 32) Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) other.GetComponent<Health>().TakeDamage(10);

        Destroy(this.gameObject);
    }
}

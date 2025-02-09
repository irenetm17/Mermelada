using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ray : MonoBehaviour
{
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //transform.LookAt(Vector3.forward, player.transform.position);
    }

    private void Update()
    {
        transform.Translate(Time.deltaTime * 16 * transform.right, Space.World);
        if (Vector3.Distance(transform.position, player.transform.position) > 32) Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) other.gameObject.GetComponent<Health>().TakeDamage(10);

        Destroy(this.gameObject);
    }
}

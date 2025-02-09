using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject rayPrefab;
    [SerializeField] private Transform[] raysSpawners;

    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        Chasing();
        Attack();
    }

    private Vector3 momentum;
    private bool isRetreiving;
    private const float SPEED = 25;
    private const float MAX_SPEED = 6;
    private const float RETREIVE_DISTANCE = 7;
    private const float RETURN_DISTANCE = 13;
    void Chasing()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < RETREIVE_DISTANCE) isRetreiving = true;
        else if (Vector3.Distance(transform.position, player.transform.position) > RETURN_DISTANCE) isRetreiving = false;

        momentum += Time.deltaTime * SPEED * (transform.position - player.transform.position).normalized * (isRetreiving ? -1.5f : 1);

        momentum = Vector3.ClampMagnitude(momentum, MAX_SPEED);

        transform.LookAt(transform.position + Vector3.forward, player.transform.position - transform.position);
        transform.Translate(Time.deltaTime * momentum);
    }

    private float attackCooldown;
    void Attack()
    {
        if (isRetreiving || Vector3.Distance(transform.position, player.transform.position) > RETURN_DISTANCE) return;

        attackCooldown -= Time.deltaTime;
        if(attackCooldown < 0)
        {
            Instantiate(rayPrefab, raysSpawners[Random.Range(0, raysSpawners.Length)].position, transform.rotation * Quaternion.Euler(0, 0, 90));
            attackCooldown = Random.Range(0.5f, 3f);
        }
    }
}

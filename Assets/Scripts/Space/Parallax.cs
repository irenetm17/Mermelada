using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private int depth;

    private Transform player;
    private Material material;

    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        material.mainTextureOffset = player.position / depth;
        transform.position = player.position;
    }
}

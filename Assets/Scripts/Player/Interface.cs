using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Interface : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform ship;
    [Space]
    [SerializeField] private Animator animator;

    private const float APPEAR_DIST = 8;
    Coroutine currentEvent;
    private void Update()
    {
        animator.SetBool("IsOpen", Vector3.Distance(ship.position, player.position) < APPEAR_DIST);
    }
}

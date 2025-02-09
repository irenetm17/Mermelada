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
    [SerializeField] private RectTransform rect;
    [SerializeField] private Animator animator;
    [Space]
    [SerializeField] private GameObject[] buttons;

    private const float APPEAR_DIST = 8;
    Coroutine currentEvent;
    private void Update()
    {
        animator.SetBool("IsOpen", Vector3.Distance(ship.position, player.position) < APPEAR_DIST);

        foreach (GameObject button in buttons) button.SetActive(rect.sizeDelta.y > 390);
    }
}

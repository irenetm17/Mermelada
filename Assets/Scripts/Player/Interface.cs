using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Interface : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform ship;
    [Space]
    [SerializeField] private RectTransform rect;
    [SerializeField] private Animator animator;
    [Space]
    [SerializeField] private GameObject[] buttons;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private const float APPEAR_DIST = 8;
    Coroutine currentEvent;
    private void Update()
    {
        bool isNear = Vector3.Distance(ship.position, player.position) < APPEAR_DIST;
        animator.SetBool("IsOpen", isNear);

        if (isNear)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        foreach (GameObject button in buttons) button.SetActive(rect.sizeDelta.y > 390);
    }
}

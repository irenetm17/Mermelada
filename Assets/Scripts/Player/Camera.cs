using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Player player;

    private Vector3 followSpeed;
    private void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + 10 * Vector3Int.back + player.MovementDir * 2, ref followSpeed, 0.2f);
    }
}

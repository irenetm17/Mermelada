using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claw : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform wire;
    [SerializeField] private Transform claw;

    private void Update()
    {
        Controller();
    }

    private bool isDeployed;
    void Controller()
    {
        wire.gameObject.SetActive(isDeployed);

        if (isDeployed) { ClawController(); return; }
        if (Input.GetKeyDown(KeyCode.Space)) isDeployed = true;
    }

    private float deployDistance;
    private const float MAX_DEPLOY_DIST = 4;
    void ClawController()
    {

    }
}

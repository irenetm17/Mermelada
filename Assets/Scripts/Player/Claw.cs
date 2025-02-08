using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claw : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform wire;
    [SerializeField] private SpriteRenderer wireRenderer;
    [SerializeField] private Transform claw;
    [Space]
    [SerializeField] private Minigames minigamesController;
    [SerializeField] private World[] worlds;

    private void Update()
    {
        Controller();
    }

    [HideInInspector] public bool IsDeployed;
    void Controller()
    {
        wire.gameObject.SetActive(IsDeployed);
        wireRenderer.size = new Vector2(wireRenderer.size.x, deployDistance);
        claw.localPosition = (deployDistance - 0.05f) * Vector3.up;

        if (Minigames.IsPlayingMinigame) return;

        if (IsDeployed) return;
        if (Input.GetKeyDown(KeyCode.Space)) DeployClaw();
    }

    private float deployDistance;
    private float deploySpeed = 3;
    private const float MAX_DEPLOY_DIST = 0.6f;
    void DeployClaw()
    {
        IsDeployed = true;
        StartCoroutine(DeployClaw_EVENT());
    }
    IEnumerator DeployClaw_EVENT()
    {
        while(deployDistance < MAX_DEPLOY_DIST)
        {
            deployDistance += Time.deltaTime * deploySpeed;
            if (CheckForWorlds()) yield break;

            yield return null;
        }
        RetreiveClaw();
    }
    public void RetreiveClaw() { StartCoroutine(RetreiveClaw_EVENT()); }
    IEnumerator RetreiveClaw_EVENT()
    {
        while (deployDistance > 0)
        {
            deployDistance -= Time.deltaTime * deploySpeed * 3;
            yield return null;
        }
        IsDeployed = false;
    }

    bool CheckForWorlds()
    {
        foreach(World world in worlds)
        {
            if (Vector3.Distance(world.transform.position, claw.position) < 7)
            {
                minigamesController.StartMinigame(world.worldId);
                return true;
            }
        }

        return false;
    }
}

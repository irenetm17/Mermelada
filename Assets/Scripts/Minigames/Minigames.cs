using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigames : MonoBehaviour
{
    [SerializeField] private Claw claw;
    [Space]
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject[] minigames;

    public static bool IsPlayingMinigame;
    private int minigamePlaying;

    private void Awake()
    {
        foreach (GameObject minigame in minigames) minigame.SetActive(false);
        EndMinigame();
    }

    private void Update()
    {
        if (IsPlayingMinigame)
            if (Input.GetKeyDown(KeyCode.F)) EndMinigame();
    }

    public void StartMinigame(byte minigameId)
    {
        IsPlayingMinigame = true;
        minigamePlaying = minigameId;

        switch (minigameId)
        {
            case 0:
                minigames[0].GetComponent<Minigame_Ice>().StartGame();
                break;


            case 1:
                minigames[1].GetComponent<Minigame_Candy>().StartGame();
                break;


            case 2:
                minigames[2].GetComponent<Minigame_Ice>().StartGame();
                break;
        }

        animator.SetTrigger("Open");
        minigames[minigameId].SetActive(true);

    }
    public void EndMinigame() { StartCoroutine(EndMinigame_EVENT()); }
    IEnumerator EndMinigame_EVENT()
    {
        animator.SetTrigger("Close");

        yield return new WaitForSeconds(0.4f);

        claw.RetreiveClaw();
        minigames[minigamePlaying].SetActive(false);
        IsPlayingMinigame = false;
        minigamePlaying = -1;
    }
}

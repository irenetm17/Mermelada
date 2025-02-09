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
    public static int MinigameID;
    private int minigamePlaying;

    private void Awake()
    {
        this.gameObject.SetActive(false);

        foreach (GameObject minigame in minigames) minigame.SetActive(false);
        minigames[2].SetActive(true);

        EndMinigame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) EndMinigame();
    }

    public void StartMinigame(byte minigameId)
    {
        this.gameObject.SetActive(true);

        IsPlayingMinigame = true;
        minigamePlaying = minigameId;
        MinigameID = minigamePlaying;

        switch (minigameId)
        {
            case 0:
                minigames[0].GetComponent<Minigame_Ice>().StartGame();
                break;


            case 1:
                minigames[1].GetComponent<Minigame_Candy>().StartGame();
                break;
        }

        if (minigameId == 2)
        {
            minigames[2].GetComponent<Minigame_Plant>().StartGame();
            IsPlayingMinigame = false;
            minigamePlaying = -1;
            MinigameID = -1;
            this.gameObject.SetActive(false);
            return;
        }

        animator.SetTrigger("Open");
        minigames[minigameId].SetActive(true);

    }
    public void EndMinigame()
    {
        if (!IsPlayingMinigame) return;
        StartCoroutine(EndMinigame_EVENT());
    }
    IEnumerator EndMinigame_EVENT()
    {
        animator.SetTrigger("Close");

        yield return new WaitForSeconds(0.4f);

        claw.RetreiveClaw();
        minigames[minigamePlaying].SetActive(false);
        IsPlayingMinigame = false;
        minigamePlaying = -1;
        MinigameID = -1;
    }
}

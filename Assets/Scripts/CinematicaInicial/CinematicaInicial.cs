using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CinematicaInicial : MonoBehaviour
{
    private SpriteRenderer sp;
    private float lastShoot;
    private int index;
    [SerializeField] private GameObject negro;
    [SerializeField] private GameObject bichillo;
    public DialogueManager dialogo;

    public string[] str = { "*Somewhere lost in space*","*An alien race called the Lenux lived peacefully till...*","*Some invaders dicovered the great potential for exploitation*", "Oh,what is that...", "Is that the last Lneux alive!?", "I should take care of it","I can't let that species of cute aliens die"};
    // Update is called once per frame
    void Update()
    {
        
    }

    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        lastShoot = Time.time;
        StartCinematic();
    }

    public void StartCinematic()
    {
        index = 0;
        StartCoroutine(DesapareceBlanco());
    }

    IEnumerator DesapareceBlanco()
    {
        SpriteRenderer im = negro.GetComponent<SpriteRenderer>();
        while (im.color.a > 0.0f)
        {
            im.color = new Color(1.0f, 1.0f, 1.0f, im.color.a - 0.07f);
            yield return null;
        }
        negro.SetActive(false);
        bichillo.SetActive(true);
        dialogo.StartDialogue(str);
    }
}

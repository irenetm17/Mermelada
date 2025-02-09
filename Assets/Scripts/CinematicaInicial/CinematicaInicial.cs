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

    public string[] str = { "HOLAAA", "ADIOOS"};
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

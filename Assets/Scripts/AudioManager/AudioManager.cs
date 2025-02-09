using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager _instance;
    public List<AudioSource> list = new List<AudioSource>();
    public AudioSource _here;
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(_instance.gameObject);
        DontDestroyOnLoad(_instance.gameObject);
        SceneManager.sceneLoaded += LoadScene;
    }
    void Start()
    {
    }

    private void LoadScene(Scene arg0, LoadSceneMode arg1)
    {
        AudioSource source = list.Find(sour => sour.name.Equals(arg0.name));
        if (source != null)
        {
            if (_here != null)
                _here.Stop();
            source.Play();
            _here = source;
        }
    }

}

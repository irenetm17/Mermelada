using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public string nameScene;
    public void OnDisable()
    {
        SceneManager.LoadScene(nameScene);
    }
}

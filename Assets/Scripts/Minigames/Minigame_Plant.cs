using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minigame_Plant : MonoBehaviour
{
    [SerializeField] private RectTransform wire;
    [Space]
    [SerializeField] private RectTransform ring;
    [SerializeField] private RectTransform handle;
    [Space]
    [SerializeField] private Image fruitImage;
    [SerializeField] private Sprite[] fruitSprites;

    public void StartGame()
    {

    }

    private void Update()
    {
        Ring();
    }

    private byte wins;
    private float speed;
    private Vector3 direction;
    void Ring()
    {
        handle.Rotate(direction * speed * Time.deltaTime);
    }
}

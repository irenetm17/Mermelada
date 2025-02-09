using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minigame_Plant : MonoBehaviour
{
    [SerializeField] private CanvasGroup alpha;
    [Space(20)]
    [SerializeField] private RectTransform wire;
    [Space]
    [SerializeField] private RectTransform ring;
    [SerializeField] private RectTransform handle;
    [Space]
    [SerializeField] private Image fruitImage;
    [SerializeField] private Sprite[] fruitSprites;

    public void StartGame()
    {
        alphaTime = 2;
    }

    private float alphaTime;
    private void Update()
    {
        alphaTime -= Time.deltaTime;
        alpha.alpha = alphaTime;
    }

    private byte wins;
    private float speed;
    private Vector3 direction;
    void Ring()
    {
        handle.Rotate(direction * speed * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Storage : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private TMP_Text countText;

    public byte currentContent;
    public int currentCount;

    public void AddContent(byte contentId, int count)
    {
        if (currentContent != contentId) currentCount = 0;
        currentContent = contentId;
        currentCount += count;
    }
    public void Use(int min, int max)
    {
        currentCount -= Random.Range(min, max);
        if (currentCount < 0) currentCount = 0;
    }

    private void Update()
    {
        icon.sprite = sprites[currentContent];
        countText.SetText(currentCount.ToString());
    }
}

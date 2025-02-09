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

    private byte currentContent;
    private int currentCount;

    public void AddContent(byte contentId, int count)
    {
        if (currentContent != contentId) currentCount = 0;
        currentContent = contentId;
        currentCount += count;
    }

    private void Update()
    {
        icon.sprite = sprites[currentContent];
        countText.SetText(currentCount.ToString());
    }
}

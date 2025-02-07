using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Interface : MonoBehaviour, IPointerClickHandler
{
    private bool isOpen;
    private bool isMoving;
    RectTransform rectTransform;
    //OffsetMin is Vector2(left, bottom)
    //OffsetMax is Vector2(right, top)

    public void OnPointerClick(PointerEventData eventData)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null)
            return;
        if (isOpen)
        {
            if (isMoving == false)
                StartCoroutine(HideInterface());
        }
        else
        {
            if (isMoving == false)
                StartCoroutine(ShowInterface());
        }
    }
    void Start()
    {
        isOpen = false;
        isMoving = false;
        rectTransform = GetComponent<RectTransform>();
    }

    public IEnumerator HideInterface()
    {
        float elapsedTime = 0;
        isMoving = true;

        while (elapsedTime < 1)
        {
            rectTransform.offsetMin = Vector2.Lerp(rectTransform.offsetMin, new Vector2(1453.362f, 50f), elapsedTime / 1);
            rectTransform.offsetMax = Vector2.Lerp(rectTransform.offsetMax, new Vector2(-50f, -809.8287f), elapsedTime / 1);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.offsetMin = new Vector2(1453.362f, 50f);
        rectTransform.offsetMax = new Vector2(-50f, -809.8287f);
        isMoving = false;
        isOpen = false;
    }

    public IEnumerator ShowInterface()
    {
        float elapsedTime = 0;
        isMoving = true;

        while (elapsedTime < 1)
        {
            rectTransform.offsetMin = Vector2.Lerp(rectTransform.offsetMin, new Vector2(882, rectTransform.offsetMin.y), elapsedTime / 1);
            rectTransform.offsetMax = Vector2.Lerp(rectTransform.offsetMax, new Vector2(rectTransform.offsetMax.x, -55), elapsedTime / 1);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.offsetMin = new Vector2(882, rectTransform.offsetMin.y);
        rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, -55);
        isMoving = false;
        isOpen = true;
    }
}

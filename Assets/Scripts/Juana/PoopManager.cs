using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoopManager : MonoBehaviour
{
    public void ToShow()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void ToHide()
    {
        this.gameObject.GetComponent<Image>().enabled = false;
    }
}

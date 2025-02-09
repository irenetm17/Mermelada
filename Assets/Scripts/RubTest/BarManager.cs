using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarManager : MonoBehaviour
{

    private int love;
    private int clean;
    private int satiety;

    public void Start()
    {
        love = 5;
        clean = 5;
        satiety = 5;
    }
    public void AddLove()
    {
        if (love != 5)
        {
            this.transform.GetChild(1).GetChild(love).gameObject.SetActive(true);
            love++;
        }
    }

    public void RemoveLove()
    {
        if (love != 0)
        { 
            this.transform.GetChild(1).GetChild(love - 1).gameObject.SetActive(false);
            love--;
        }
    }

    public void AddSatiety()
    {
        if (satiety != 5)
        {
            this.transform.GetChild(2).GetChild(satiety).gameObject.SetActive(true);
            satiety++;
        }
    }

    public void RemoveSatiety()
    {
        if (satiety != 0)
        {
            this.transform.GetChild(2).GetChild(satiety - 1).gameObject.SetActive(false);
            satiety--;
        }
    }

    public void AddClean()
    {
        if (clean != 5)
        {
            this.transform.GetChild(0).GetChild(clean).gameObject.SetActive(true);
            clean++;
        }
    }

    public void RemoveClean()
    {
        if (clean != 0)
        {
            this.transform.GetChild(0).GetChild(clean - 1).gameObject.SetActive(false);
            clean--;
        }
    }
}
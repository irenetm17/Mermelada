using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarManager : MonoBehaviour
{

    private int love;
    private int dirty;
    private int hungry;

    public void Start()
    {
        love = 0;
        dirty = 0;
        hungry = 0;
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

    public void AddHungry()
    {
        if (hungry != 5)
        {
            this.transform.GetChild(2).GetChild(hungry).gameObject.SetActive(true);
            hungry++;
        }
    }

    public void RemoveHungry()
    {
        if (hungry != 0)
        {
            this.transform.GetChild(2).GetChild(hungry - 1).gameObject.SetActive(false);
            hungry--;
        }
    }

    public void AddDirty()
    {
        if (dirty != 5)
        {
            this.transform.GetChild(0).GetChild(dirty).gameObject.SetActive(true);
            dirty++;
        }
    }

    public void RemoveDirty()
    {
        if (dirty != 0)
        {
            this.transform.GetChild(0).GetChild(dirty - 1).gameObject.SetActive(false);
            dirty--;
        }
    }
}
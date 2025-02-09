using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PetManager : MonoBehaviour
{
    public AnimatorController[] _animations = new AnimatorController[4];
    private Animator _animator;
    public BarManager barManager;
    public float time;
    public int index;

    private readonly float loveRemoveTime = 5f;
    private readonly float satietyRemoveTime = 2f;
    private readonly float cleanRemoveTime = 3f;

    private float loveTime;
    private float satietyTime;
    private float cleanTime;

    private int love;
    private int satiety;
    private int clean;

    void Start()
    {
        time = 0;
        _animator = GetComponent<Animator>();
        _animator.runtimeAnimatorController = _animations[0];
        loveTime = 0;
        satietyTime = 0;
        cleanTime = 0;

        love = 5;
        clean = 5;
        satiety = 5;
    }

    void Update()
    {
        if (time >= 60f)
        {
            index++;
            _animator.runtimeAnimatorController = _animations[index];
            time = 0;
        }
        else
            time += Time.deltaTime;

        if (loveTime > loveRemoveTime)
        {
            LoveAdjust();
            loveTime -= loveRemoveTime;
        }
        else
            loveTime += Time.deltaTime;

        if (satietyTime > satietyRemoveTime)
        {
            SatietyAdjust();
            satietyTime -= satietyRemoveTime;
        }
        else
            satietyTime += Time.deltaTime;

        if (cleanTime > cleanRemoveTime)
        {
            Poop();
            CleanAdjust();
            cleanTime -= cleanRemoveTime;
        }
        else
            cleanTime += Time.deltaTime;
    }

    public void Pet()
    {
        _animator.SetTrigger("Pet");
    }

    public void Feed()
    {
        _animator.SetTrigger("Feed");
    }

    public void Poop()
    {
        _animator.SetTrigger("Poop");
    }

    public void Die()
    {
        _animator.SetBool("isAlive", false);
    }

    public void LoveAdjust()
    {
        if (love != 0)
        {
            barManager.RemoveLove();
            love--;
        }
        else
            Debug.Log("Pierdes porque: amor 0");//PERDER EL JUEGO PORQUE KAPUT
    }
    public void SatietyAdjust()
    {
        if (satiety != 0)
        {
            barManager.RemoveSatiety();
            satiety--;
        }
        else
            Debug.Log("Pierdes porque: hambre 5");//PERDER EL JUEGO PORQUE KAPUT
    }
    public void CleanAdjust()
    {
        if (clean != 0)
        {
            barManager.RemoveClean();
            gameObject.transform.GetChild(5 - clean).gameObject.SetActive(true);
            clean--;
        }
        else
            Debug.Log("Pierdes porque: limpio 0");//PERDER EL JUEGO PORQUE KAPUT
    }

}

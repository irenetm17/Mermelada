using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PetManager : MonoBehaviour
{
    public AnimatorController[] _animations = new AnimatorController[4];
    private Animator _animator;
    public BarManager barManager;
    public float time;
    public int index;

    private readonly float loveRemoveTime = 36f;
    private readonly float satietyRemoveTime = 15f;
    private readonly float cleanRemoveTime = 29f;

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
            Pet();
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
            SceneManager.LoadScene("GameLost");
    }
    public void SatietyAdjust()
    {
        if (satiety != 0)
        {
            barManager.RemoveSatiety();
            satiety--;
        }
        else
            SceneManager.LoadScene("GameLost");
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
            SceneManager.LoadScene("GameLost");
    }

    public void AddLove()
    {
        if (love != 5)
        {
            love++;
            Pet();
            barManager.AddLove();
        }
    }

    public void AddSatiety()
    {
        if (satiety != 5)
        {
            satiety++;
            Feed();
            barManager.AddSatiety();
        }
    }

    public void AddClean()
    {
        if (clean != 5)
        {
            clean++;
            this.transform.GetChild(5 - clean).gameObject.SetActive(false);
            barManager.AddClean();
        }
    }
}

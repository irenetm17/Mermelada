using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PetManager : MonoBehaviour
{
    public Storage storage;
    public Button foodButton;
    public Button cleanButton;
    public AnimatorController[] _animations = new AnimatorController[4];
    public Animator _animator;
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
        _animator.runtimeAnimatorController = _animations[0];
        loveTime = 0;
        satietyTime = 0;
        cleanTime = 0;

        love = 5;
        clean = 5;
        satiety = 5;
    }

    public IEnumerator MuertePeroBuena()
    {
        _animator.SetBool("isAlive", false);
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("CinematicaFinal");
    }
    void Update()
    {
        cleanButton.interactable = storage.currentContent == 0 && storage.currentCount != 0;
        foodButton.interactable = storage.currentContent == 1 && storage.currentCount != 0;

        if (time >= 60f)
        {
            index++;
            AudioManager._instance.PlayOne("Crecer");
            if (index == 4)
                StartCoroutine(MuertePeroBuena());
            else
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
        {
            AudioManager._instance.PlayOne("Muerte");
            SceneManager.LoadScene("GameLost");
        }
    }
    public void SatietyAdjust()
    {
        if (satiety != 0)
        {
            barManager.RemoveSatiety();
            satiety--;
        }
        else
        {
            AudioManager._instance.PlayOne("Muerte");
            SceneManager.LoadScene("GameLost");
        }
    }
    public void CleanAdjust()
    {
        if (clean != 0)
        {
            barManager.RemoveClean();
            _animator.transform.GetChild(5 - clean).gameObject.SetActive(true);
            clean--;
        }
        else
        {
            AudioManager._instance.PlayOne("Muerte");
            SceneManager.LoadScene("GameLost");
        }
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
            AudioManager._instance.PlayOne("Comer");
            storage.Use(2, 5);
        }
    }

    public void AddClean()
    {
        if (clean != 5)
        {
            clean++;
            _animator.transform.GetChild(5 - clean).gameObject.SetActive(false);
            barManager.AddClean();
            AudioManager._instance.PlayOne("Limpiar");
            storage.Use(1, 6);
        }
    }
}

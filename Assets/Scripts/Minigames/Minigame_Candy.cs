using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minigame_Candy : MonoBehaviour
{
    [SerializeField] private RectTransform reticle;
    [SerializeField] private Animator reticleAnimator;

    [System.Serializable]
    public class Candy
    {
        public RectTransform transform;
        public Image image;
        public float movementCooldown;
        public float speed;
        public Vector2 direction;
        public bool isDead;
    }
    [System.Serializable]
    public class LongCandy : Candy
    {
        public Vector2 gravityForce;
        public Sprite[] sprites;
    }
    [SerializeField] private LongCandy[] longCandies;


    private const float FLOOR_HEIGHT = -230;


    public void StartGame()
    {
        foreach(LongCandy candy in longCandies)
        {
            candy.direction = Random.Range(0, 2) == 1 ? Vector2.left : Vector2.right;
            candy.movementCooldown = Random.Range(1f, 4f);
            candy.isDead = false;
        }
    }

    private void Update()
    {
        Reticle();
        CandiesController();
    }

    private float shootCooldown;
    private const float SHOOT_COOLDOWN = 0.5f;
    void Reticle()
    {
        shootCooldown -= Time.deltaTime;

        if (shootCooldown > 0) return;

        reticle.position = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            shootCooldown = SHOOT_COOLDOWN;
            Candy candy = Shoot();

            if (candy != null) { candy.isDead = true; reticleAnimator.SetTrigger("Hit"); } else reticleAnimator.SetTrigger("Miss");
        }

    }
    private const float RETICLE_RANGE = 50;
    Candy Shoot()
    {
        foreach (LongCandy candy in longCandies)
            if (Vector2.Distance(candy.transform.localPosition, reticle.localPosition) < RETICLE_RANGE)
                if(!candy.isDead) return candy;

        return null;
    }

    private const float MARGIN = 660;
    private readonly Vector2Int LONG_CANDY_JUMP_FORCE = new Vector2Int(100, 700);
    private readonly Vector2Int LONG_CANDY_SPEED = new Vector2Int(100, 700);
    void CandiesController()
    {
        foreach(LongCandy candy in longCandies)
        {
            candy.gravityForce.y -= Time.deltaTime * 900;

            candy.transform.Translate(Time.deltaTime * candy.gravityForce);

            if (candy.transform.localPosition.y <= FLOOR_HEIGHT)
            {
                candy.gravityForce = Vector2.zero;
                candy.transform.localPosition = new Vector2(candy.transform.localPosition.x, FLOOR_HEIGHT);
                candy.image.sprite = candy.sprites[0];
            }
            else
            {
                candy.image.sprite = candy.sprites[candy.gravityForce.y > 0 ? 1 : 2];
                candy.transform.Translate(Time.deltaTime * candy.direction * candy.speed);
            }


            if (candy.transform.localPosition.x > MARGIN) candy.transform.localPosition = new Vector2(-MARGIN, candy.transform.localPosition.y);
            else if (candy.transform.localPosition.x < -MARGIN) candy.transform.localPosition = new Vector2(MARGIN, candy.transform.localPosition.y);
            if(candy.isDead) candy.image.sprite = candy.sprites[3];

            if (candy.movementCooldown > 0)
            {
                candy.movementCooldown -= Time.deltaTime;
                continue;
            }
            if (candy.transform.localPosition.y > FLOOR_HEIGHT) continue;
            if (candy.isDead) { continue; }

            candy.gravityForce = Random.Range(LONG_CANDY_JUMP_FORCE.x, LONG_CANDY_JUMP_FORCE.y) * Vector2.up;
            candy.movementCooldown = Random.Range(0f, 4f);
            candy.speed = Random.Range(LONG_CANDY_SPEED.x, LONG_CANDY_SPEED.y);

            int rand = Random.Range(0, 7);
            if (rand == 1) candy.direction = Vector2.left; else if (rand == 2) candy.direction = Vector2.right;
        }
    }
}

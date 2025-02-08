using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minigame_Ice : MonoBehaviour
{
    [SerializeField] private RectTransform claw;
    [SerializeField] private Image clawImage;
    [SerializeField] private Sprite[] clawSprites;
    [Space]
    [SerializeField] private RectTransform block;

    public void StartGame()
    {
        claw.localPosition = new Vector2(0, 400);
        clawImage.sprite = clawSprites[0];
        block.localPosition = new Vector2(MAX_BLOCK_DIST, FLOOR_HEIGHT);
        claw.sizeDelta = new Vector2(claw.sizeDelta.x, MIN_CLAW_HEIGHT);
        isClawDeployed = false;
        isDown = false;
        hasCatchedBlock = false;
    }

    private void Update()
    {
        if (!isClawDeployed) ClawController();
        IceBlock();
    }

    private bool isClawDeployed;
    private bool isDown;
    private const float MARGIN = 480;
    private const float CLAW_SPEED = 400;
    void ClawController()
    {
        if (claw.localPosition.x > -MARGIN)
            if (Input.GetKey(KeyCode.A)) claw.Translate(Time.deltaTime * CLAW_SPEED * Vector2.right);

        if (claw.localPosition.x < MARGIN)
            if (Input.GetKey(KeyCode.D)) claw.Translate(Time.deltaTime * CLAW_SPEED * Vector2.left);

        if (Input.GetKeyDown(KeyCode.Space)) DeployClaw();
    }

    private const float MIN_CLAW_HEIGHT = 100;
    private const float MAX_CLAW_HEIGHT = 500;
    private const float DEPLOY_SPEED = 490;
    void DeployClaw()
    {
        isClawDeployed = true;
        StartCoroutine(DeployClaw_EVENT());
    }
    IEnumerator DeployClaw_EVENT()
    {
        clawImage.sprite = clawSprites[1];

        while(claw.sizeDelta.y < MAX_CLAW_HEIGHT)
        {
            claw.sizeDelta = new Vector2(claw.sizeDelta.x, claw.sizeDelta.y + Time.deltaTime * DEPLOY_SPEED);
            yield return null;
        }

        yield return new WaitForSeconds(1);

        while (claw.sizeDelta.y > MIN_CLAW_HEIGHT)
        {
            claw.sizeDelta = new Vector2(claw.sizeDelta.x, claw.sizeDelta.y - Time.deltaTime * DEPLOY_SPEED);
            yield return null;
        }

        clawImage.sprite = clawSprites[0];
        isClawDeployed = false;

        if (hasCatchedBlock)
        {
            block.localPosition = new Vector2(MAX_BLOCK_DIST, FLOOR_HEIGHT);
            hasCatchedBlock = false;

            //  todo:   add storage points
        }
    }

    private bool hasCatchedBlock;
    private float blockSpeed = 1;
    private const float FLOOR_HEIGHT = -180;
    private const float MAX_BLOCK_DIST = 680;
    void IceBlock()
    {
        if (!hasCatchedBlock) block.Translate(Time.deltaTime * blockSpeed * Vector2.right); else block.localPosition = claw.localPosition + (claw.sizeDelta.y + 160) * Vector3.down;
        if (block.localPosition.x > MAX_BLOCK_DIST) RespawnIceBlock();

        isDown = claw.sizeDelta.y > MAX_CLAW_HEIGHT - 40 && claw.sizeDelta.y < MAX_CLAW_HEIGHT;
        if (isDown)
            if(Mathf.Abs(claw.localPosition.x - block.localPosition.x) < 80)
            {
                hasCatchedBlock = true;
                clawImage.sprite = clawSprites[0];
            }
    }
    void RespawnIceBlock()
    {
        block.localPosition = new Vector2(-MAX_BLOCK_DIST * Random.Range(1.0f, 1.5f), block.localPosition.y);
        blockSpeed = Random.Range(200, 600);
    }
}

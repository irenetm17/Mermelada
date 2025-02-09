using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private SpriteRenderer slider;
    [SerializeField] private CanvasGroup alpha;

    private float health;
    private float hitCooldown;
    private float recoveryCooldown;
    private readonly Vector2 RECOVERY_COOLDOWN = new Vector2(0.1f, 1f);
    private void Awake()
    {
        health = 100;
    }

    private void LateUpdate()
    {
        slider.transform.position = transform.position + Vector3.down * 1.2f + Vector3.left * 1;
        slider.transform.rotation = Quaternion.Euler(0, 0, 0);

        hitCooldown += Time.deltaTime;

        if (health < 99)
            hitCooldown = 0;

        recoveryCooldown -= Time.deltaTime;
        if (recoveryCooldown < 0)
        {
            health += Random.Range(5, 15);
            recoveryCooldown = Random.Range(RECOVERY_COOLDOWN.x, RECOVERY_COOLDOWN.y);
        }

        health = Mathf.Clamp(health, 0, 100);

        alpha.alpha += hitCooldown < 3 ? 1 : -Time.deltaTime;

        slider.size = new Vector2(0.4f * (health / 100), 0.01f);
    }

    public void TakeDamage(float damage)
    {
        recoveryCooldown = 2;
        health -= damage;
    }
}

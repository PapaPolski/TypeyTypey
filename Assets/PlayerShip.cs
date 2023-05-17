using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShip : MonoBehaviour
{

    private static PlayerShip instance;

    public static PlayerShip Instance
    { get { return instance; } }


    public GameObject bulletPrefab;
    public float maxShieldAmount, currentShieldAmount, shieldDepletionRate;

    public float currentHealth, maxHealth;
    public SpriteRenderer shield;
    public Slider shieldSlider;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        maxShieldAmount = 100;
        maxHealth = 100;
        shieldSlider.maxValue = maxShieldAmount;

        currentShieldAmount = maxShieldAmount;
        shieldSlider.value = currentShieldAmount;
        currentHealth = maxHealth;

    }

    private void Update()
    {
        if (currentShieldAmount >= 0)
            currentShieldAmount -= Time.deltaTime * shieldDepletionRate;

        SetShieldAlphaValue(currentShieldAmount);
    }

    public void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
        bullet.GetComponent<Projectile>().isPlayerBullet = true;
    }

    public void ChargeShield()
    {
        currentShieldAmount += (maxShieldAmount * 0.25f);
        if (currentShieldAmount >= maxShieldAmount)
            currentShieldAmount = maxShieldAmount;
    }

    void SetShieldAlphaValue(float value)
    {
        value = Mathf.Clamp(value, 0f, 100f);
        float alpha = value / 100f;
        Color color = shield.color;
        color.a = alpha;
        shield.color = color;

        shieldSlider.value = currentShieldAmount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.gameObject.GetComponent<Projectile>().isPlayerBullet)
        {
            Destroy(collision.gameObject);
            if(currentShieldAmount > 0)
            {
                currentShieldAmount -= TakeDamage(collision.gameObject.GetComponent<Projectile>().damage);
            }
            else
            {
                currentHealth -= TakeDamage(collision.gameObject.GetComponent<Projectile>().damage);
            }    

            if(currentHealth <= 0)
            {
                GameOver();
            }
        }
    }

    float TakeDamage(float amount)
    {
        return amount;
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
    }
}

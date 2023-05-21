using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject bulletPrefab;
    float maxHealth, currentHealth;
    int scoreValue;
    public bool isEnemyUnlocked;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("FireBullet", 1f, 2f);
        maxHealth = 1;
        currentHealth = maxHealth;
    }


    void FireBullet()
    {
        if (GameManager.Instance.CurrentGameState == GameState.Playing)
        {
            Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
            bulletPrefab.GetComponent<Projectile>().isPlayerBullet = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Projectile>().isPlayerBullet)
        {
            Destroy(collision.gameObject);
            currentHealth -= GameManager.Instance.TakeDamage(collision.gameObject.GetComponent<Projectile>().damage);
            if (currentHealth <= 0)
                DestroyEnemy();
        }
    }

    void DestroyEnemy()
    {
        Debug.Log("Enemy defeated");
        GameManager.Instance.UpdateScore(scoreValue);
        Destroy(gameObject);
    }
}

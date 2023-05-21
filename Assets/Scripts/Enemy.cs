using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject bulletPrefab;
    float maxHealth, currentHealth, timer;
    int scoreValue;
    public bool isEnemyUnlocked;
    public float speed;
    Vector3 directionOfMovement;

    // Start is called before the first frame update
    void Start()
    {
        SetMovement();
        InvokeRepeating("FireBullet", 1f, 2f);
        maxHealth = 1;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentGameState == GameState.Playing)
        {
            transform.position += directionOfMovement * Time.deltaTime * speed;

            timer += Time.deltaTime;
            if (timer > 6f)
                Destroy(gameObject);
        }
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

    public void SetMovement()
    {
        if(transform.position.x == -3)
        {
            directionOfMovement = Vector2.down;
        }
        else if(transform.position.x > -3) 
        {
            directionOfMovement = Vector2.left;
        }
        else if(transform.position.x < -3)
        {
            directionOfMovement = Vector2.right;
        }
    }    
}

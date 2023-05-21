using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed, damage, timer;
    public bool isPlayerBullet;

    private void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (isPlayerBullet)
            spriteRenderer.color = Color.green;
        else
            spriteRenderer.color = Color.red;  
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.CurrentGameState == GameState.Playing)
        {
            if (isPlayerBullet)
                this.transform.position += Vector3.up * Time.deltaTime * speed;
            else
                this.transform.position -= Vector3.up * Time.deltaTime * speed;
        }
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentGameState == GameState.Playing)
        {
            timer += Time.deltaTime;
            if (timer > 3f)
                Destroy(gameObject);
        }
    }
}

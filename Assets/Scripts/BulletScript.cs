using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int bulletSpeed;
    [SerializeField] private int destroyTime;
    [HideInInspector] [SerializeField] private SpriteRenderer spriteRend;
    private Color32 playerBulletColor, enemyBulletColor;

    void Start()
    {
        playerBulletColor = new Color32(174, 255, 153, 255);
        enemyBulletColor = new Color32(255, 88, 81, 255);

    }

    void OnEnable()
    {
        Invoke("DisableBullet", destroyTime);
    }

    void DisableBullet()
    {
        gameObject.SetActive(false);
        BulletPoolScript.bulletPoolList.Add(gameObject);
    }

    void Update()
    {
        Movement();
        SetBulletColor();
    }

    void Movement()
    {
        transform.position += transform.up * Time.deltaTime * bulletSpeed;
    }

    void SetBulletColor()
    {
        if (this.gameObject.tag == "PlayerBullet" && spriteRend.color != playerBulletColor)
        {
            spriteRend.color = playerBulletColor;
        }
        if(this.gameObject.tag == "EnemyBullet" && spriteRend.color != enemyBulletColor)
        {
            spriteRend.color = enemyBulletColor;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject gameManager;
    [HideInInspector] [SerializeField] private GameObject upgradePrefab;

    [SerializeField] private int fullHealth;
    [SerializeField] private int shootingRate;
    [SerializeField] [Tooltip("-1 = left, 1 = right")]  private int moveDirection;
    [SerializeField] [Range(0, 100)] private int dropRate;
    [SerializeField] private int bulletSpeed;

    private int currentHealth;
    private BulletPoolScript bulletPoolScript;
    private GameController gameController;
    
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Manager");
        bulletPoolScript = gameManager.GetComponent<BulletPoolScript>();
        gameController = gameManager.GetComponent<GameController>();
    }

    void OnEnable()
    {
        currentHealth = fullHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shooting();
        Death();
    }

    void Movement()
    {
        float moveSpeed = transform.position.x + Time.deltaTime * gameController.enemyMovementSpeed * moveDirection;

        if (moveSpeed < -gameController.borderPosition || moveSpeed > gameController.borderPosition)
        {
            gameController.EnemyHitTheWall();
        }
    }

    void Shooting()
    {
        int randomShoot = Random.Range(0, 100000);
        if(randomShoot <= shootingRate)
        {
                bulletPoolScript.PoolSpawn(transform.position,transform.rotation ,bulletSpeed, "EnemyBullet");
        }

    }

    void Death()
    {
        if (currentHealth <= 0)
        {
            int randomDrop = Random.Range(0, 100);

            if (randomDrop <= dropRate)
            {
                Instantiate(upgradePrefab, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            currentHealth--;
            collision.gameObject.SetActive(false);
            BulletPoolScript.bulletPoolList.Add(collision.gameObject);
        }
    }

}
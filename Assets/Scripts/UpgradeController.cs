using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    [SerializeField] private Sprite[] upgradeSprites;
    [SerializeField] private int moveSpeed;
    [SerializeField] private int destroyTime;

    int randomUpgrade;
    void Start()
    {
        randomUpgrade = Random.Range(0, 3);

        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = upgradeSprites[randomUpgrade];

        Destroy(gameObject, destroyTime);
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - Time.deltaTime * moveSpeed);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
            
            switch(randomUpgrade)
            {
                case 0:
                    {
                        playerMovement.HealthAdded();
                        break;
                    }

                case 1:
                    {
                        playerMovement.weaponID = 1;
                        break;
                    }
                case 2:
                    {
                        playerMovement.weaponID = 2;
                        break;
                    }

            }
            Destroy(gameObject);
        }
    }
}

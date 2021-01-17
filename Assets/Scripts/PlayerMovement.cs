using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] [SerializeField] GameObject gameManager;
    private float movementValue;
    private BulletPoolScript bulletPoolScript;
    private GameController gameController;
    [SerializeField] [Range(0f, 100f)] private float movementSpeed;
    [SerializeField] private float doubleShotRangeBetween, tripleShotRadius;
    [SerializeField] private int bulletSpeed;
    public int weaponID; //0 - standard; 1 - double shot; 2 - triple shot

    void Start()
    {
        bulletPoolScript = gameManager.GetComponent<BulletPoolScript>();
        gameController = gameManager.GetComponent<GameController>();
    }

    void Update()
    {
      Movement();
    }

    public void Move(InputAction.CallbackContext value)
    {
        movementValue = value.ReadValue<float>();
    }

    void Movement()
    {
        float moveInput = transform.position.x + movementValue * Time.deltaTime * movementSpeed;
        if (moveInput < -gameController.borderPosition || moveInput > gameController.borderPosition) return;
        this.transform.position = new Vector2(moveInput, transform.position.y);
    }

    public void Shoot(InputAction.CallbackContext value)
    {
        float shootvalue = value.ReadValue<float>();

        if (shootvalue != 1) return;
        Weapons();
    }

    public void Weapons()
    {
        switch (weaponID)
        {
            case 0:
                    {
                    bulletPoolScript.PoolSpawn(transform.position, transform.rotation, bulletSpeed, "PlayerBullet");
                    break;
                }

            case 1:
                {
                    bulletPoolScript.PoolSpawn(new Vector2(transform.position.x + doubleShotRangeBetween, transform.position.y), transform.rotation, bulletSpeed, "PlayerBullet");
                    bulletPoolScript.PoolSpawn(new Vector2(transform.position.x - doubleShotRangeBetween, transform.position.y), transform.rotation, bulletSpeed, "PlayerBullet");
                    break;
                }

            case 2:
                {
                    bulletPoolScript.PoolSpawn(transform.position, transform.rotation, bulletSpeed, "PlayerBullet");
                    bulletPoolScript.PoolSpawn(transform.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + tripleShotRadius), bulletSpeed, "PlayerBullet");
                    bulletPoolScript.PoolSpawn(transform.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z - tripleShotRadius), bulletSpeed, "PlayerBullet");
                    break;
                }
        }
    }

    void HealthLost()
    {
        gameController.currentPlayerHealth--;
        gameController.RefreshPlayerHealth();
        weaponID = 0;
    }

    public void HealthAdded()
    {
        gameController.currentPlayerHealth++;
        gameController.RefreshPlayerHealth();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyBullet")
        {
            HealthLost();
            collision.gameObject.SetActive(false);
            BulletPoolScript.bulletPoolList.Add(collision.gameObject);
        }
        if(collision.tag == "Enemy")
        {
            gameController.GameOver();
        }
    }
}

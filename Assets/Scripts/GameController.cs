using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [HideInInspector] [SerializeField] private GameObject[] lifeBarImage;
    [SerializeField] private GameObject enemyPool;
    [SerializeField] private Text levelText;
    [SerializeField] private GameObject[] levels;
    [SerializeField] private int maxPlayerHealth;
    public int currentPlayerHealth;

    private int currentLvl;

    public float borderPosition;
    
    [Range(0f, 100f)] public float enemyMovementSpeed;
    public float enemyScrollingDownSpeed;
    [SerializeField] [Tooltip("-1 = left, 1 = right")] private int enemyMoveDirection;

    private Vector3 poolPosition;

    void Start()
    {
        currentLvl = 0;
        currentPlayerHealth = maxPlayerHealth;
        RefreshPlayerHealth();
        poolPosition = enemyPool.transform.position;
    }

    public void RefreshPlayerHealth()
    {
        for (int i = 0; i < 5; i++)
        {
            if (i < currentPlayerHealth)
            {
                lifeBarImage[i].SetActive(true);
            }
            else
            {
                lifeBarImage[i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        ChangeLvl();
        EnemyMoving();
        GameOver();
    }

    void EnemyMoving()
    {
        float moveSpeed = enemyPool.transform.position.x + Time.deltaTime * enemyMovementSpeed * enemyMoveDirection;
        enemyPool.transform.position = new Vector2(moveSpeed, enemyPool.transform.position.y);
    }

    public void EnemyHitTheWall()
    {
        enemyPool.transform.position = new Vector2(enemyPool.transform.position.x - enemyMoveDirection * 0.2f, enemyPool.transform.position.y - enemyScrollingDownSpeed);
        enemyMoveDirection = -enemyMoveDirection;
    }

    void ChangeLvl()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            if (currentLvl == i)
            {
                levels[i].SetActive(true);
            }
            else
            {
                levels[i].SetActive(false);
            }
        }
        int levelEnemies = levels[currentLvl].transform.childCount;
        if (levelEnemies == 0)
        {
            if (currentLvl == 2)
            {
                SceneManager.LoadScene("Menu");
            }
            currentLvl++;
            enemyPool.transform.position = poolPosition;
            levelText.text = "Level: " + (currentLvl + 1).ToString();
        }
    }

    public void GameOver()
    {
        if (currentPlayerHealth <= 0)
        {
            SceneManager.LoadScene("Menu");
        }
    }
}

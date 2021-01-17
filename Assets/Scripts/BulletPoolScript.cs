using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolScript : MonoBehaviour
{

    [HideInInspector][SerializeField] private GameObject bulletPrefab;
    [HideInInspector][SerializeField] private Transform bulletPool;
    [SerializeField] private int bulletPoolSize;

    public static List<GameObject> bulletPoolList;

    void Start()
    {
        bulletPoolList = new List<GameObject>();
        PoolCreate();
    }

    void PoolCreate()
    {
        for (int i = 0; i < bulletPoolSize; i++)
        {
            var bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.SetActive(false);
            bullet.transform.parent = bulletPool.transform;
            bulletPoolList.Add(bullet);
        }
    }

    public void PoolSpawn(Vector2 position, Quaternion rotation, int bulletSpeed, string bulletTag)
    {
        var bullet = bulletPoolList[0];

        BulletScript bulletScript = bullet.GetComponent<BulletScript>();
        bulletScript.bulletSpeed = bulletSpeed;
        bullet.tag = bulletTag;
        bullet.SetActive(true);
        bullet.transform.position = position;
        bullet.transform.rotation = rotation;
        bulletPoolList.Remove(bullet);
    }
}

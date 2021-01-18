using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : MonoBehaviour
{

    public GameObject bulletModel;
    public GameObject bullets;
    public GameObject scrapObject;
    public GameObject scrapHolder;

    private float health = 100f;
    
    private bool rotateRight;
    private float rotationSpeed = 7f;
    private float right;
    private float left;
    private bool shooting = false;

    void Start()
    {
        right = Random.Range(-4f, -8f);
        left = Random.Range(4f, 8f);
        int direction = Random.Range(0, 2);
        if (direction == 0)
        {
            rotateRight = true;
        }
        else
        {
            rotateRight = false;
        }
    }

    void Update()
    {
        
        if (transform.position.y > 13f)
        {
            transform.position += new Vector3(0, -17f * Time.deltaTime, 0);
        }
        else
        {
            if (!shooting)
            {
                StartCoroutine(Shoot());
                shooting = true;
            }
        }
        Rotate();
    }

    private void Rotate()
    {

        if (rotateRight)
        {
            transform.Rotate(new Vector3(-1f, 0, 0) * Time.deltaTime * rotationSpeed);
            if (-transform.rotation.eulerAngles.y <= right)
            {
                left = Random.Range(4f, 8f);
                rotateRight = false;
            }
        }
        else {
            transform.Rotate(new Vector3(1f, 0, 0) * Time.deltaTime * rotationSpeed);
            if (transform.rotation.eulerAngles.y >= left)
            {
                right = Random.Range(-4f, -8f);
                rotateRight = true;
            }
        }
    }

    IEnumerator Shoot()
    {
        while (this != null)
        {
            float randInterval = Random.Range(0.5f, 1.5f);
            yield return new WaitForSeconds(randInterval);
            Vector3 bulletSpawnPoint = transform.position;
            bulletSpawnPoint.y--;
            GameObject bullet = Instantiate(bulletModel, bulletSpawnPoint, Quaternion.identity);
            bullet.transform.SetParent(bullets.transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet"))
        {

            health -= other.GetComponent<Bullet>().damage;
            Destroy(other.gameObject);
            if (health <= 0f)
            {
                Vector3 scrapSpawnPoint = transform.position;
                GameObject scrap = Instantiate(scrapObject, scrapSpawnPoint, Quaternion.identity);
                scrap.transform.SetParent(scrapHolder.transform);
                Destroy(this.gameObject);
            }
        }
    }
}

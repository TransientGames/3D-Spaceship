using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{

    public GameObject shipModel;
    public GameObject bulletModel;
    public Camera playerCam;
    public Transform bulletSpawnPoint;

    private GameManager gameManager;
    private Transform bullets;

    private float shipAnimRotationSpeed = 10f;
    private bool rotateRight = true;
    private bool flying = false;
    private float shipMovementLerpSpeed = 0.2f;
    public float playerHealth = 100f;
    public float playerShields = 100f;
    private float right;
    private float left;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("Could not find GameManager in Player!");
        }
        bullets = GameObject.Find("Bullets").GetComponent<Transform>();
        if (bullets == null)
        {
            Debug.LogError("Could not find bullets transform in Player!");
        }
    }


    void Start()
    {
        flying = true;
        right = Random.Range(183f, 195f);
        left = Random.Range(165f, 177f);
        StartCoroutine(Shoot());
    }


    void Update()
    {
        Movement();
        Rotation();
        
    }

    void Movement()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = playerCam.ScreenToWorldPoint(touch.position);
            // offset for comfort on larger devices
            Vector3 viewportTouch = playerCam.WorldToViewportPoint(touchPosition);
            float multiplyer = 0.9f;
            float screenLimit = 1f;
            float xOffset = screenLimit - ((touchPosition.x * viewportTouch.x) * multiplyer);
            touchPosition.x -= xOffset;
            // lock the player to a point on screen
            touchPosition.y = 0;
            touchPosition.z = 0;
            transform.position = Vector3.Lerp(transform.position, touchPosition, shipMovementLerpSpeed);

            
            if (transform.position.x > screenLimit)
            {
                transform.position = new Vector3(screenLimit, 0, 0);
            }
            else if (transform.position.x < -screenLimit)
            {
                transform.position = new Vector3(-screenLimit, 0, 0);
            }
            
        }
    }

    private void Rotation()
    {
        if (rotateRight)
        {
            shipModel.transform.Rotate(new Vector3(-1f, 0, 0) * Time.deltaTime * 7f);
            if (shipModel.transform.rotation.eulerAngles.y >= right)
            {
                left = Random.Range(165f, 177f);
                rotateRight = false;
            }
        }
        else
        {
            shipModel.transform.Rotate(new Vector3(1f, 0, 0) * Time.deltaTime * 7f);
            if (shipModel.transform.rotation.eulerAngles.y <= left)
            {
                right = Random.Range(183f, 195f);
                rotateRight = true;
            }
        }

        Quaternion target = Quaternion.Euler(transform.rotation.x, transform.rotation.y, (-transform.position.x * 2f));
        transform.rotation = Quaternion.Lerp(transform.rotation, target, shipAnimRotationSpeed);
    }

    IEnumerator Shoot()
    {
        while (flying)
        {
            yield return new WaitForSeconds(0.3f);
            GameObject bullet = Instantiate(bulletModel, bulletSpawnPoint.position, transform.rotation);
            bullet.transform.SetParent(bullets);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            float damage = other.GetComponent<EnemyBullet>().damage;
            if (playerShields > 0)
            {
                if (damage > playerShields)
                {
                    playerShields = 0;
                    damage -= playerShields;
                }
                else
                {
                    playerShields -= damage;
                    damage = 0;
                }
            }
            if (damage > 0)
            {
                playerHealth -= damage;
            }
            if (playerHealth <= 0)
            {
                // playerdeath
            }
            gameManager.PlayerDamaged();
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Gear"))
        {
            Destroy(other.gameObject);
            // collect
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private float _speed = 13f;

    public float damage = 10f;

    void Start()
    {
        StartCoroutine(Die());
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }


    IEnumerator Die()
    {
        yield return new WaitForSeconds(3f);
        if (this.gameObject != null)
        {
            Destroy(this.gameObject);
        }
    }
}

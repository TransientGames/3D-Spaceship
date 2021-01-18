using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _speed = 15f;
    public float damage = 10f;


    void Start()
    {
        StartCoroutine(Die());
    }

    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime, Space.Self);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrap : MonoBehaviour
{

    private float speed = 70f;
    private float fallSpeed = 7f; 
    private float pivot;
    private float tilt;
    private float spin;

    void Start()
    {
        pivot = Random.Range(-1f, 1f);
        tilt = Random.Range(-1f, 1f);
        spin = Random.Range(-1f, 1f);
    }


    void Update()
    {
        transform.Rotate(new Vector3(tilt,pivot,spin) * Time.deltaTime * speed);
        transform.Translate(Vector3.down * Time.deltaTime * fallSpeed, Space.World);
        if (transform.position.y < -2f)
        {
            Destroy(this.gameObject);
        }
    }
}

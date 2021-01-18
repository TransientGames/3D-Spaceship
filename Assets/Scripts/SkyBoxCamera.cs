using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxCamera : MonoBehaviour
{
    private float cameraRotateSpeed = 30f;

    void Update()
    {
        transform.Rotate(Vector3.left, Time.deltaTime * cameraRotateSpeed);
    }
}

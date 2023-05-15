using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] public float projectileSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
    }
}

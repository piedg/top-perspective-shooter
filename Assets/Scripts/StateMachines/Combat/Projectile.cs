using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] int speed;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        ShootDirection();
    }

    void OnEnable()
    {
        StartCoroutine(DisableObject(5));
    }

    IEnumerator DisableObject(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

    void ShootDirection()
    {
        rb.velocity = transform.forward * speed;

    }

    public void OnHit()
    {
        gameObject.SetActive(false);
    }
}
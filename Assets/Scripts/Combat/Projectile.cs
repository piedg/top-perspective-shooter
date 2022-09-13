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

    private void FixedUpdate()
    {
        rb.velocity = transform.forward * speed;
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

    public void OnHit()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            //TODO ParticleEffects, Sounds
            gameObject.SetActive(false);
        }
        else if(other.gameObject.layer == LayerMask.NameToLayer("Environment"))
        {
            //TODO ParticleEffects, Sounds
            gameObject.SetActive(false);
        }
    }
}
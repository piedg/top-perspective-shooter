using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    private List<Collider> alreadyCollidedWith = new List<Collider>();
    [SerializeField] int damage;
    [SerializeField] GameObject MissileImpactFX;
    bool isImpact;

    private void OnTriggerStay(Collider other)
    {
        if (alreadyCollidedWith.Contains(other)) { return; }

        if (other.CompareTag("EnemyMissile"))
        {
            Destroy(other.gameObject);
            GameObject explosion = Instantiate(MissileImpactFX, transform.position, Quaternion.identity);
            explosion.GetComponent<ParticleSystem>().Play();
            isImpact = true;

        }
        if(isImpact)
        {
            if(other.CompareTag("Player"))
            {
                alreadyCollidedWith.Add(other);
                if (other.TryGetComponent<Health>(out Health health))
                {
                    health.DealDamage(damage);
                }
            }

        }
    }

  
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    private List<Collider> alreadyCollidedWith = new List<Collider>();
    [SerializeField] int damage;

    private void OnTriggerEnter(Collider other)
    {
        if (alreadyCollidedWith.Contains(other)) { return; }

        alreadyCollidedWith.Add(other);

        if (other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(damage);
        }
    }
}

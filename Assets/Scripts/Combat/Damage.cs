using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private Collider CharacterCollider;
    private int damage;
    private float knockback;
    private float radius;
    private bool isSuperAttack;

    private List<Collider> alreadyCollidedWith = new List<Collider>();
    private Vector3 startPosition = new Vector3(0f, 1f, 1f);

    private void OnEnable()
    {
        if(gameObject.TryGetComponent<SphereCollider>(out SphereCollider sphereCollider))
        sphereCollider.center = isSuperAttack ? Vector3.up : startPosition;

        sphereCollider.radius = radius;

        alreadyCollidedWith.Clear();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other == CharacterCollider && CharacterCollider != null) { return; }

        if (alreadyCollidedWith.Contains(other)) { return; }

        alreadyCollidedWith.Add(other);


        if (other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(damage);
        }

      
    }

    public void SetAttack(int damage, float radius = 1.8f, bool isSuperAttack = false)
    {
        this.damage = damage;
        this.radius = radius;
        this.isSuperAttack = isSuperAttack;
    }
}

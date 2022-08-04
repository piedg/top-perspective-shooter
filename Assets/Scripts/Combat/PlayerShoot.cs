using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] Transform firePoint;

    [SerializeField] float fireRate;
    private float nextFire;

    [SerializeField] ObjectPool bulletPool;

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        if (Input.GetMouseButton(0) && Time.fixedTime > nextFire)
        {
            nextFire = Time.fixedTime + fireRate;

            GameObject bullet = bulletPool.GetObjectFromPool();

            //Set missile 
            bullet.transform.SetPositionAndRotation(firePoint.transform.position, firePoint.transform.rotation);

            //Active from Pool
            bullet.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenFury : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float fireRate = 30F;
    private float nextFire = 0.0F;
    public GameObject ShurikenSkill;

    private void Start()
    {
        ShurikenSkill.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            StartCoroutine(Loop());
            ShurikenSkill.SetActive(false);
        }

        if (Time.time > nextFire)
        {
            ShurikenSkill.SetActive(true);
        }
    }


    IEnumerator Loop()
    {
        for (var i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.1f);
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
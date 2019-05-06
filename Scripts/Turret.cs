using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    private Transform target;
    private Enemy targetEnemy;
    private Bullet bullet;

    [Header("Атрибуты")]
    public float range = 15f;

    [Header("Использовать снаряды")]
    private float fireDownCount = 0f;
    public float fireRate = 1f;
    public GameObject bulletPrefab;

    [Header("Использовать лазер")]
    public float slowPct = 0.5f;
    public int damagePerSecond = 35;
    public bool useLaser = false;
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;

    [Header("Unity настройки")]
    public string enemyTag = "Enemy";
    public float turnSpeed = 10f;
    public Transform rotateTransform;
    public Transform firePoint;
    public Transform launchPoint;

    void Start () {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortnesDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemey in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemey.transform.position);
            if (distanceToEnemy < shortnesDistance)
            {
                shortnesDistance = distanceToEnemy;
                nearestEnemy = enemey;
            }

            if (nearestEnemy != null && shortnesDistance <= range && !nearestEnemy.GetComponent<Enemy>().isDeath )
            {
                target = nearestEnemy.transform;
                targetEnemy = target.GetComponent<Enemy>();
            }
            else
            {
                target = null;
            }

        }
    }

    void Update()
    {
        if (target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false;
                }
            }
            return;
        }

        LockOnTarget();
        if (useLaser)
        {
            Laser();
        }
        else
        {
            if (fireDownCount <= 0)
            {
                Shoot();
                fireDownCount = 1f / fireRate;
            }

            fireDownCount -= Time.deltaTime;
        }
    }

    private void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(rotateTransform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        rotateTransform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }


    private void Laser()
    {
        targetEnemy.TakeDamage(damagePerSecond * Time.deltaTime);
        targetEnemy.Slow(slowPct);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.transform.position);

        Vector3 dir = firePoint.position - target.position;
        impactEffect.transform.position = target.position + dir.normalized;
        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }

    void Shoot()
    {
        GameObject bulletGo = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet = bulletGo.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.SetLaunchPoint(launchPoint);
            bullet.SetTarget(target);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

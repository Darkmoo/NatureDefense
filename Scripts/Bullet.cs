using System;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private Transform target;
    private Transform launchPos;
    private Transform curTarget;

    public float expRadius = 0f;
    public float speed = 30f;
    public int bulDamage = 30;
    public GameObject impactEffect;

    public void SetLaunchPoint(Transform point)
    {
        launchPos = point;
    }

    public void SetTarget(Transform tar)
    {
        target = tar;
    }

    void Update() {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        if (launchPos != null)
        {
            curTarget = launchPos;
            speed = 20f;
            if (Vector3.Distance(transform.position, launchPos.position) <= 0.4)
            {
                speed = 30f;
                curTarget = target;
                launchPos = null;
            }
        }
        else
        {
            curTarget = target;
        }

        Vector3 dir = curTarget.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame && launchPos == null)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
	}

    void HitTarget()
    {
        GameObject effect =  (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);

        Destroy(effect, 2f);

        if (expRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }


        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, expRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        if (e != null)
        {
            e.TakeDamage(bulDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, expRadius);
    }
}

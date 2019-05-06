using System;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float startSpeed = 10f;
    public float health = 100;
    public int price = 50;

    [HideInInspector]
    public float speed;
    [HideInInspector]
    public bool isDeath = false;

    void Start()
    {
        speed = startSpeed;
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            if(!isDeath)
                Die();
        }
    }

    private void Die()
    {
        PlayerStats.Money += price;
        isDeath = true;
    }

    public void Slow(float slowPct)
    {
        speed = startSpeed * (1f - slowPct);
    }
}

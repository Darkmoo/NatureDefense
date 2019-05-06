using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {
    public Transform enemyPrfab;
    public Transform spawnPoint;
    public Text counterText;
    public float timeBetweenWaves = 5f;
    private float countdown = 2f;
    private int waveIndex = 0;
    void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
           
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;
        counterText.text = Math.Round(countdown).ToString();
    }

    IEnumerator SpawnWave()
    {
        waveIndex++;
        PlayerStats.Rounds++;
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrfab, spawnPoint.position, spawnPoint.rotation);
    }
}

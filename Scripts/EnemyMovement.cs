using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour {

    private Transform target;
    private int wavepointIndex = 0;
    private Enemy enemy;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        target = Waypoints.points[0];
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4)
        {
            GetNextWaypoint();
        }
        if(!enemy.isDeath)
            enemy.speed = enemy.startSpeed;

        DieScenario();
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }

        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }

    void EndPath()
    {
        PlayerStats.Lives--;
        Destroy(gameObject);
    }

    void DieScenario()
    {
        if (!enemy.isDeath)
            return;

        //сценарий смерти
        if (enemy.speed <= 0)
        {
            Vector3 dir = Vector3.up * 5f;
            transform.Translate(dir * enemy.speed * 6 * Time.deltaTime, Space.World);
            Destroy(gameObject, 4f);
        }
        else
        {
            enemy.speed -= Time.deltaTime * 10;
        }
    }
}

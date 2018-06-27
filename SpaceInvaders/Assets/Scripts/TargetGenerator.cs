using UnityEngine;

public class TargetGenerator : MonoBehaviour {
    public GameObject enemyPrefab;
    public int spawnPointDistance = 5000;
    public int interval = 10;

    private double lastSpawned = 0;
    private int waveSize = 1;
    private Random random;
    private bool negSwitch;

    void Start () {
        SpawnEnemy ();
    }

    void Update () {
        if (Time.time - lastSpawned > interval) {
            for (int i = 0; i < waveSize; i++) {
                SpawnEnemy ();
            }
            waveSize += 1;
            lastSpawned = Time.time;
        }
    }

    void SpawnEnemy () {
        Vector3 spawnPoint = new Vector3 (transform.position.x, transform.position.y, spawnPointDistance);
        Vector3 spawnLocation = spawnPoint + Random.insideUnitSphere * spawnPointDistance / 3;
        var enemy = Instantiate (enemyPrefab, spawnLocation, transform.rotation);
        enemy.GetComponent<TargetController> ().target = transform.position;
    }
}
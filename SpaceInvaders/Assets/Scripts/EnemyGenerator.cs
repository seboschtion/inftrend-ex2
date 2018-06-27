using UnityEngine;

public class EnemyGenerator : MonoBehaviour {
    public GameObject EnemyPrefab;
    public int SpawnPointDistance = 8000;
    public int SecondsBetweenWaves = 6;

    private double timeOfLastWave = 0;
    private int waveSize = 1;
    private Random random;
    private bool negSwitch;

    void Start () {
        SpawnEnemy ();
    }

    void Update () {
        if (Time.time - timeOfLastWave > SecondsBetweenWaves) {
            for (int i = 0; i < waveSize; i++) {
                SpawnEnemy ();
            }
            waveSize += 1;
            timeOfLastWave = Time.time;
        }
    }

    void SpawnEnemy () {
        Vector3 spawnPoint = transform.position + Vector3.forward * SpawnPointDistance ;
        Vector3 spawnPointOffset = Random.insideUnitSphere * SpawnPointDistance / 3;
        var enemy = Instantiate (EnemyPrefab, spawnPoint + spawnPointOffset, transform.rotation);
        enemy.GetComponent<EnemyController> ().Target = transform.position;
    }
}
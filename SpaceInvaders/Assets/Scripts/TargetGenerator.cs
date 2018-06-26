using System;
using UnityEngine;

using Random = System.Random;

public class TargetGenerator : MonoBehaviour
{
    public int interval = 10;
    public GameObject enemyPrefab;
    public int windowThreshold = 7;

    private double lastSpawned;
    private Random random;
    private bool negSwitch;
    
	void Start ()
    {
        random = new Random((int)DateTime.Now.Ticks);
        Spawn();
	}
	
	void Update ()
    {
        if(ConvertToUnixTimestamp(DateTime.Now) - lastSpawned > interval)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        lastSpawned = ConvertToUnixTimestamp(DateTime.Now);
        var enemy = Instantiate(enemyPrefab, transform);

        Vector3 forceDirection = new Vector3(RandomBetweenPosNeg(8), RandomBetweenPosNeg(8), -100);
        var rigid = enemy.GetComponent<Rigidbody>();
        rigid.AddForce(forceDirection * 10, ForceMode.Acceleration);
        rigid.AddTorque(new Vector3(0, 0, Switch(20f)));
    }

    float RandomBetweenPosNeg(float pos)
    {
        var value = random.Next(0, (int)pos);
        if(value < windowThreshold)
        {
            value += windowThreshold;
        }
        return Switch(value);
    }

    float Switch(float value)
    {
        negSwitch = !negSwitch;
        return negSwitch ? -value : value;
    }

    static double ConvertToUnixTimestamp(DateTime date)
    {
        DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        TimeSpan diff = date.ToUniversalTime() - origin;
        return Math.Floor(diff.TotalSeconds);
    }
}

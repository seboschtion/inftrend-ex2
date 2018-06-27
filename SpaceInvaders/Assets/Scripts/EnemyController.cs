using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Vector3 Target;
    public ParticleSystem Laser1;
    public ParticleSystem Laser2;
    public int AttackDistance = 800;

    private float speed;
    private Vector3 finalTarget;
    private bool escaped = false;

    private bool isShooting
    {
        get { return Laser1.isPlaying && Laser2.isPlaying; }
    }

    void Start()
    {
        speed = 10 + Random.value * 5;
        finalTarget = Target + Random.onUnitSphere * 300;
        PrepareLaser(Laser1);
        PrepareLaser(Laser2);
    }

    void PrepareLaser(ParticleSystem laser)
    {
        var main = laser.main;
        main.simulationSpeed = 10;
        laser.Stop();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, Target) < AttackDistance)
        {
            MoveTarget();
            OpenFire();
            MessageEscapedEnemy();
        }

        if (Vector3.Distance(transform.position, Target) < 10)
        {
            Destroy(gameObject);
            GetMainController().EnemyPassed();
        }
        transform.LookAt(Target);
        transform.position = Vector3.MoveTowards(transform.position, Target, speed);
    }

    void MoveTarget()
    {
        Target = Vector3.MoveTowards(Target, finalTarget, speed);
    }

    void OpenFire()
    {
        if (!isShooting)
        {
            Laser1.Play();
            Laser2.Play();
        }
    }

    void MessageEscapedEnemy()
    {
        if (!escaped)
        {
            escaped = true;
        }
    }

    MainController GetMainController()
    {
        GameObject go = GameObject.Find("Main Controller");
        return (MainController)go.GetComponent(typeof(MainController));
    }
}

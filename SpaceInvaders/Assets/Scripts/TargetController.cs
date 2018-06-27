using UnityEngine;

public class TargetController : MonoBehaviour {

    public Vector3 target;
    public ParticleSystem laser1;
    public ParticleSystem laser2;
    public int attackDistance = 1500;
    private Vector3 finalTarget;
    private float speed;
    private bool escaped = false;

    private bool isShooting {
        get { return laser1.isPlaying && laser2.isPlaying; }
    }
    void Start () {
        speed = 10 + Random.value * 5;
        finalTarget = target + Random.onUnitSphere * 300;
        StartLaser (laser1);
        StartLaser (laser2);
    }

    void StartLaser (ParticleSystem laser) {
        var main = laser.main;
        main.simulationSpeed = 10;
        laser.Stop ();
    }

    void Update () {
        if (Vector3.Distance (transform.position, target) < attackDistance) {
            MoveTarget ();
            OpenFire ();
            MessageEscapedEnemy ();
        }

        if (Vector3.Distance (transform.position, target) < 10) {
            Destroy (gameObject);
        }
        transform.LookAt (target);
        transform.position = Vector3.MoveTowards (transform.position, target, speed);
    }

    void MoveTarget () {
        target = Vector3.MoveTowards (target, finalTarget, speed);
    }

    void OpenFire () {
        if (!isShooting) {
            laser1.Play ();
            laser2.Play ();
        }
    }

    void MessageEscapedEnemy () {
        if (!escaped) {
            Debug.Log ("Missing messaging");
            escaped = true;
        }
    }
}
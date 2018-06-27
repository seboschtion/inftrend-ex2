using UnityEngine;

public class GunController : MonoBehaviour {
    public float speed = 0.05F;
    public Camera GameCamera;
    public GameObject GunRotatorX;
    public GameObject GunRotatorY;
    public GameObject ExplosionPrefab;
    public ParticleSystem Laser1;
    public ParticleSystem Laser2;

    public MainController MainController;

    private bool isShooting {
        get { return Laser1.isPlaying && Laser2.isPlaying; }
    }

    void Start () {
        StartLaser (Laser1);
        StartLaser (Laser2);
    }

    void StartLaser (ParticleSystem laser) {
        var main = laser.main;
        main.simulationSpeed = 10;
        laser.Stop ();
    }

    void Update () {
        UpdateGunOpenFire ();
        UpdateGunAim ();
    }

    void UpdateGunOpenFire () {
        if (Input.GetButton ("Fire1")) {
            OpenFire ();
        }
    }

    void UpdateGunAim () {
        Ray ray = GameCamera.ViewportPointToRay (new Vector3 (0.5F, 0.5F, 0));
        RaycastHit hit;
        
        if (Physics.Raycast (ray, out hit) && hit.transform.gameObject.CompareTag ("Enemy")) {
            TargetGameObject (hit.transform.gameObject);
            CheckGunHit (hit);
        } else {
            TargetDirection (ray.direction);
        }
    }

    void CheckGunHit (RaycastHit hit) {
        if (isShooting) {
            DestroyEnemy (hit.transform.gameObject);
        }
    }

    void TargetDirection (Quaternion direction) {
        var rotationY = Quaternion.Euler (new Vector3 (0, 0, -1 * direction.eulerAngles.y));
        var rotationX = Quaternion.Euler (new Vector3 (0, 90, direction.eulerAngles.x));

        GunRotatorY.transform.localRotation = Quaternion.Lerp (GunRotatorY.transform.localRotation, rotationY, Time.time * speed);
        GunRotatorX.transform.localRotation = Quaternion.Lerp (GunRotatorX.transform.localRotation, rotationX, Time.time * speed);
    }

    void TargetDirection (Vector3 direction) {
        Quaternion rotation = Quaternion.LookRotation (direction);
        TargetDirection (rotation);
    }

    void TargetGameObject (GameObject target) {
        Vector3 direction = target.transform.position - GunRotatorX.transform.position;
        TargetDirection (direction);
    }

    void OpenFire () {
        if (!isShooting) {
            Laser1.Play ();
            Laser2.Play ();
        }
    }

    void DestroyEnemy (GameObject enemy) {
        var explosion = Instantiate (ExplosionPrefab, enemy.transform.position, enemy.transform.rotation);
        var particle = explosion.GetComponent<ParticleSystem> ();
        Destroy (enemy);
        Destroy (explosion, particle.main.duration);
        MainController.CountUp ();
    }
}
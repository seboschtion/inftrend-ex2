﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Camera GameCamera;
    public GameObject GunRotatorX;
    public GameObject GunRotatorY;
    public GameObject ExplosionPrefab;
    public ParticleSystem LaserChargeBeam1;
    public ParticleSystem LaserChargeBeam2;

    private bool isShooting
    {
        get { return LaserChargeBeam1.isPlaying && LaserChargeBeam2.isPlaying; }
    }

    void Start()
    {
        StartLaser(LaserChargeBeam1);
        StartLaser(LaserChargeBeam2);
    }

    void StartLaser(ParticleSystem laser)
    {
        var main = laser.main;
        main.simulationSpeed = 10;
        laser.Stop();
    }
    
    void Update()
    {
        Ray ray = GameCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            TargetGameObject(hit.transform.gameObject);
            if (isShooting && hit.transform.gameObject.tag == "Enemy")
            {
                DestroyEnemy(hit.transform.gameObject);
            }
        }
        else
        {
            TargetDirection(ray.direction);
        }

        if (Input.GetButton("Fire1"))
        {
            Fire();
        }
    }

    void TargetDirection(Quaternion direction)
    {
        GunRotatorY.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -1 * direction.eulerAngles.y));
        GunRotatorX.transform.localRotation = Quaternion.Euler(new Vector3(0, 90, direction.eulerAngles.x));
    }

    void TargetDirection(Vector3 direction)
    {
        Quaternion rotation = Quaternion.LookRotation(direction);
        TargetDirection(rotation);
    }

    void TargetGameObject(GameObject target)
    {
        Vector3 direction = target.transform.position - GunRotatorX.transform.position;
        TargetDirection(direction);
    }

    void Fire()
    {
        if (!isShooting)
        {
            LaserChargeBeam1.Play();
            LaserChargeBeam2.Play();
        }
    }

    void DestroyEnemy(GameObject enemy)
    {
        var explosion = Instantiate(ExplosionPrefab, enemy.transform.position, enemy.transform.rotation);
        var particle = explosion.GetComponent<ParticleSystem>();
        Destroy(enemy);
        Destroy(particle, particle.main.duration);
    }
}

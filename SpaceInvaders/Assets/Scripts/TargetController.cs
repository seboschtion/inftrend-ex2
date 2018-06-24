using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{     
	void Update ()
    {
        //if (gameObject.transform.position.z <= -100)
        //{
        //    gameObject.SetActive(false);
        //}
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("particle collided!");
    }
}

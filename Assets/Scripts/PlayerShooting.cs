using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    public GameObject prefab;
    public GameObject shootingPoint;
    public ParticleSystem muzzleEffect;

    public void OnFire(InputValue value)
    {
        if (value.isPressed)
        {
            GameObject clone = Instantiate(prefab);
            clone.transform.position = shootingPoint.transform.position;
            clone.transform.rotation = shootingPoint.transform.rotation;
            muzzleEffect.Play();
        }
    }
}
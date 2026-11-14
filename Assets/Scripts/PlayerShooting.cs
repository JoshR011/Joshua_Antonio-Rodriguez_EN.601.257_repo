using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    public GameObject prefab;
    public GameObject shootingPoint;
    public ParticleSystem muzzleEffect;
    public AudioSource shootSound;
    public int bulletsAmount = 100;
    public float fireRate = 0.2f;

    private Animator animator;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void OnFire(InputValue value)
    {
        if (animator != null)
            animator.SetBool("Shooting", value.isPressed);

        if (value.isPressed)
            InvokeRepeating(nameof(Shoot), 0f, fireRate);
        else
            CancelInvoke(nameof(Shoot));
    }

    void Shoot()
    {
        if (bulletsAmount > 0 && Time.timeScale > 0)
        {
            bulletsAmount--;
            GameObject clone = Instantiate(prefab);
            clone.transform.position = shootingPoint.transform.position;
            clone.transform.rotation = shootingPoint.transform.rotation;
            muzzleEffect.Play();
            shootSound.Play();
        }
    }
}

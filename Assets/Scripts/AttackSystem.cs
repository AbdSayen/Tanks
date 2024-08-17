using System.Collections;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    [Header("Reborn Settings")]
    [SerializeField] private Transform rebornPlace;
    [SerializeField] private float rebornTime;

    [Header("Attack Settings")]
    [SerializeField] private KeyCode attackKey = KeyCode.Space;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform barrel;
    [SerializeField] private float reloadTime = 1f;
    [SerializeField] private float bulletSpeed = 15f;
    [SerializeField] private float recoilForce = 5f; // Сила отдачи

    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;

    [HideInInspector] public bool isAlive = true;
    private float currentHealth;
    private bool canAttack = true;
    private ParticleSystem hitEffect;
    private Rigidbody rb;

    private const string BULLET_TAG = "Bullet";

    private void Start()
    {
        hitEffect = GetComponent<ParticleSystem>();
        rb = GetComponent<Rigidbody>();

        hitEffect.Stop();
        currentHealth = maxHealth;
        Respawn(0f);
    }

    private void Update()
    {
        HandleAttackInput();
    }

    private void HandleAttackInput()
    {
        if (canAttack && Input.GetKeyDown(attackKey))
        {
            Attack();
            StartCoroutine(ReloadRoutine());
        }
    }

    private void Attack()
    {
        GameObject newBullet = Instantiate(bulletPrefab, barrel.position, barrel.rotation);
        Rigidbody bulletRb = newBullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.velocity = barrel.forward * bulletSpeed;
        }

        ApplyRecoil();
    }

    private void ApplyRecoil()
    {
        if (rb != null)
        {
            // Отдача: добавляем импульс в противоположном направлении выстрела
            rb.AddForce(-barrel.forward * recoilForce, ForceMode.Impulse);
        }
    }

    private void TakeDamage(float damage)
    {
        if (!isAlive) return;

        currentHealth -= damage;
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        isAlive = false;
        hitEffect.Play();
        Respawn(rebornTime);
    }

    public void Respawn(float delay)
    {
        StartCoroutine(RespawnRoutine(delay));
    }

    private IEnumerator RespawnRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        isAlive = true;
        currentHealth = maxHealth;
        transform.position = rebornPlace.position;
        transform.rotation = rebornPlace.rotation;
        hitEffect.Stop();
    }

    private IEnumerator ReloadRoutine()
    {
        canAttack = false;
        yield return new WaitForSeconds(reloadTime);
        canAttack = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(BULLET_TAG))
        {
            TakeDamage(100f);
        }
    }
}

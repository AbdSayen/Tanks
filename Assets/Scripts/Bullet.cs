using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 20f;

    private void Start()
    {
        StartCoroutine(AutoDestroy());
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    private IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}

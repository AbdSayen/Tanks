using UnityEngine;
using YG;

public class Base : MonoBehaviour
{
    private string lang;
    private ParticleSystem particle;

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
        lang = YandexGame.EnvironmentData.language;
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * 75);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Bullet")
        {
            Lose();
        }
    }

    private void Lose()
    {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
        {
            GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<AttackSystem>().Respawn(2);
            particle.Play();
        }
    }
}